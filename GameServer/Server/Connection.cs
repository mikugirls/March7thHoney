using System.Buffers;
using System.Globalization;
using System.Net;
using System.Reflection;
using March7thHoney.Configuration;
using March7thHoney.Data.Custom;
using March7thHoney.Enums.Server;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.GameServer.Server.Packet;
using March7thHoney.Kcp;
using March7thHoney.Kcp.KcpSharp;
using March7thHoney.Proto;
using March7thHoney.Util;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace March7thHoney.GameServer.Server;

public class Connection(KcpConversation conversation, IPEndPoint remote) : March7thHoneyConnection(conversation, remote)
{
    private static readonly Logger Logger = new("GameServer");

    public PlayerInstance? Player { get; set; }
    public List<PacketActionData> CustomPacketQueue { get; set; } = [];

    private PacketActionData? GetCurActionData()
    {
        if (CustomPacketQueue.Count == 0) return null;
        var actionData = CustomPacketQueue[0];

        return actionData;
    }

    public override async void Start()
    {
        Logger.Info($"New connection from {RemoteEndPoint}.");
        State = SessionStateEnum.WAITING_FOR_TOKEN;
        await ReceiveLoop();
    }

    public override void Stop()
    {
        if (!IsOnline) return;

        Player?.TrainCakeCatchManager?.LeaveSocialPlayRoom();
        Player?.OnLogoutAsync();
        March7thHoneyListener.UnregisterConnection(this);
        base.Stop();
    }

    public override async Task SendWatermarkLuaAsync()
    {
        if (Player == null) return;
        var watermark = ConfigManager.Config.ServerOption.Watermark;
        if (!watermark.Enabled) return;

        var nickname = Player.Data.Name ?? "";
        var uid = Player.Uid.ToString();
        var version = string.IsNullOrEmpty(ClientVersionCache.LatestVersion)
            ? $"WIN{GameConstants.GAME_VERSION}"
            : ClientVersionCache.LatestVersion;

        var line1 = BuildWatermarkLine(watermark.Line1Template, nickname, uid, version, watermark);
        var line2 = BuildWatermarkLine(watermark.Line2Template, nickname, uid, version, watermark);

        var lines = new List<string>();
        if (!string.IsNullOrWhiteSpace(line1)) lines.Add(line1);
        if (!string.IsNullOrWhiteSpace(line2)) lines.Add(line2);
        if (lines.Count == 0) return;

        var luaText = EscapeLuaString(string.Join("\n", lines));
        var fontSize = watermark.FontSize.ToString(CultureInfo.InvariantCulture);

        var lua =
            "local function modify_texts()\n" +
            "    local vGo = CS.UnityEngine.GameObject.Find(\"VersionText\")\n" +
            "    if vGo == nil then return end\n" +
            "    local vt = vGo:GetComponent(\"Text\")\n" +
            "    vt.supportRichText = true\n" +
            "    vt.text = \"" + luaText + "\"\n" +
            "    vt.fontSize = " + fontSize + "\n" +
            "end\n" +
            "\n" +
            "local function on_error(err)\n" +
            "    local files = io.open(\"./error.txt\", \"w\")\n" +
            "    files:write(err)\n" +
            "    files:close()\n" +
            "end\n" +
            "\n" +
            "local status, err = pcall(modify_texts)\n" +
            "if not status then on_error(err) end\n";

        var pkt = new HandshakePacket(System.Text.Encoding.UTF8.GetBytes(lua));
        await SendPacket(pkt.BuildPacket());
    }

    private static string BuildWatermarkLine(string template, string nickname, string uid, string version,
        WatermarkConfig watermark)
    {
        var line = template
            .Replace("{nickname}", nickname, StringComparison.Ordinal)
            .Replace("{uid}", uid, StringComparison.Ordinal)
            .Replace("{version}", version, StringComparison.Ordinal)
            .Replace("{serverName}", ConfigManager.Config.GameServer.GameServerName, StringComparison.Ordinal)
            .Replace("{serverId}", ConfigManager.Config.GameServer.GameServerId, StringComparison.Ordinal);

        if (!watermark.UseGradient) return line;
        return ApplyGradient(line, watermark.GradientStartColor, watermark.GradientEndColor);
    }

