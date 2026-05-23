using March7thHoney.Kcp;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Server.Packet.Recv.Player;

[Opcode(CmdIds.PlayerLogoutCsReq)]
public class HandlerPlayerLogoutCsReq : Handler
{
    private readonly Logger _logger = new("GameServer");

    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var uid = connection.Player?.Uid.ToString() ?? "<none>";
        _logger.Info($"Player logout requested. remote={connection.RemoteEndPoint} uid={uid}");

        await March7thHoneyListener.SendDisconnectPacket(connection, 5);
        await Task.Delay(50);
        connection.Stop();
    }
}
