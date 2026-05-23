namespace March7thHoney.Internationalization.Message;

#region Root

public class LanguageCHT
{
    public GameTextCHT Game { get; } = new();
    public ServerTextCHT Server { get; } = new();
    public WordTextCHT Word { get; } = new(); 
}

#endregion

#region Layer 1




public class GameTextCHT
{
    public CommandTextCHT Command { get; } = new();
}




public class ServerTextCHT
{
    public WebTextCHT Web { get; } = new();
    public ServerInfoTextCHT ServerInfo { get; } = new();
}




public class WordTextCHT
{
    public string Rank => "星魂";
    public string Avatar => "角色";
    public string Material => "材料";
    public string Pet => "寵物";
    public string Relic => "遺器";
    public string Equipment => "光錐";
    public string Talent => "行跡";
    public string Banner => "卡池";
    public string VideoKeys => "遊戲CG密鑰";
    public string Activity => "活動";
    public string Buff => "祝福";
    public string Miracle => "奇物";
    public string Unlock => "奢侈品";
    public string TrainParty => "列車派對物品";

    
    public string Config => "配置文件";
    public string Language => "語言";
    public string Log => "日誌";
    public string GameData => "遊戲數據";
    public string Cache => "資源緩存";
    public string Database => "數據庫";
    public string Command => "命令";
    public string WebServer => "Web服務器";
    public string Plugin => "插件";
    public string Handler => "包處理器";
    public string Dispatch => "全局分發";
    public string Game => "遊戲";
    public string Handbook => "手冊";
    public string NotFound => "未找到";
    public string Error => "錯誤";
    public string FloorInfo => "區域文件";
    public string FloorGroupInfo => "區域組文件";
    public string FloorMissingResult => "傳送與世界生成";
    public string FloorGroupMissingResult => "傳送、怪物戰鬥與世界生成";
    public string Mission => "任務";
    public string MissionInfo => "任務文件";
    public string SubMission => "子任務";
    public string SubMissionInfo => "子任務文件";
    public string MazeSkill => "角色秘技";
    public string MazeSkillInfo => "角色秘技文件";
    public string Dialogue => "模擬宇宙事件";
    public string DialogueInfo => "模擬宇宙事件文件";
    public string Performance => "劇情操作";
    public string PerformanceInfo => "劇情操作文件";
    public string RogueChestMap => "模擬宇宙地圖";
    public string RogueChestMapInfo => "模擬宇宙地圖文件";
    public string ChessRogueRoom => "模擬宇宙DLC";
    public string ChessRogueRoomInfo => "模擬宇宙DLC文件";
    public string SummonUnit => "秘技生成";
    public string SummonUnitInfo => "秘技生成文件";
    public string RogueTournRoom => "差分宇宙";
    public string RogueTournRoomInfo => "差分宇宙房間文件";
    public string TypesOfRogue => "類型的模擬宇宙";
    public string RogueMagicRoom => "不可知域";
    public string RogueMagicRoomInfo => "不可知域房間文件";
    public string RogueDiceSurface => "骰面效果";
    public string RogueDiceSurfaceInfo => "骰面效果文件";
    public string AdventureModifier => "AdventureModifier";
    public string AdventureModifierInfo => "AdventureModifier文件";

    public string DatabaseAccount => "數據庫賬號";
    public string Tutorial => "教程";
}

#endregion

#region Layer 2

#region GameText




public class CommandTextCHT
{
    public NoticeTextCHT Notice { get; } = new();

