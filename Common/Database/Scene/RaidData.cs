using March7thHoney.Database.Lineup;
using March7thHoney.Proto;
using March7thHoney.Util;
using SqlSugar;

namespace March7thHoney.Database.Scene;

[SugarTable("RaidData")]
public class RaidData : BaseDatabaseDataHelper
{
    [SugarColumn(IsJson = true)] public Dictionary<int, Dictionary<int, RaidRecord>> RaidRecordDatas { get; set; } = [];

    [SugarColumn(IsJson = true)]
    [Obsolete("Using RaidRecordDatas")]
    public Dictionary<int, RaidRecord> RaidRecordData { get; set; } = [];

    
    

    
    

    
    public int CurRaidId { get; set; }
    public int CurRaidWorldLevel { get; set; }
}

public class RaidRecord
{
    
    public int RaidId { get; set; }
    public int WorldLevel { get; set; }
    public RaidStatus Status { get; set; }
    public long FinishTimeStamp { get; set; }

    
    public List<LineupAvatarInfo> Lineup { get; set; } = [];

    
    public Position Pos { get; set; } = new();
    public Position Rot { get; set; } = new();
    public int PlaneId { get; set; }
    public int FloorId { get; set; }
    public int EntryId { get; set; }

    public Position OldPos { get; set; } = new();
    public Position OldRot { get; set; } = new();
    public int OldEntryId { get; set; }
}