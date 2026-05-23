using March7thHoney.GameServer.Server.Packet.Send.DiceCombat;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.DiceCombat;

[Opcode(CmdIds.DiceCombatFinishPveStageCsReq)]
public class HandlerDiceCombatFinishPveStageCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = DiceCombatFinishPveStageCsReq.Parser.ParseFrom(data);
        var instance = connection.Player!.ActivityManager!.DiceCombat;
        instance.RecordStageFinish(req.MMLKFJIAKKH, req.IsWin);
        await connection.SendPacket(new PacketDiceCombatFinishPveStageScRsp(req.MMLKFJIAKKH, req.IsWin));
    }
}