    public HeroTextCHT Hero { get; } = new();
    public AvatarTextCHT Avatar { get; } = new();
    public GiveTextCHT Give { get; } = new();
    public GiveAllTextCHT GiveAll { get; } = new();
    public LineupTextCHT Lineup { get; } = new();
    public HelpTextCHT Help { get; } = new();
    public KickTextCHT Kick { get; } = new();
    public MissionTextCHT Mission { get; } = new();
    public RelicTextCHT Relic { get; } = new();
    public ReloadTextCHT Reload { get; } = new();
    public RogueTextCHT Rogue { get; } = new();
    public SceneTextCHT Scene { get; } = new();
    public UnlockAllTextCHT UnlockAll { get; } = new();
    public MailTextCHT Mail { get; } = new();
    public RaidTextCHT Raid { get; } = new();
    public AccountTextCHT Account { get; } = new();
    public UnstuckTextCHT Unstuck { get; } = new();
    public SetlevelTextCHT Setlevel { get; } = new();
    public GridTextCHT Grid { get; } = new();
    public JsonTextCHT Json { get; } = new();
    public AnnoTextCHT Anno { get; } = new();
    public CalyxTextCHT Calyx { get; } = new();
    public ClearTextCHT Clear { get; } = new();
    public DebugTextCHT Debug { get; } = new();
    public RemoveTextCHT Remove { get; } = new();
    public WindyTextCHT Windy { get; } = new();
}

#endregion

#region ServerText




public class WebTextCHT
{
    public string Maintain => "服務器正在維修，請稍後嘗試。";
}




public class ServerInfoTextCHT
{
    public string Shutdown => "關閉中…";
    public string CancelKeyPressed => "已按下取消鍵 (Ctrl + C)，服務器即將關閉…";
    public string StartingServer => "正在啟動 March7thHoney…";
    public string CurrentVersion => "當前服務端支持的版本: {0}";
    public string LoadingItem => "正在加載 {0}…";
    public string GeneratingItem => "正在生成 {0}…";
    public string WaitingItem => "正在等待進程 {0} 完成…";
    public string RegisterItem => "註冊了 {0} 個 {1}。";
    public string FailedToLoadItem => "加載 {0} 失敗。";
    public string NewClientSecretKey => "客戶端密鑰不存在，正在生成新的客戶端密鑰。";
    public string FailedToInitializeItem => "初始化 {0} 失敗。";
    public string FailedToReadItem => "讀取 {0} 失敗，文件{1}";
    public string GeneratedItem => "已生成 {0}。";
    public string UpdatedItem => "已更新 {0}。";
    public string LoadedItem => "已加載 {0}。";
    public string LoadedItems => "已加載 {0} 個 {1}。";
    public string ServerRunning => "{0} 服務器正在監聽 {1}";
    public string ServerStarted => "啟動完成！用時 {0}s，輸入 ‘help’ 來獲取命令幫助"; 
    public string MissionEnabled => "任務系統已啟用，此功能仍在開發中，且可能不會按預期工作，如果遇見任何bug，請彙報給開發者。";
    public string CacheLoadSkip => "已跳過緩存加載。";

    public string ConfigMissing => "{0} 缺失，請檢查你的資源文件夾：{1}，{2} 可能不能使用。";
    public string PartialConfigMissing => "{0} 部分缺失，缺失數量: {1}，示例路徑: {2}";
    public string UnloadedItems => "卸載了所有 {0}。";
    public string SaveDatabase => "已保存數據庫，用時 {0}s";
    public string WaitForAllDone => "現在還不可以進入遊戲，請等待所有項目加載完成後再試";

