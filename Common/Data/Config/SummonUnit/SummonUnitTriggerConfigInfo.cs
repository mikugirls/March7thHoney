using Newtonsoft.Json.Linq;

namespace March7thHoney.Data.Config.SummonUnit;

public class SummonUnitTriggerConfigInfo
{
    
    
    public List<UnitCustomTriggerConfigInfo> CustomTriggers { get; set; } = [];

    public static SummonUnitTriggerConfigInfo LoadFromJsonObject(JObject obj)
    {
        SummonUnitTriggerConfigInfo info = new();
        if (obj.ContainsKey(nameof(CustomTriggers)))
            info.CustomTriggers = obj[nameof(CustomTriggers)]
                ?.Select(x => UnitCustomTriggerConfigInfo.LoadFromJsonObject((x as JObject)!)).ToList() ?? [];
        return info;
    }
}