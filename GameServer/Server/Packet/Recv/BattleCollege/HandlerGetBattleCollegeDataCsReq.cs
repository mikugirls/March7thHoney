using March7thHoney.GameServer.Server.Packet.Send.BattleCollege;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.BattleCollege;

[Opcode(CmdIds.GetBattleCollegeDataCsReq)]
public class HandlerGetBattleCollegeDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetBattleCollegeDataScRsp(connection.Player!));
    }
}