    public string UnhandledException => "發生未經處理的異常：{0}";
    public string LicenseFileNotFound => "未找到授權檔：{0}";
    public string LicenseInvalid => "授權無效，請前往 {0} 取得授權";
    public string LicenseParseFailed => "授權解析失敗。";
    public string LicenseExpired => "授權已過期。";
    public string LicenseHwidMismatch => "授權 HWID 不匹配。";
    public string LicenseCurrentHwid => "當前機器 HWID：{0}";
    public string LicenseVerified => "授權驗證通過：{0}，到期時間：{1}";
    public string LicenseValidateFailed => "授權驗證失敗。";
    public string PublicModeTrialActive => "授權文件缺失，但當前時間在試用期內，試用期剩餘 {0} 天。";
    public string PublicModeBuildTimeUnavailable => "試用模式不可用。";
    public string PublicModeEnabled => "伺服器為公開版本，正在啟用額外的安全措施。";
    public string PublicModeValidationFailed => "許可證驗證失敗，程序加載已停止。";
    public string HandbookGeneratedAt => "手冊生成時間 {0}";
    public string HandbookSectionCommand => "命令";
    public string HandbookSectionAvatar => "角色";
    public string HandbookSectionItem => "物品";
    public string HandbookSectionStageId => "關卡ID";
    public string HandbookSectionMainMission => "主任務";
    public string HandbookSectionSubMission => "子任務";
    public string HandbookSectionRogueBuff => "模擬宇宙祝福";
    public string HandbookSectionRogueMiracle => "模擬宇宙奇物";
    public string HandbookSectionCurrencyWarRole => "貨幣戰爭角色";
    public string HandbookSectionCurrencyWarEquipment => "貨幣戰爭裝備";
    public string HandbookSectionCurrencyWarConsumable => "貨幣戰爭消耗品";
    public string HandbookSectionCurrencyWarOrb => "貨幣戰爭晶礦";
    public string HandbookSectionRogueDiceSurface => "骰面效果";
    public string HandbookSectionRogueDialogue => "模擬宇宙事件對話";
    public string HandbookDescriptionLabel => "描述";
    public string HandbookUsageLabel => "用法";
    public string HandbookAliasesLabel => "別名";
    public string HandbookPermissionLabel => "權限";
    public string HandbookCommandNotesTitle => "命令使用說明：";
    public string HandbookCommandNoteTarget => "可在命令前後加 @<UID> 指定目標玩家。";
    public string HandbookCommandNoteQuote => "包含空格的參數請用引號包裹，例如 \"Sender Name\"。";
    public string HandbookCommandNoteCaseInsensitive => "子命令不區分大小寫。";
    public string HandbookCommandNoteRequiredOptional => "<value> 為必填，[value] 為可選。";
    public string HandbookCommandNoteOptionStyle => "x<count>、l<level>、r<rank> 為選項式參數。";
    public string HandbookCommandNoteLongOptionAliases => "支持長選項別名：--count、--amount、--level、--rank、--eidolon、--superimposition。";
}

#endregion

#endregion

#region Layer 3

#region CommandText




public class NoticeTextCHT
{
    public string PlayerNotFound => "未找到玩家!";
    public string InvalidArguments => "無效的參數!";
    public string NoPermission => "你沒有權限這麼做!";
    public string CommandNotFound => "未找到命令! 輸入 '/help' 來獲取幫助";
    public string TargetOffline => "目標 {0}({1}) 離線了！清除當前目標";
    public string TargetFound => "找到目標 {0}({1})，下一次命令將默認對其執行";
    public string TargetNotFound => "未找到目標 {0}!";
    public string InternalError => "在處理命令時發生了內部錯誤!";
}




public class HeroTextCHT
{
    public string Desc =>
        "切換主角的性別/形態\n當切換性別時，genderId為1代表男性，2代表女性\n當切換形態時，8001代表毀滅命途，8003代表存護命途，8005代表同諧命途。\n注意，切換性別時會清空所有可選命途以及行跡，為不可逆操作！";

    public string Usage => "用法：/hero gender <genderId: 1=男, 2=女>\n\n用法：/hero type <typeId: 8001|8003|8005|8007|8009>";
    public string GenderNotSpecified => "性別不存在!";
    public string HeroTypeNotSpecified => "主角類型不存在!";
    public string GenderChanged => "性別已更改!";
    public string HeroTypeChanged => "主角類型已更改!";
}




public class UnlockAllTextCHT
{
    public string Desc =>
        "解鎖所有在類別內的對象\n" +
        "使用 /unlockall mission 以完成所有任務，使用後會被踢出，重新登錄後可能會被教程卡住，請謹慎使用\n" +
        "使用 /unlockall tutorial 以解鎖所有教程，使用後會被踢出，用於部分界面卡住無法行動的情況\n" +
        "使用 /unlockall rogue 以解鎖所有類型模擬宇宙，使用後會被踢出，建議與 /unlockall tutorial 搭配使用以獲取更好效果";

    public string Usage => "用法：/unlockall <mission|quest|tutorial|rogue|challenge|grid>";
    public string UnlockedAll => "已解鎖/完成所有{0}!";
}




public class AvatarTextCHT
{
    public string Desc => "設定玩家已有角色的屬性\n設置行跡等級時，設置X級即設置所有行跡節點至X級，若大於此節點允許的最高等級，設置為最高等級\n注意：-1意為所有已擁有角色";

