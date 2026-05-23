using March7thHoney.GameServer.Server.Packet.Send.DiceCombat;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.DiceCombat;

[Opcode(CmdIds.V2FinishPveStageCsReq)]
public class HandlerV2FinishPveStageCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = V2FinishPveStageCsReq.Parser.ParseFrom(data);
        connection.Player!.ActivityManager!.DiceCombat.RecordStageFinish(req.MMLKFJIAKKH, req.IsWin);
        await connection.SendPacket(new PacketV2FinishPveStageScRsp(req.MMLKFJIAKKH, req.IsWin));
    }
}
