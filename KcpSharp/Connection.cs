using System.Collections.Concurrent;
using System.Net;
using March7thHoney.Kcp.KcpSharp;
using March7thHoney.Util;

namespace March7thHoney.Kcp;

public class March7thHoneyConnection
{
    public const int HANDSHAKE_SIZE = 20;
    public static readonly ConcurrentBag<int> BannedPackets = [];
    private static readonly Logger Logger = new("GameServer");
    public static readonly ConcurrentDictionary<int, string> LogMap = [];

    public static readonly ConcurrentBag<int> IgnoreLog =
    [
        CmdIds.PlayerHeartBeatCsReq, CmdIds.PlayerHeartBeatScRsp, CmdIds.SceneEntityMoveCsReq,
        CmdIds.SceneEntityMoveScRsp, CmdIds.GetShopListCsReq, CmdIds.GetShopListScRsp
    ];

    protected readonly CancellationTokenSource CancelToken;
    protected readonly KcpConversation Conversation;
    public readonly IPEndPoint RemoteEndPoint;

    public string DebugFile = "";
    public bool IsOnline = true;
    public StreamWriter? Writer;

    public March7thHoneyConnection(KcpConversation conversation, IPEndPoint remote)
    {
        Conversation = conversation;
        RemoteEndPoint = remote;
        CancelToken = new CancellationTokenSource();
        if (ConfigManager.Config.GameServer.UsePacketEncryption) XorKey = Crypto.ClientSecretKey!.GetXorKey();

        Start();
    }

    public byte[]? XorKey { get; set; }
    public ulong ClientSecretKeySeed { get; set; }

    public long? ConversationId => Conversation.ConversationId;

    public SessionStateEnum State { get; set; } = SessionStateEnum.INACTIVE;
    

    public virtual void Start()
    {
        Logger.Info($"New connection from {RemoteEndPoint}.");
        State = SessionStateEnum.WAITING_FOR_TOKEN;
    }

    public virtual void Stop()
    {
        
        
        Conversation.Dispose();
        try
        {
            CancelToken.Cancel();
            CancelToken.Dispose();
        }
        catch
        {
        }

        IsOnline = false;
    }

    public void LogPacket(string sendOrRecv, ushort opcode, byte[] payload)
    {
        if (!ConfigManager.Config.ServerOption.LogOption.EnableGamePacketLog) return;

        try
        {
            if (IgnoreLog.Contains(opcode)) return;

            if (ConfigManager.Config.ServerOption.LogOption.DisableLogDetailPacket) throw new Exception();

            var asJson = PacketLogHelper.ConvertPacketToJson(opcode, payload);
            var output = $"{sendOrRecv}: {LogMap[opcode]}({opcode})\r\n{asJson}";

            if (ConfigManager.Config.ServerOption.LogOption.LogPacketToConsole)
                Logger.Debug(output);

            if (DebugFile == "" || !ConfigManager.Config.ServerOption.LogOption.SavePersonalDebugFile) return;
            var sw = GetWriter();
            sw.WriteLine($"[{DateTime.Now:HH:mm:ss}] [GameServer] [DEBUG] " + output);
            sw.Flush();
        }
        catch
        {
            var output = $"{sendOrRecv}: {LogMap.GetValueOrDefault(opcode, "UnknownPacket")}({opcode})";

            if (ConfigManager.Config.ServerOption.LogOption.LogPacketToConsole)
                Logger.Debug(output);

            if (DebugFile != "" && ConfigManager.Config.ServerOption.LogOption.SavePersonalDebugFile)
            {
                var sw = GetWriter();
                sw.WriteLine($"[{DateTime.Now:HH:mm:ss}] [GameServer] [DEBUG] " + output);
                sw.Flush();
            }
        }
    }

    private StreamWriter GetWriter()
    {
        
        var file = new FileInfo(DebugFile);
        if (!file.Exists)
        {
            Directory.CreateDirectory(file.DirectoryName!);
            File.Create(DebugFile).Dispose();
        }

        Writer ??= new StreamWriter(DebugFile, true);
        return Writer;
    }

    public async Task SendPacket(byte[] packet)
    {
        try
        {
            if (ConfigManager.Config.GameServer.UsePacketEncryption)
                Crypto.Xor(packet, XorKey!);

            _ = await Conversation.SendAsync(packet, CancelToken.Token);
        }
        catch
        {
            
        }
    }

    public async Task SendPacket(BasePacket packet)
    {
        
        if (packet.CmdId <= 0)
        {
            Logger.Debug("Tried to send packet with missing cmd id!");
            return;
        }

        
        if (BannedPackets.Contains(packet.CmdId)) return;
        LogPacket("Send", packet.CmdId, packet.Data);
        
        var packetBytes = packet.BuildPacket();

        try
        {
            await SendPacket(packetBytes);
        }
        catch
        {
            
        }

        if (packet.CmdId == CmdIds.SetClientPausedScRsp)
        {
            await SendWatermarkLuaAsync();
        }

    }

    public async Task SendPacket(int cmdId)
    {
        await SendPacket(new BasePacket((ushort)cmdId));
    }

    public virtual Task SendWatermarkLuaAsync() => Task.CompletedTask;
}