    public string Usage =>
        "用法：/avatar talent <角色ID|-1> <行跡等級 0-10>\n\n用法：/avatar get <角色ID>\n\n用法：/avatar rank <角色ID|-1> <星魂 0-6>\n\n用法：/avatar level <角色ID|-1> <角色等級 1-80>\n\n用法：/avatar path <角色ID> <命途ID>";

    public string InvalidLevel => "{0}等級無效";
    public string AllAvatarsLevelSet => "已將全部角色 {0}等級設置為 {1}";
    public string AvatarLevelSet => "已將 {0} 角色 {1}等級設置為 {2}";
    public string AvatarNotFound => "角色不存在!";
    public string AvatarGet => "獲取到角色 {0}!";
    public string AvatarFailedGet => "獲取角色 {0} 失敗!";
}




public class GiveTextCHT
{
    public string Desc => "給予玩家物品，此處可輸入角色ID，但無法設置行跡、等級及星魂";
    public string Usage => "用法：/give <物品ID> [x<數量=1>] [l<等級=1, 1-80>] [r<疊影=1, 1-5>]";
    public string ItemNotFound => "未找到物品!";
    public string GiveItem => "給予 @{0} {1} 個物品 {2}";
}




public class GiveAllTextCHT
{
    public string Desc =>
        "給予玩家全部指定類型的物品\navatar意為角色，equipment意為光錐，material意為可堆疊材料，relic意為遺器，unlock意為氣泡、手機壁紙、頭像，train意為開拓者房間內容，pet意為寵物，path意為多命途角色命途";

    public string Usage =>
        "用法：/giveall avatar r<星魂> l<等級>\n\n" +
        "用法：/giveall equipment|lightcone r<疊影> l<等級> x<數量>\n\n" +
        "用法：/giveall material [x<數量=1>]\n\n" +
        "用法：/giveall relic l<等級> x<數量>\n\n" +
        "用法：/giveall unlock\n\n" +
        "用法：/giveall train\n\n" +
        "用法：/giveall pet [x<數量=1>]\n\n" +
        "用法：/giveall path";

    public string GiveAllItems => "已給予所有 {0}, 各 {1} 個";
}




public class LineupTextCHT
{
    public string Desc => "管理玩家的隊伍\n秘技點一次性只能獲得兩個";
    public string Usage => "用法：/lineup mp [數量=2]\n\n用法：/lineup heal";
    public string PlayerGainedMp => "玩家已獲得 {0} 秘技點";
    public string HealedAllAvatars => "成功治癒當前隊伍中的所有角色";
}




public class HelpTextCHT
{
    public string Desc => "顯示幫助信息。控制台命令可使用 @<UID> 指定在線目標。";
    public string Usage => "用法：/help\n\n用法：/help [命令]\n\n目標：@<UID> <命令> ... 或 <命令> ... @<UID>";
    public string Commands => "命令:";
    public string CommandPermission => "所需權限: ";
    public string CommandAlias => "命令別名：";
    public string CommandNotFound => "未找到命令! 輸入 '/help' 來獲取幫助";
}




public class KickTextCHT
{
    public string Desc => "踢出玩家";
    public string Usage => "用法：/kick";
    public string PlayerKicked => "玩家 {0} 已被踢出!";
}




public class MissionTextCHT
{
    public string Desc =>
        "管理玩家的任務\n" +
        "使用 pass 完成當前正在進行的所有任務，此命令易造成嚴重卡頓，請儘量使用 /mission finish 替代\n" +
        "使用 finish [子任務ID] 完成指定子任務，請瀏覽 handbook 來獲取子任務ID\n" +
        "使用 finishmain [主任務ID] 完成指定主任務，請瀏覽 handbook 來獲取主任務ID\n" +
        "使用 running <-all> 獲取正在追蹤的任務，增加'-all'則顯示所有正在進行的任務以及可能卡住的任務，使用後可能會出現較長任務列表，請注意甄別\n" +
        "使用 reaccept [主任務ID] 可重新進行指定主任務，請瀏覽 handbook 來獲取主任務ID";

