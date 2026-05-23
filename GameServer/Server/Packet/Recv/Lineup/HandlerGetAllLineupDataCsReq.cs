using March7thHoney.GameServer.Server.Packet.Send.Lineup;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Lineup;

[Opcode(CmdIds.GetAllLineupDataCsReq)]
public class HandlerGetAllLineupDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetAllLineupDataScRsp(connection.Player!));
    }
}
