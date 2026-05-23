using March7thHoney.GameServer.Game.GridFight;
using March7thHoney.GameServer.Game.GridFight.Battle;
using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.HNEJHAJJIOB)]
public class HandlerHNEJHAJJIOB : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = HNEJHAJJIOB.Parser.ParseFrom(data);
        var player = connection.Player!;
        var inst = player.GridFightManager?.GridFightInstance;
        if (inst == null)
        {
            await connection.SendPacket(BuildAck(Retcode.RetFail));
            return;
        }

        var ackPos = inst.QueuePosition;
        var idx = (int)req.EliteBranchId - 1;
        if (idx < 0 || idx >= inst.CurrentEliteBranchOptions.Count) idx = 0;
        var chosen = inst.CurrentEliteBranchOptions.Count > 0 ? inst.CurrentEliteBranchOptions[idx] : null;
        if (chosen != null)
            inst.ConfigureNextBattle(chosen.StageId, chosen.MonsterIds);

        inst.LastEliteBranchConsumedSection = inst.SectionId;
        inst.ClearEliteBranchOptions();

        var nextPos = ackPos + 1;
        inst.QueuePosition = nextPos;
        inst.PendingAction = new GridFightPendingAction
        {
            QueuePosition = nextPos,
            ReturnPreparationAction = new GridFightReturnPreparationActionInfo()
        };

        await connection.SendPacket(BuildAck(Retcode.RetSucc));

        var sync = new GridFightSyncUpdateResultScNotify();
        var pickSec = new GridFightSyncResultData();
        pickSec.UpdateDynamicList.Add(new GridFightSyncData
        {
            EliteBranchSyncInfo = new GridFightEliteBranchSyncInfo { EliteBranchId = req.EliteBranchId }
        });
        sync.SyncResultDataList.Add(pickSec);

        var finishSec = new GridFightSyncResultData();
        finishSec.UpdateDynamicList.Add(new GridFightSyncData { FinishPendingActionPos = ackPos });
        finishSec.UpdateDynamicList.Add(new GridFightSyncData { SyncLockInfo = new GridFightLockInfo() });
        sync.SyncResultDataList.Add(finishSec);

        var nextSec = new GridFightSyncResultData();
        nextSec.UpdateDynamicList.Add(new GridFightSyncData
        {
            SyncLockInfo = new GridFightLockInfo
            {
                LockReason = GridFightLockReason.DfofffceffoKjmjdbjmbmc,
                LockType = GridFightLockType.PjbmhhnlclbEhfhdgpocnh
            }
        });
        nextSec.UpdateDynamicList.Add(new GridFightSyncData { PendingAction = inst.PendingAction });
        sync.SyncResultDataList.Add(nextSec);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(sync));
    }

    private static BasePacket BuildAck(Retcode code)
    {
        var rsp = new BasePacket((ushort)CmdIds.AKHODIIOGDJ);
        rsp.SetData(new AKHODIIOGDJ { Retcode = (uint)code });
        return rsp;
    }
}
