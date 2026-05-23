using March7thHoney.Enums.Task;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace March7thHoney.Data.Config.Task;

public class TargetFetchAdvPropEx : TargetEvaluator
{
    [JsonConverter(typeof(StringEnumConverter))]
    public TargetFetchAdvPropFetchTypeEnum FetchType { get; set; }

    
    public FetchAdvPropData SinglePropID { get; set; } = new();

    
    
    
    
    public DynamicFloat PropGroup { get; set; } = new();
    public DynamicFloat PropIDInOwnerGroup { get; set; } = new();
}