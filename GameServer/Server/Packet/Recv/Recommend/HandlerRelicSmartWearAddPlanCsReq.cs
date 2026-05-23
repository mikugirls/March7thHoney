using March7thHoney.GameServer.Server.Packet.Send.Recommend;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Recommend;

[Opcode(CmdIds.RelicSmartWearAddPlanCsReq)]
public class HandlerRelicSmartWearAddPlanCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RelicSmartWearAddPlanCsReq.Parser.ParseFrom(data);
        var plan = connection.Player!.AvatarManager!.AddRelicPlan(req.RelicPlan);
        await connection.SendPacket(new PacketRelicSmartWearAddPlanScRsp(plan));
    }
}
