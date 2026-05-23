using SqlSugar;

namespace March7thHoney.Database.Expedition;

[SugarTable("Expedition")]
public class ExpeditionData : BaseDatabaseDataHelper
{
    [SugarColumn(IsJson = true)]
    public List<uint> ActiveExpeditionIds { get; set; } = [];

    public long RefreshTime { get; set; } = 0;
}
