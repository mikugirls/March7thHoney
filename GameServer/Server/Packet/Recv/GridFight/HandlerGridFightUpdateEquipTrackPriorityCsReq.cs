using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightUpdateEquipTrackPriorityCsReq)]
public class HandlerGridFightUpdateEquipTrackPriorityCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = GridFightUpdateEquipTrackPriorityCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketGridFightUpdateEquipTrackScRsp());
    }
}