    public string Usage =>
        "用法：/mission pass\n\n用法：/mission finish [子任務ID]\n\n用法：/mission running <-all>\n\n用法：/mission reaccept [主任務ID]\n\n用法：/mission finishmain [主任務ID]";

    public string AllMissionsFinished => "所有任務已完成!";
    public string AllRunningMissionsFinished => "共 {0} 個進行中的任務已完成!";
    public string MissionFinished => "任務 {0} 已完成!";
    public string InvalidMissionId => "無效的任務ID!";
    public string NoRunningMissions => "沒有正在進行的任務!";

    public string RunningMissions => "正在進行的任務:";
    public string PossibleStuckMissions => "可能卡住的任務:";
    public string MainMission => "主任務";

    public string MissionReAccepted => "重新接受任務 {0}!";
}




public class RelicTextCHT
{
    public string Desc => "管理玩家的遺器\n主詞條可選，副詞條可選，但至少存在其中之一\n等級限制：1≤等級≤9999";

    public string Usage =>
        "用法：/relic <遺器ID> <主詞條ID> <小詞條ID1:小詞條等級> <小詞條ID2:小詞條等級> <小詞條ID3:小詞條等級> <小詞條ID4:小詞條等級> l<等級> x<數量>";

    public string RelicNotFound => "遺器不存在!";
    public string InvalidMainAffixId => "主詞條ID無效";
    public string InvalidSubAffixId => "副詞條ID無效";
    public string RelicGiven => "給予玩家 @{0} {1} 個遺器 {2}, 主詞條 {3}";
}




public class ReloadTextCHT
{
    public string Desc => "重新加載指定的配置\n配置名：banner, activity, videokey, plugin";
    public string Usage => "用法：/reload <banner|activity|videokey|plugin>";
    public string ConfigReloaded => "配置 {0} 已重新加載!";
}




public class RogueTextCHT
{
    public string Desc => "管理玩家模擬宇宙中的數據\n-1意為所有祝福（已擁有祝福）\n使用 buff 來獲取祝福\n使用 enhance 來強化祝福";

    public string Usage =>
        "用法：/rogue money [宇宙碎片數量]\n\n用法：/rogue buff [祝福ID/-1]\n\n用法：/rogue miracle [奇物ID]\n\n用法：/rogue enhance [祝福ID/-1]\n\n用法：/rogue unstuck - 脫離事件";

    public string PlayerGainedMoney => "玩家已獲得 {0} 宇宙碎片";
    public string PlayerGainedAllItems => "玩家已獲得所有{0}";
    public string PlayerGainedItem => "玩家已獲得{0} {1}";
    public string PlayerEnhancedBuff => "玩家已強化祝福 {0}";
    public string PlayerEnhancedAllBuffs => "玩家已強化所有祝福";
    public string PlayerUnstuck => "玩家已脫離事件";
    public string NotFoundItem => "未找到 {0}!";
    public string PlayerNotInRogue => "玩家不在模擬宇宙中!";
}




public class SceneTextCHT
{
    public string Desc =>
        "管理玩家場景\n" +
        "提示：此組大多為調試使用，使用命令前，請確保你清楚你在做什麼！\n" +
        "使用 prop 來設置道具狀態，在Common/Enums/Scene/PropStateEnum.cs獲取狀態列表\n" +
        "使用 unlockall 來解鎖場景內所有道具（即將所有能設置為open狀態的道具設置為open狀態），此命令有較大可能會導致遊戲加載卡條約90%，使用 /scene reset <floorId> 來解決問題\n" +
        "使用 change 來進入指定場景，要獲取EntryId，請訪問 Resources/MapEntrance.json\n" +
        "使用 reload 來重新加載當前場景，並回到初始位置\n" +
        "使用 reset 來重置指定場景所有道具狀態，要獲取當前FloorId，請使用 /scene cur";

    public string Usage =>
        "用法：/scene group\n\n用法：/scene prop <組ID> <道具ID> <狀態>\n\n用法：/scene remove <實體ID>\n\n用法：/scene unlockall\n\n用法：/scene unlockallgroup\n\n用法：/scene change <entryId>\n\n用法：/scene reload\n\n用法：/scene reset <floorId>\n\n用法：/scene fsv <名稱> <值>\n\n用法：/scene gp <組ID> <名稱> <值>\n\n用法：/scene cur\n\n用法：/scene near\n\n用法：/scene forward <距離>";

