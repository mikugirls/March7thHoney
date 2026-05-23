using March7thHoney.GameServer.Server.Packet.Send.Mission;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Mission;

[Opcode(CmdIds.GetMissionDataCsReq)]
public class HandlerGetMissionDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetMissionDataScRsp(connection.Player!));
    }
}
