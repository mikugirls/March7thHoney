using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.GetUnlockTeleportCsReq)]
public class HandlerGetUnlockTeleportCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetUnlockTeleportCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(new PacketGetUnlockTeleportScRsp(req));
    }
}
