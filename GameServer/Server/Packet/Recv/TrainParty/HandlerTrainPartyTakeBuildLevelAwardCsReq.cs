using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.TrainParty;

[Opcode(CmdIds.TrainPartyTakeBuildLevelAwardCsReq)]
public class HandlerTrainPartyTakeBuildLevelAwardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TrainPartyTakeBuildLevelAwardCsReq.Parser.ParseFrom(data);
        var manager = connection.Player!.TrainPartyManager!;
        var ret = manager.TakeBuildLevelAward(req.HBLOHOFKKKO, out var items);

        var rsp = new BasePacket(CmdIds.TrainPartyTakeBuildLevelAwardScRsp);
        rsp.SetData(new TrainPartyTakeBuildLevelAwardScRsp
        {
            HBLOHOFKKKO = req.HBLOHOFKKKO,
            ItemList = items,
            Retcode = ret
        });
        await connection.SendPacket(rsp);
    }
}