    private static string ApplyGradient(string line, string startColor, string endColor)
    {
        if (!TryParseHexColor(startColor, out var r0, out var g0, out var b0) ||
            !TryParseHexColor(endColor, out var r1, out var g1, out var b1))
        {
            return line;
        }

        var visible = 0;
        foreach (var c in line)
            if (c != ' ') visible++;

        var sb = new System.Text.StringBuilder(line.Length * 24);
        var idx = 0;
        foreach (var c in line)
        {
            if (c == ' ')
            {
                sb.Append(' ');
                continue;
            }
            var t = visible <= 1 ? 0.0 : (double)idx / (visible - 1);
            var r = (int)(r0 + (r1 - r0) * t);
            var g = (int)(g0 + (g1 - g0) * t);
            var b = (int)(b0 + (b1 - b0) * t);
            sb.Append($"<color=#{r:X2}{g:X2}{b:X2}>").Append(c).Append("</color>");
            idx++;
        }
        return sb.ToString();
    }

    private static bool TryParseHexColor(string value, out int r, out int g, out int b)
    {
        r = g = b = 0;
        var hex = value.Trim();
        if (hex.StartsWith("#", StringComparison.Ordinal))
            hex = hex[1..];

        if (hex.Length != 6) return false;
        if (!int.TryParse(hex[..2], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out r)) return false;
        if (!int.TryParse(hex.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out g))
            return false;
        return int.TryParse(hex.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out b);
    }

