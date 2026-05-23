using Google.Protobuf;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Game.Activity.Activities.DiceCombat;

public class DiceCombatStageState
{
    public bool Completed { get; set; }
    public long CompletedTime { get; set; }
}

public class DiceCombatActivityInstance : BaseActivityInstance
{
    public GetSystemDataRsp Snapshot { get; }
    public Dictionary<uint, DiceCombatStageState> Stages { get; } = new();

    public DiceCombatActivityInstance(ActivityManager manager) : base(manager)
    {
        Snapshot = DiceCombatConfig.BuildOfficialCompletedSnapshot();

        foreach (var stage in Snapshot.FAMFCIPLGON)
            Stages[stage.FLEOCMEFANH] = new DiceCombatStageState
            {
                Completed = true,
                CompletedTime = stage.UnlockTimestamp
            };
    }

    public DiceCombatAvatar? FindAvatar(uint avatarId)
    {
        foreach (var avatar in Snapshot.AvatarList)
            if (avatar.DiceAvatarId == avatarId) return avatar;
        return null;
    }

    public void RecordStageFinish(uint stageId, bool isWin)
    {
        if (!isWin) return;
        var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        if (!Stages.TryGetValue(stageId, out var state))
        {
            state = new DiceCombatStageState();
            Stages[stageId] = state;
            Snapshot.FAMFCIPLGON.Add(new INACEGAAALJ { FLEOCMEFANH = stageId, UnlockTimestamp = time });
        }
        state.Completed = true;
        state.CompletedTime = time;
    }

    public byte[] BuildSystemDataPayload() => Snapshot.ToByteArray();
}
