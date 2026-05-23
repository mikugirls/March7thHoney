using March7thHoney.Data;
using March7thHoney.Proto;
using SqlSugar;

namespace March7thHoney.Database.TrainParty;

[SugarTable("TrainParty")]
public class TrainData : BaseDatabaseDataHelper
{
    public int Fund { get; set; }
    public int RecordId { get; set; }
    public uint WorldId { get; set; }
    public long RefreshTime { get; set; }
    public bool BadgeAutoFill { get; set; }
    [SugarColumn(IsJson = true)] public List<uint> UnlockedPassengerIds { get; set; } = [];
    [SugarColumn(IsJson = true)] public List<uint> PassengerRecordMarks { get; set; } = [];
    [SugarColumn(IsJson = true)] public List<TrainCardInfo> Cards { get; set; } = [];
    [SugarColumn(IsJson = true)] public List<TrainPendingPassengerInfo> GameplayPassengers { get; set; } = [];
    [SugarColumn(IsJson = true)] public List<TrainSkillInfo> GameplaySkills { get; set; } = [];
    [SugarColumn(IsJson = true)] public List<TrainMoveInfo> MoveHistory { get; set; } = [];
    public int GameplayRound { get; set; }
    public int GameplayType { get; set; }
    public int GameplayQueuePosition { get; set; }
    public int LastUsedCardId { get; set; }
    [SugarColumn(IsJson = true)] public Dictionary<int, TrainAreaInfo> Areas { get; set; } = [];
    [SugarColumn(IsJson = true)] public List<TrainSelfDisplayEntry> SelfDisplay { get; set; } = [];
    [SugarColumn(IsJson = true)] public Dictionary<int, long> PropTimes { get; set; } = [];
    [SugarColumn(IsJson = true)] public Dictionary<int, long> TimedDynamicPropTimes { get; set; } = [];
}

public class TrainAreaInfo
{
    public int AreaId { get; set; }
    public List<int> StepList { get; set; } = [];
    public Dictionary<int, int> DynamicInfo { get; set; } = [];

    public TrainPartyArea ToProto()
    {
        var info = new TrainPartyArea
        {
            AreaId = (uint)AreaId,
            AreaStepInfo = new AreaStepInfo(),
            StepIdList = { StepList.Select(x => (uint)x) },
            VerifyStepIdList = { StepList.Select(x => (uint)x) },
            Progress = 100,
            DynamicInfo =
            {
                DynamicInfo.Select(x => new AreaDynamicInfo
                {
                    DiceSlotId = (uint)x.Key,
                    DiyDynamicId = (uint)x.Value
                })
            }
        };

        foreach (var step in StepList)
        {
            GameData.TrainPartyStepConfigData.TryGetValue(step, out var stepExcel);
            if (stepExcel == null) continue;

            info.StaticPropIdList.AddRange(stepExcel.StaticPropIDList.Select(x => (uint)x));
        }

        return info;
    }
}

public class TrainSelfDisplayEntry
{
    public uint Slot { get; set; }
    public uint Id { get; set; }
    public uint Type { get; set; }
}

public class TrainCardInfo
{
    public uint CardId { get; set; }
    public uint UniqueId { get; set; }
    public uint CurIndex { get; set; }
}

public class TrainPendingPassengerInfo
{
    public uint PassengerId { get; set; }
    public uint Hp { get; set; }
    public uint Atk { get; set; }
}

public class TrainSkillInfo
{
    public uint SkillId { get; set; }
    public uint SkillLevel { get; set; }
    public uint Count { get; set; }
}

public class TrainMoveInfo
{
    public uint CardId { get; set; }
    public uint UniqueId { get; set; }
    public uint DisplayValue { get; set; }
    public uint BoardIndex { get; set; }
}