    private static string EscapeLuaString(string s)
    {
        return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "");
    }

    protected async Task ReceiveLoop()
    {
        try
        {
            while (!CancelToken.IsCancellationRequested)
            {
                KcpConversationReceiveResult result;
                try
                {
                    
                    result = await Conversation.WaitToReceiveAsync(CancelToken.Token);
                }
                catch (OperationCanceledException) when (CancelToken.IsCancellationRequested)
                {
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }

                if (result.TransportClosed)
                {
                    Logger.Debug("Connection was closed");
                    break;
                }

                var buffer = ArrayPool<byte>.Shared.Rent(result.BytesReceived);
                try
                {
                    
                    
                    if (!Conversation.TryReceive(buffer, out result))
                    {
                        Logger.Error("Failed to receive packet");
                        break;
                    }

                    await ProcessMessageAsync(buffer.AsMemory(0, result.BytesReceived));
                }
                catch (Exception ex)
                {
                    Logger.Error("Packet parse error", ex);
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buffer);
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error("Connection receive loop crashed", ex);
        }

        Stop();
    }

    
    private async Task ProcessMessageAsync(Memory<byte> data)
    {
        var gamePacket = data.ToArray();
        if (ConfigManager.Config.GameServer.UsePacketEncryption)
            Crypto.Xor(gamePacket, XorKey!);

        await using MemoryStream ms = new(gamePacket);
        using BinaryReader br = new(ms);

        
        try
        {
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                
                if (br.BaseStream.Length - br.BaseStream.Position < 12) return;
                
                var magic1 = br.ReadUInt32BE();
                if (magic1 != 0x9D74C714)
                {
                    Logger.Error($"Bad Data Package Received: got 0x{magic1:X}, expect 0x9D74C714");
                    return; 
                }

                
                var opcode = br.ReadUInt16BE();
                var headerLength = br.ReadUInt16BE();
                var payloadLength = br.ReadUInt32BE();
                var header = br.ReadBytes(headerLength);
                var payload = br.ReadBytes((int)payloadLength);
                LogPacket("Recv", opcode, payload);
                await HandlePacket(opcode, header, payload);
            }
        }
        catch (Exception e)
        {
            Logger.Error(e.Message, e);
        }
    }

    private async Task HandlePacket(ushort opcode, byte[] header, byte[] payload)
    {
        
        var action = GetCurActionData();
        var packetName = LogMap.GetValueOrDefault(opcode);

        if (action is { Action: PacketActionTypeEnum.WaitForPacket } && action.Param.PacketName == packetName)
        {
            
            var interrupt = action.Param.InterruptFormalHandler;
            while (true)
            {
                switch (action.Action)
                {
                    case PacketActionTypeEnum.SendPacket:
                    {
                        var sendPacket = new BasePacket((ushort)LogMap.FirstOrDefault(x =>
                            x.Value == action.Param.PacketName).Key);

                        sendPacket.SetData(action.Param.PacketData);

                        await SendPacket(sendPacket);
                        break;
                    }
                    case PacketActionTypeEnum.WaitForPacket:
                    {
                        break;
                    }
                }

                CustomPacketQueue.RemoveAt(0);
                action = GetCurActionData();
                if (action == null || action.Action == PacketActionTypeEnum.WaitForPacket) break;
            }

            if (interrupt)
                return;
        }

        
        var handler = HandlerManager.GetHandler(opcode);
        if (handler != null)
        {
            
            
            var state = State;
            switch (opcode)
            {
                case CmdIds.PlayerGetTokenCsReq:
                {
                    if (state != SessionStateEnum.WAITING_FOR_TOKEN) return;
                    goto default;
                }
                case CmdIds.PlayerLoginCsReq:
                {
                    if (state != SessionStateEnum.WAITING_FOR_LOGIN) return;
                    goto default;
                }
                default:
                    break;
            }

            try
            {
                await handler.OnHandle(this, header, payload);
            }
            catch (Exception e)
            {
                Logger.Error("An error occured ", e);

                
                var curPacket = LogMap.GetValueOrDefault(opcode);
                if (curPacket == null) return;

                var rspName = curPacket.Replace("Cs", "Sc").Replace("Req", "Rsp"); 
                if (rspName == curPacket) return; 
                if (!TryGetOpcodeByName(rspName, out var rspOpcode)) return;

                
                var typ = AppDomain.CurrentDomain.GetAssemblies()
                    .SingleOrDefault(assembly => assembly.GetName().Name == "March7thHoney.Proto")!.GetTypes()
                    .First(t => t.Name == rspName); 
                var curTyp = AppDomain.CurrentDomain.GetAssemblies()
                    .SingleOrDefault(assembly => assembly.GetName().Name == "March7thHoney.Proto")!.GetTypes()
                    .First(t => t.Name == curPacket); 

                
                if (Activator.CreateInstance(typ) is not IMessage rsp) return;

                
                var retCode = typ.GetProperty("Retcode");
                retCode?.SetValue(rsp, (uint)Retcode.RetFail);

                
                var descriptor =
                    curTyp.GetProperty("Descriptor", BindingFlags.Public | BindingFlags.Static)?.GetValue(
                        null, null) as MessageDescriptor; 
                var reqPacket = descriptor?.Parser.ParseFrom(payload);

                foreach (var propertyInfo in curTyp.GetProperties())
                {
                    var prop = typ.GetProperty(propertyInfo.Name);
                    if (prop != null && prop.CanWrite)
                    {
                        var value = propertyInfo.GetValue(reqPacket);
                        if (value != null)
                            prop.SetValue(rsp, value);
                    }
                }

                
                var packet = new BasePacket((ushort)rspOpcode);
                packet.SetData(rsp);
                await SendPacket(packet);
            }

            return;
        }

        
        
        if (packetName == null) return;

        var respName = packetName.Replace("Cs", "Sc").Replace("Req", "Rsp"); 
        if (respName == packetName) return; 
        if (!TryGetOpcodeByName(respName, out var respOpcode)) return;

        
        await SendPacket(respOpcode);
    }

    private static bool TryGetOpcodeByName(string packetName, out int opcode)
    {
        var match = LogMap.FirstOrDefault(x => x.Value == packetName);
        opcode = match.Key;
        return match.Value != null && opcode > 0;
    }
}