    public string LoadedGroups => "已加載組: {0}";
    public string PropStateChanged => "道具: {0} 的狀態已設置為 {1}";
    public string PropNotFound => "未找到道具!";
    public string EntityRemoved => "實體 {0} 已被移除";
    public string EntityNotFound => "未找到實體!";
    public string AllPropsUnlocked => "所有道具已解鎖!";
    public string SceneChanged => "已進入場景 {0}";
    public string SceneReloaded => "場景已重新加載!";
    public string SceneReset => "已重置場景 {0} 中所有道具狀態！";
    public string CurrentScene => "當前場景Entry Id: {0}, Plane Id: {1}, Floor Id: {2}";
    public string FSVSet => "已將 {0} 設置為 {1}";
    public string Teleported => "已傳送！";
}




public class MailTextCHT
{
    public string Desc => "管理玩家的郵件";
    public string Usage => "用法：/mail <發送者> <模板ID> <過期天數> _TITLE <標題> _CONTENT <內容> _ATTACH <物品ID> [x<數量>] [r<星魂/疊影>] [s<疊影>] [l<等級>] [p<晉階>] [t<行跡等級|max>] ...\n用法：/mail all <發送者> <模板ID> <過期天數> _TITLE <標題> _CONTENT <內容> _ATTACH ...\n用法：/mail welcome [all]";
    public string MailSent => "郵件已發送!";
    public string MailSentWithAttachment => "帶附件的郵件已發送!";
}




public class RaidTextCHT
{
    public string Desc => "管理玩家的任務臨時場景";
    public string Usage => "用法：/raid leave\n\n用法：/raid reset <RaidID> <Level>";
    public string Leaved => "已離開臨時場景!";
}




public class AccountTextCHT
{
    public string Desc => "管理賬號\n注意：此命令未經測試，請謹慎使用！";
    public string Usage => "用法：/account create <用戶名> <郵箱> [密碼] [UID]\n用法：/account create <用戶名> <郵箱> [--password <密碼>] [--uid <UID>]\n用法：/account setpassword <用戶名|UID|郵箱> <密碼>\n用法：/account setemail <用戶名|UID|郵箱> <郵箱> [verified:true|false]\n用法：/account verifyemail <用戶名|UID|郵箱>\n用法：/account ban <用戶名|UID|郵箱> <permanent|duration> [原因]\n用法：/account unban <用戶名|UID|郵箱>\n用法：/account baninfo <用戶名|UID|郵箱>\n用法：/account reset [用戶名|UID|郵箱]\n用法：/account delete <用戶名|UID|郵箱> confirm\n用法：/account remove <用戶名|UID|郵箱> confirm\n用法：/account role <用戶名|UID|郵箱> <角色>\n用法：/account setrole <用戶名|UID|郵箱> <角色>\n用法：/account roles";
    public string SetPasswordUsage => "用法：/account setpassword <用戶名|UID|郵箱> <密碼>";
    public string SetEmailUsage => "用法：/account setemail [用戶名|UID|郵箱] [郵箱] [verified:true|false]";
    public string VerifyEmailUsage => "用法：/account verifyemail [用戶名|UID|郵箱]";
    public string BanUsage => "用法：/account ban [用戶名|UID|郵箱] [permanent|duration] [原因]";
    public string BanDurationExamples => "時長示例：30m、2h、7d、1w。";
    public string UnbanUsage => "用法：/account unban [用戶名|UID|郵箱]";
    public string BanInfoUsage => "用法：/account baninfo [用戶名|UID|郵箱]";
    public string RoleUsage => "用法：/account role <用戶名|UID|郵箱> <角色>";
    public string ResetUsage => "用法：/account reset [用戶名|UID|郵箱]";
    public string AccountNotFound => "賬號 {0} 不存在。";
    public string ResetStarted => "正在重置賬號 {0}。你將被斷開連接，重新登入後會重新開始。";
    public string ResetSuccess => "賬號 {0} 已重置。";
    public string DeleteUsage => "用法：/account delete|remove <用戶名|UID|郵箱> confirm";
    public string DeleteRequiresConfirm => "這會永久刪除賬號 {0} 及其所有玩家資料。請執行 /account delete {0} confirm 繼續。";
    public string DeleteStarted => "正在刪除賬號 {0}。你將被斷開連接。";
    public string DeleteSuccess => "賬號 {0} 已刪除。";
    public string InvalidUid => "無效UID參數！";
    public string CreateError => "出現內部錯誤 {0} ";
    public string CreateSuccess => "新賬號 {0} 創建成功!";
    public string DuplicateAccount => "賬號 {0} 已存在!";
    public string DuplicateUID => "UID {0} 已存在!";
    public string DataError => "新賬號獲取失敗! {0}!";
}




