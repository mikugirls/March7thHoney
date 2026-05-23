using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.TrainParty;

[Opcode(CmdIds.TrainPartySetBadgeAutoFillCsReq)]
public class HandlerTrainPartySetBadgeAutoFillCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TrainPartySetBadgeAutoFillCsReq.Parser.ParseFrom(data);
        var ret = connection.Player!.TrainPartyManager!.SetBadgeAutoFill(req.AELMBPLDFGJ);

        var rsp = new BasePacket(CmdIds.TrainPartySetBadgeAutoFillScRsp);
        rsp.SetData(new TrainPartySetBadgeAutoFillScRsp
        {
            AELMBPLDFGJ = req.AELMBPLDFGJ,
            Retcode = ret
        });
        await connection.SendPacket(rsp);
    }
}
