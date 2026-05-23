using March7thHoney.GameServer.Server.Packet.Send.Tutorial;
using March7thHoney.Kcp;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Server.Packet.Recv.Tutorial;

[Opcode(CmdIds.GetTutorialCsReq)]
public class HandlerGetTutorialCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await SendPlayerData(connection);
        if (ConfigManager.Config.ServerOption.EnableMission) 
            await connection.SendPacket(new PacketGetTutorialScRsp(connection.Player!));
    }

    private async Task SendPlayerData(Connection connection)
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, "Lua", "welcome.lua");
        if (File.Exists(filePath))
        {
            var fileBytes = await File.ReadAllBytesAsync(filePath);
            await connection.SendPacket(new HandshakePacket(fileBytes));
        }
    }
}
