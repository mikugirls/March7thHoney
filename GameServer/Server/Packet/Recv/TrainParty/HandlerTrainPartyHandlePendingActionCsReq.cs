using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.TrainParty;

[Opcode(CmdIds.TrainPartyHandlePendingActionCsReq)]
public class HandlerTrainPartyHandlePendingActionCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TrainPartyHandlePendingActionCsReq.Parser.ParseFrom(data);
        var manager = connection.Player!.TrainPartyManager!;
        var ret = manager.HandlePendingAction(req.QueuePosition, out var rspProto);
        rspProto.Retcode = ret;

        var rsp = new BasePacket(CmdIds.TrainPartyHandlePendingActionScRsp);
        rsp.SetData(rspProto);
        await connection.SendPacket(rsp);
    }
}
