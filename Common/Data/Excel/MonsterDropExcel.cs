using March7thHoney.Database.Inventory;
using March7thHoney.Util;
using Newtonsoft.Json;

namespace March7thHoney.Data.Excel;

[ResourceEntity("MonsterDrop.json")]
public class MonsterDropExcel : ExcelResource
{
    public int MonsterTemplateID { get; set; }
    public int WorldLevel { get; set; }
    public int AvatarExpReward { get; set; }

    public List<MonsterDropItem> DisplayItemList { get; set; } = [];

    [JsonIgnore] public List<MonsterDropItem> DropItemList { get; set; } = [];

    public override int GetId()
    {
        return MonsterTemplateID * 10 + WorldLevel;
    }

    public override void AfterAllDone()
    {
        foreach (var item in DisplayItemList)
        {
            GameData.ItemConfigData.TryGetValue(item.ItemID, out var config);
            if (config == null) continue;
            
            
            
            
            
            
            
            
            double mod = 1; 
            double count = WorldLevel + 3;
            var maxCount = (int)(count * mod);
            var minCount = (int)(count * mod * 0.5);
            item.MinCount = minCount;
            item.MaxCount = maxCount;
            DropItemList.Add(item);
        }

        GameData.MonsterDropData.Add(GetId(), this);
    }

    public List<ItemData> CalculateDrop()
    {
        var result = new List<ItemData>();
        foreach (var item in DropItemList)
        {
            var count = Random.Shared.Next(item.MinCount, item.MaxCount + 1);
            result.Add(new ItemData
            {
                ItemId = item.ItemID,
                Count = count * ConfigManager.Config.ServerOption.ValidFarmingDropRate()
            });
        }

        return result;
    }
}

public class MonsterDropItem
{
    public int ItemID { get; set; }

    [JsonIgnore] public int MinCount { get; set; }

    [JsonIgnore] public int MaxCount { get; set; }
}