using March7thHoney.Data.Excel;

namespace March7thHoney.Data.Custom;

public class GridFightBasicOrbRewardsConfig
{
    public Dictionary<uint, GridFightBasicOrbRewardsInfo> OrbRewards { get; set; } = [];
}

public class GridFightBasicOrbRewardsInfo
{
    public uint OrbId { get; set; }
    public Dictionary<uint, List<GridFightBasicBonusPoolV2Excel>> Rewards { get; set; } = [];
}