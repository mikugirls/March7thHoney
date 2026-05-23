using March7thHoney.GameServer.Server.Packet.Send.Lineup;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Lineup;

[Opcode(CmdIds.GetLineupAvatarDataCsReq)]
public class HandlerGetLineupAvatarDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetLineupAvatarDataScRsp(connection.Player!));
    }
}
