using SqlSugar;

namespace March7thHoney.Database.TrainCakeCatch;

[SugarTable("TrainCakeCatch")]
public class TrainCakeCatchData : BaseDatabaseDataHelper
{
    [SugarColumn(IsJson = true)] public List<CakeTreeSlotInfo> CatTreeSlots { get; set; } = [];

    [SugarColumn(IsJson = true)] public List<DiyDiceSlotInfo> DiceSlots { get; set; } = [];

    [SugarColumn(IsJson = true)] public List<DiyStagePlacement> StagePlacements { get; set; } = [];

    public int DiyTheme { get; set; }

    [SugarColumn(IsJson = true)] public List<int> PerformanceIds { get; set; } = [];
    [SugarColumn(IsJson = true)] public List<CakeCreatureInventoryInfo> CollectedCreatures { get; set; } = [];
    [SugarColumn(IsJson = true)] public List<int> SearchCreatureIds { get; set; } = [];
    [SugarColumn(IsJson = true)] public List<int> DiyLikeIds { get; set; } = [];
    public int AvailableSearchCount { get; set; }
    public long RefreshTime { get; set; }
    public long DailyRefreshTime { get; set; }
}

public class CakeCreatureInventoryInfo
{
    public int CreatureId { get; set; }
    public int Count { get; set; }
}

public class CakeTreeSlotInfo
{
    public int CreatureId { get; set; }
    public int Slot { get; set; }
}

public class DiyDiceSlotInfo
{
    public int DiceSlotId { get; set; }
    public int Index { get; set; }
    public List<int> Values { get; set; } = [];
}

public class DiyStagePlacement
{
    public int CreatureId { get; set; }
    public int Slot { get; set; }
}