public class UnstuckTextCHT
{
    public string Desc => "將離線玩家傳送回默認場景。面向控制台/管理員使用，目標必須離線。";
    public string Usage => "用法：/unstuck <離線UID>";
    public string UnstuckSuccess => "已成功將該玩家傳送回默認場景";
    public string UidNotExist => "該UID不存在！";
    public string PlayerIsOnline => "該玩家目前在線上！";
}




public class SetlevelTextCHT
{
    public string Desc => "設定玩家等級";
    public string Usage => "用法：/setlevel <等級 1-70>";
    public string SetlevelSuccess => "等級設定成功！";
}




public class GridTextCHT
{
    public string Desc => "管理貨幣戰爭內容，注意，此部分內容尚未完善，若有問題請及時反饋。\n此命令不一定會檢查ID是否存在，若無效果，請檢查參數是否正確";
    public string Usage => "用法：/grid gold [金幣數量]\n\n" +
                           "用法：/grid role [角色ID] [角色星級]\n\n" +
                           "用法：/grid equip [裝備ID]\n\n" +
                           "用法：/grid consumable [消耗品ID]\n\n" +
                           "用法：/grid orb [晶礦ID]\n\n" +
                           "用法：/grid section [章節ID] [關卡ID]";
    public string NotInGame => "不在貨幣戰爭中！";
    public string InvalidRole => "角色ID或星級不存在！";
    public string InvalidEquipment => "裝備ID不存在！";
    public string InvalidOrb => "晶礦ID不存在！";
    public string InvalidConsumable => "消耗品ID不存在！";
    public string AddedRole => "已添加角色。";
    public string UpdateGold => "獲得 {0} 個金幣。";
    public string AddEquipment => "已添加 {0} 裝備。";
    public string AddOrb => "已添加 {0} 晶礦。";
    public string AddConsumable => "已添加 {0} 消耗品。";
    public string EnterSection => "進入 {0}-{1}。";
}




public class JsonTextCHT
{
    public string Desc => "從 JSON 導入角色、遺器、光錐數據，或清空現有遺器和光錐。";
    public string Usage => "用法：/json\n\n用法：/json <文件名|序號|路徑>\n\n用法：/json clear\n\n用法：/json export [name] [format=true|false]";
    public string ClearInventory => "已清空當前玩家的遺器和光錐。";
    public string FileNotFound => "未找到文件：{0}";
    public string ReadOrParseFailed => "讀取或解析 JSON 失敗：{0}";
    public string InvalidJsonContent => "JSON 內容無效或為空。";
    public string ImportSummary => "導入完成：{0}，角色 {1}，遺器 {2}，光錐 {3}。";
    public string NoFileFoundWithHint => "未找到可導入的 JSON 文件，請先將文件放到 Config/Json。";
    public string InvalidChoice => "無效序號，可選範圍：1 ~ {0}";
    public string NoFileFound => "未找到可導入的 JSON 文件。";
    public string SearchedDirectories => "已搜索目錄：";
    public string SearchedDirectoryItem => "- {0}";
    public string FoundFiles => "找到以下 JSON 文件：";
    public string FileListItem => "{0}. {1}";
    public string UsageSelectHint => "輸入 /json [序號] 或 /json [文件名] 進行導入。";
    public string AvatarExcelNotFound => "未找到角色配置：{0}";

