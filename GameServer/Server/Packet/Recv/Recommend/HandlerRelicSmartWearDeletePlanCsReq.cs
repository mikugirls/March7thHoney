using March7thHoney.GameServer.Server.Packet.Send.Recommend;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Recommend;

[Opcode(CmdIds.RelicSmartWearDeletePlanCsReq)]
public class HandlerRelicSmartWearDeletePlanCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RelicSmartWearDeletePlanCsReq.Parser.ParseFrom(data);
        connection.Player!.AvatarManager!.DeleteRelicPlan((int)req.UniqueId);
        await connection.SendPacket(new PacketRelicSmartWearDeletePlanScRsp(req.UniqueId));
    }
}
