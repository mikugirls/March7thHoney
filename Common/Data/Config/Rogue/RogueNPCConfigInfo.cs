using March7thHoney.Enums.Rogue;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace March7thHoney.Data.Config.Rogue;

public class RogueNPCConfigInfo
{
    [JsonConverter(typeof(StringEnumConverter))]
    public RogueDialogueTypeEnum DialogueType { get; set; }

    public List<RogueNPCDialogueConfigInfo> DialogueList { get; set; } = [];

    public void Loaded()
    {
        if (DialogueList.Count == 0) return;

        foreach (var info in DialogueList) info.Loaded();
    }
}