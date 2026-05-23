using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.TrainParty;

[Opcode(CmdIds.TrainPartyBuildStartStepCsReq)]
public class HandlerTrainPartyBuildStartStepCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TrainPartyBuildStartStepCsReq.Parser.ParseFrom(data);
        var manager = connection.Player!.TrainPartyManager!;
        var ret = manager.BuildStartStep(req.AreaId, req.FNLNPLHOEJN, req.LMHIBHGINLG, out var curFund);
        var packet = new BasePacket(CmdIds.TrainPartyBuildStartStepScRsp);
        packet.SetData(new TrainPartyBuildStartStepScRsp
        {
            CurFund = curFund,
            Retcode = ret
        });
        await connection.SendPacket(packet);
    }
}