    public string ExportSuccess => "匯出成功，檔案：{0}";
}




public class AnnoTextCHT
{
    public string Desc => "發送伺服器彈窗公告。";
    public string Usage =>
        "用法：/anno [有效時長] [顯示間隔] <公告內容>\n\n" +
        "示例：\n" +
        "  /anno 10m 伺服器即將重啟\n" +
        "  /anno 2h 5m 雙倍掉落已開啟\n" +
        "  /anno @10001 10m 單人通知\n\n" +
        "選項：\n" +
        "  --duration <時間>    公告有效時長，例如：30s、10m、2h、3d\n" +
        "  --frequency <時間>   橫幅再次顯示間隔，例如：30s、10m、1h";
    public string Disabled => "公告功能未啟用，請先在配置中開啟 ServerOption.ServerAnnounce.EnableAnnounce。";
    public string InvalidDuration => "公告有效時長無效，請使用類似 30s、10m、2h 或 3d 的格式。";
    public string InvalidFrequency => "公告顯示間隔無效，請使用類似 30s、10m、2h 或 3d 的格式。";
    public string Sent => "已向 {0} 名在線玩家發送彈窗消息。有效時長：{1}。顯示間隔：{2}。";
}




public class CalyxTextCHT
{
    public string Desc => "切換花萼覆蓋模式：開啟時將擬造花萼替換為 freesr 文件中的關卡內容。";
    public string Usage => "用法：/calyx on\n用法：/calyx off";
    public string ToggleOn => "花萼覆蓋已開啟。";
    public string ToggleOff => "花萼覆蓋已關閉。";
    public string MissingIndex => "請指定 freesr 文件序號。用法：/calyx on [序號]";
    public string InvalidMode => "未知模式 '{0}'。請使用 /calyx on 或 /calyx off。";
    public string LoadFailed => "加載 freesr 文件失敗。";
    public string NoCachedData => "未找到已緩存的 freesr 數據。請先使用 /json <檔名> 或通過 srtools 網站同步導入。";
}




public class ClearTextCHT
{
    public string Desc => "從目標玩家背包中移除未裝備的光錐或遺器。";
    public string Usage => "用法：/clear equipment\n\n用法：/clear relic";
    public string ClearEquipment => "已移除 {0} 個未裝備光錐。";
    public string ClearRelic => "已移除 {0} 個未裝備遺器。";
}




public class DebugTextCHT
{
    public string Desc => "調試戰鬥配置和自定義包加載。";
    public string Usage => "用法：/debug specific <StageID>\n\n用法：/debug monster <MonsterID>\n\n用法：/debug customP <PacketJsonPath>";
    public string InvalidStageId => "無效的關卡或怪物ID。";
    public string SetStageId => "調試戰鬥覆蓋已更新。";
    public string CustomPacketFileNotFound => "未找到自定義包文件。";
    public string CustomPacketFileInvalid => "自定義包文件無效或為空。";
    public string CustomPacketFileLoaded => "自定義包隊列已加載。";
}




public class RemoveTextCHT
{
    public string Desc => "管理員專用：移除已擁有角色或光錐。";
    public string Usage => "用法：/remove character <角色ID> confirm\n\n用法：/remove avatar <角色ID> confirm\n\n用法：/remove lightcone <唯一ID|物品ID> confirm\n\n用法：/remove equipment <唯一ID|物品ID> confirm\n\n添加 --dry-run 可預覽將移除的內容。";
    public string RequiresConfirm => "這是破壞性操作。添加 confirm 執行，或使用 --dry-run 預覽。";
    public string NotFound => "未找到 {0} {1}。";
    public string DryRun => "預覽：將移除 {0} 個 {1}。";
    public string Success => "已移除 {0} 個 {1}。客戶端可能需要重新登入才能完全刷新。";
}




public class WindyTextCHT
{
    public string Desc => "加載 Lua 字節碼並發送給目標玩家。";
    public string Usage => "用法：/windy <相對於 Lua 文件夾的 lua 路徑>";
    public string Loaded => "已從 Lua 腳本讀取 BYTECODE：{0}";
    public string ReadError => "讀取 Lua 腳本失敗：{0}";
}

#endregion

#endregion

