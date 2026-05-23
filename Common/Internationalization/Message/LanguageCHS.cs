namespace March7thHoney.Internationalization.Message;

#region Root

public class LanguageCHS
{
    public GameTextCHS Game { get; } = new();
    public ServerTextCHS Server { get; } = new();
    public WordTextCHS Word { get; } = new(); 
}

#endregion

#region Layer 1




public class GameTextCHS
{
    public CommandTextCHS Command { get; } = new();
}




public class ServerTextCHS
{
    public WebTextCHS Web { get; } = new();
    public ServerInfoTextCHS ServerInfo { get; } = new();
}




public class WordTextCHS
{
    public string Rank => "星魂";
    public string Avatar => "角色";
    public string Material => "材料";
    public string Pet => "宠物";
    public string Relic => "遗器";
    public string Equipment => "光锥";
    public string Talent => "行迹";
    public string Banner => "卡池";
    public string VideoKeys => "游戏CG密钥";
    public string Activity => "活动";
    public string Buff => "祝福";
    public string Miracle => "奇物";
    public string Unlock => "奢侈品";
    public string TrainParty => "列车派对物品";

    
    public string Config => "配置文件";
    public string Language => "语言";
    public string Log => "日志";
    public string GameData => "游戏数据";
    public string Cache => "资源缓存";
    public string Database => "数据库";
    public string Command => "命令";
    public string WebServer => "Web服务器";
    public string Plugin => "插件";
    public string Handler => "包处理器";
    public string Dispatch => "全局分发";
    public string Game => "游戏";
    public string Handbook => "手册";
    public string NotFound => "未找到";
    public string Error => "错误";
    public string FloorInfo => "区域文件";
    public string FloorGroupInfo => "区域组文件";
    public string FloorMissingResult => "传送与世界生成";
    public string FloorGroupMissingResult => "传送、怪物战斗与世界生成";
    public string Mission => "任务";
    public string MissionInfo => "任务文件";
    public string SubMission => "子任务";
    public string SubMissionInfo => "子任务文件";
    public string MazeSkill => "角色秘技";
    public string MazeSkillInfo => "角色秘技文件";
    public string Dialogue => "模拟宇宙事件";
    public string DialogueInfo => "模拟宇宙事件文件";
    public string Performance => "剧情操作";
    public string PerformanceInfo => "剧情操作文件";
    public string RogueChestMap => "模拟宇宙地图";
    public string RogueChestMapInfo => "模拟宇宙地图文件";
    public string ChessRogueRoom => "模拟宇宙DLC";
    public string ChessRogueRoomInfo => "模拟宇宙DLC文件";
    public string SummonUnit => "秘技生成";
    public string SummonUnitInfo => "秘技生成文件";
    public string RogueTournRoom => "差分宇宙";
    public string RogueTournRoomInfo => "差分宇宙房间文件";
    public string TypesOfRogue => "类型的模拟宇宙";
    public string RogueMagicRoom => "不可知域";
    public string RogueMagicRoomInfo => "不可知域房间文件";
    public string RogueDiceSurface => "骰面效果";
    public string RogueDiceSurfaceInfo => "骰面效果文件";
    public string AdventureModifier => "AdventureModifier";
    public string AdventureModifierInfo => "AdventureModifier文件";

    public string DatabaseAccount => "数据库账号";
    public string Tutorial => "教程";
}

#endregion

#region Layer 2

#region GameText




public class CommandTextCHS
{
    public NoticeTextCHS Notice { get; } = new();

    public HeroTextCHS Hero { get; } = new();
    public AvatarTextCHS Avatar { get; } = new();
    public GiveTextCHS Give { get; } = new();
    public GiveAllTextCHS GiveAll { get; } = new();
    public LineupTextCHS Lineup { get; } = new();
    public HelpTextCHS Help { get; } = new();
    public KickTextCHS Kick { get; } = new();
    public MissionTextCHS Mission { get; } = new();
    public RelicTextCHS Relic { get; } = new();
    public ReloadTextCHS Reload { get; } = new();
    public RogueTextCHS Rogue { get; } = new();
    public SceneTextCHS Scene { get; } = new();
    public UnlockAllTextCHS UnlockAll { get; } = new();
    public MailTextCHS Mail { get; } = new();
    public RaidTextCHS Raid { get; } = new();
    public AccountTextCHS Account { get; } = new();
    public UnstuckTextCHS Unstuck { get; } = new();
    public SetlevelTextCHS Setlevel { get; } = new();
    public GridTextCHS Grid { get; } = new();
    public JsonTextCHS Json { get; } = new();
    public AnnoTextCHS Anno { get; } = new();
    public CalyxTextCHS Calyx { get; } = new();
    public ClearTextCHS Clear { get; } = new();
    public DebugTextCHS Debug { get; } = new();
    public RemoveTextCHS Remove { get; } = new();
    public WindyTextCHS Windy { get; } = new();
}

#endregion

#region ServerText




public class WebTextCHS
{
    public string Maintain => "服务器正在维修，请稍后尝试。";
}




public class ServerInfoTextCHS
{
    public string Shutdown => "关闭中…";
    public string CancelKeyPressed => "已按下取消键 (Ctrl + C)，服务器即将关闭…";
    public string StartingServer => "正在启动 March7thHoney…";
    public string CurrentVersion => "当前服务端支持的版本: {0}";
    public string LoadingItem => "正在加载 {0}…";
    public string GeneratingItem => "正在生成 {0}…";
    public string WaitingItem => "正在等待进程 {0} 完成…";
    public string RegisterItem => "注册了 {0} 个 {1}。";
    public string FailedToLoadItem => "加载 {0} 失败。";
    public string NewClientSecretKey => "客户端密钥不存在，正在生成新的客户端密钥。";
    public string FailedToInitializeItem => "初始化 {0} 失败。";
    public string FailedToReadItem => "读取 {0} 失败，文件{1}";
    public string GeneratedItem => "已生成 {0}。";
    public string UpdatedItem => "已更新 {0}。";
    public string LoadedItem => "已加载 {0}。";
    public string LoadedItems => "已加载 {0} 个 {1}。";
    public string ServerRunning => "{0} 服务器正在监听 {1}";
    public string ServerStarted => "启动完成！用时 {0}s，输入 ‘help’ 来获取命令帮助"; 
    public string MissionEnabled => "任务系统已启用，此功能仍在开发中，且可能不会按预期工作，如果遇见任何bug，请汇报给开发者。";
    public string CacheLoadSkip => "已跳过缓存加载。";

    public string ConfigMissing => "{0} 缺失，请检查你的资源文件夹：{1}，{2} 可能不能使用。";
    public string PartialConfigMissing => "{0} 部分缺失，缺失数量: {1}，示例路径: {2}";
    public string UnloadedItems => "卸载了所有 {0}。";
    public string SaveDatabase => "已保存数据库，用时 {0}s";
    public string WaitForAllDone => "现在还不可以进入游戏，请等待所有项目加载完成后再试";

    public string UnhandledException => "发生未经处理的异常：{0}";
    public string LicenseFileNotFound => "未找到许可证文件：{0}";
    public string LicenseInvalid => "许可证无效，请前往 {0} 获取授权";
    public string LicenseParseFailed => "许可证解析失败。";
    public string LicenseExpired => "许可证已过期。";
    public string LicenseHwidMismatch => "许可证 HWID 不匹配。";
    public string LicenseCurrentHwid => "当前机器 HWID：{0}";
    public string LicenseVerified => "许可证校验通过：{0}，过期时间：{1}";
    public string LicenseValidateFailed => "许可证校验失败。";
    public string PublicModeTrialActive => "授权文件缺失，但当前时间在试用期内，试用期剩余 {0} 天。";
    public string PublicModeBuildTimeUnavailable => "试用模式不可用。";
    public string PublicModeEnabled => "服务器为公开版本，正在启用额外的安全措施。";
    public string PublicModeValidationFailed => "许可证验证失败，程序加载已停止。";
    public string HandbookGeneratedAt => "手册生成时间 {0}";
    public string HandbookSectionCommand => "命令";
    public string HandbookSectionAvatar => "角色";
    public string HandbookSectionItem => "物品";
    public string HandbookSectionStageId => "关卡ID";
    public string HandbookSectionMainMission => "主任务";
    public string HandbookSectionSubMission => "子任务";
    public string HandbookSectionRogueBuff => "模拟宇宙祝福";
    public string HandbookSectionRogueMiracle => "模拟宇宙奇物";
    public string HandbookSectionCurrencyWarRole => "货币战争角色";
    public string HandbookSectionCurrencyWarEquipment => "货币战争装备";
    public string HandbookSectionCurrencyWarConsumable => "货币战争消耗品";
    public string HandbookSectionCurrencyWarOrb => "货币战争晶矿";
    public string HandbookSectionRogueDiceSurface => "骰面效果";
    public string HandbookSectionRogueDialogue => "模拟宇宙事件对话";
    public string HandbookDescriptionLabel => "描述";
    public string HandbookUsageLabel => "用法";
    public string HandbookAliasesLabel => "别名";
    public string HandbookPermissionLabel => "权限";
    public string HandbookCommandNotesTitle => "命令使用说明：";
    public string HandbookCommandNoteTarget => "可在命令前后加 @<UID> 指定目标玩家。";
    public string HandbookCommandNoteQuote => "包含空格的参数请用引号包裹，例如 \"Sender Name\"。";
    public string HandbookCommandNoteCaseInsensitive => "子命令不区分大小写。";
    public string HandbookCommandNoteRequiredOptional => "<value> 为必填，[value] 为可选。";
    public string HandbookCommandNoteOptionStyle => "x<count>、l<level>、r<rank> 为选项式参数。";
    public string HandbookCommandNoteLongOptionAliases => "支持长选项别名：--count、--amount、--level、--rank、--eidolon、--superimposition。";
}

#endregion

#endregion

#region Layer 3

#region CommandText




public class NoticeTextCHS
{
    public string PlayerNotFound => "未找到玩家!";
    public string InvalidArguments => "无效的参数!";
    public string NoPermission => "你没有权限这么做!";
    public string CommandNotFound => "未找到命令! 输入 '/help' 来获取帮助";
    public string TargetOffline => "目标 {0}({1}) 离线了！清除当前目标";
    public string TargetFound => "找到目标 {0}({1})，下一次命令将默认对其执行";
    public string TargetNotFound => "未找到目标 {0}!";
    public string InternalError => "在处理命令时发生了内部错误!";
}




public class HeroTextCHS
{
    public string Desc =>
        "切换主角的性别/形态\n当切换性别时，genderId为1代表男性，2代表女性\n当切换形态时，8001代表毁灭命途，8003代表存护命途，8005代表同谐命途。\n注意，切换性别时会清空所有可选命途以及行迹，为不可逆操作！";

    public string Usage => "用法：/hero gender <genderId: 1=男, 2=女>\n\n用法：/hero type <typeId: 8001|8003|8005|8007|8009>";
    public string GenderNotSpecified => "性别不存在!";
    public string HeroTypeNotSpecified => "主角类型不存在!";
    public string GenderChanged => "性别已更改!";
    public string HeroTypeChanged => "主角类型已更改!";
}




public class UnlockAllTextCHS
{
    public string Desc =>
        "解锁所有在类别内的对象\n" +
        "使用 /unlockall mission 以完成所有任务，使用后会被踢出，重新登录后可能会被教程卡住，请谨慎使用\n" +
        "使用 /unlockall tutorial 以解锁所有教程，使用后会被踢出，用于部分界面卡住无法行动的情况\n" +
        "使用 /unlockall rogue 以解锁所有类型模拟宇宙，使用后会被踢出，建议与 /unlockall tutorial 搭配使用以获取更好效果";

    public string Usage => "用法：/unlockall <mission|quest|tutorial|rogue|challenge|grid>";
    public string UnlockedAll => "已解锁/完成所有{0}!";
}




public class AvatarTextCHS
{
    public string Desc => "设定玩家已有角色的属性\n设置行迹等级时，设置X级即设置所有行迹节点至X级，若大于此节点允许的最高等级，设置为最高等级\n注意：-1意为所有已拥有角色";

    public string Usage =>
        "用法：/avatar talent <角色ID|-1> <行迹等级 0-10>\n\n用法：/avatar get <角色ID>\n\n用法：/avatar rank <角色ID|-1> <星魂 0-6>\n\n用法：/avatar level <角色ID|-1> <角色等级 1-80>\n\n用法：/avatar path <角色ID> <命途ID>";

    public string InvalidLevel => "{0}等级无效";
    public string AllAvatarsLevelSet => "已将全部角色 {0}等级设置为 {1}";
    public string AvatarLevelSet => "已将 {0} 角色 {1}等级设置为 {2}";
    public string AvatarNotFound => "角色不存在!";
    public string AvatarGet => "获取到角色 {0}!";
    public string AvatarFailedGet => "获取角色 {0} 失败!";
}




public class GiveTextCHS
{
    public string Desc => "给予玩家物品，此处可输入角色ID，但无法设置行迹、等级及星魂";
    public string Usage => "用法：/give <物品ID> [x<数量=1>] [l<等级=1, 1-80>] [r<叠影=1, 1-5>]";
    public string ItemNotFound => "未找到物品!";
    public string GiveItem => "给予 @{0} {1} 个物品 {2}";
}




public class GiveAllTextCHS
{
    public string Desc =>
        "给予玩家全部指定类型的物品\navatar意为角色，equipment意为光锥，material意为可堆叠材料，relic意为遗器，unlock意为气泡、手机壁纸、头像，train意为开拓者房间内容，pet意为宠物，path意为多命途角色命途";

    public string Usage =>
        "用法：/giveall avatar r<星魂> l<等级>\n\n" +
        "用法：/giveall equipment|lightcone r<叠影> l<等级> x<数量>\n\n" +
        "用法：/giveall material [x<数量=1>]\n\n" +
        "用法：/giveall relic l<等级> x<数量>\n\n" +
        "用法：/giveall unlock\n\n" +
        "用法：/giveall train\n\n" +
        "用法：/giveall pet [x<数量=1>]\n\n" +
        "用法：/giveall path";

    public string GiveAllItems => "已给予所有 {0}, 各 {1} 个";
}




public class LineupTextCHS
{
    public string Desc => "管理玩家的队伍\n秘技点一次性只能获得两个";
    public string Usage => "用法：/lineup mp [数量=2]\n\n用法：/lineup heal";
    public string PlayerGainedMp => "玩家已获得 {0} 秘技点";
    public string HealedAllAvatars => "成功治愈当前队伍中的所有角色";
}




public class HelpTextCHS
{
    public string Desc => "显示帮助信息。控制台命令可使用 @<UID> 指定在线目标。";
    public string Usage => "用法：/help\n\n用法：/help [命令]\n\n目标：@<UID> <命令> ... 或 <命令> ... @<UID>";
    public string Commands => "命令:";
    public string CommandPermission => "所需权限: ";
    public string CommandAlias => "命令别名：";
    public string CommandNotFound => "未找到命令! 输入 '/help' 来获取帮助";
}




public class KickTextCHS
{
    public string Desc => "踢出玩家";
    public string Usage => "用法：/kick";
    public string PlayerKicked => "玩家 {0} 已被踢出!";
}




public class MissionTextCHS
{
    public string Desc =>
        "管理玩家的任务\n" +
        "使用 pass 完成当前正在进行的所有任务，此命令易造成严重卡顿，请尽量使用 /mission finish 替代\n" +
        "使用 finish [子任务ID] 完成指定子任务，请浏览 handbook 来获取子任务ID\n" +
        "使用 finishmain [主任务ID] 完成指定主任务，请浏览 handbook 来获取主任务ID\n" +
        "使用 running <-all> 获取正在追踪的任务，增加'-all'则显示所有正在进行的任务以及可能卡住的任务，使用后可能会出现较长任务列表，请注意甄别\n" +
        "使用 reaccept [主任务ID] 可重新进行指定主任务，请浏览 handbook 来获取主任务ID";

    public string Usage =>
        "用法：/mission pass\n\n用法：/mission finish [子任务ID]\n\n用法：/mission running <-all>\n\n用法：/mission reaccept [主任务ID]\n\n用法：/mission finishmain [主任务ID]";

    public string AllMissionsFinished => "所有任务已完成!";
    public string AllRunningMissionsFinished => "共 {0} 个进行中的任务已完成!";
    public string MissionFinished => "任务 {0} 已完成!";
    public string InvalidMissionId => "无效的任务ID!";
    public string NoRunningMissions => "没有正在进行的任务!";

    public string RunningMissions => "正在进行的任务:";
    public string PossibleStuckMissions => "可能卡住的任务:";
    public string MainMission => "主任务";

    public string MissionReAccepted => "重新接受任务 {0}!";
}




public class RelicTextCHS
{
    public string Desc => "管理玩家的遗器\n主词条可选，副词条可选，但至少存在其中之一\n等级限制：1≤等级≤9999";

    public string Usage =>
        "用法：/relic <遗器ID> <主词条ID> <小词条ID1:小词条等级> <小词条ID2:小词条等级> <小词条ID3:小词条等级> <小词条ID4:小词条等级> l<等级> x<数量>";

    public string RelicNotFound => "遗器不存在!";
    public string InvalidMainAffixId => "主词条ID无效";
    public string InvalidSubAffixId => "副词条ID无效";
    public string RelicGiven => "给予玩家 @{0} {1} 个遗器 {2}, 主词条 {3}";
}




public class ReloadTextCHS
{
    public string Desc => "重新加载指定的配置\n配置名：banner, activity, videokey, plugin";
    public string Usage => "用法：/reload <banner|activity|videokey|plugin>";
    public string ConfigReloaded => "配置 {0} 已重新加载!";
}




public class RogueTextCHS
{
    public string Desc => "管理玩家模拟宇宙中的数据\n-1意为所有祝福（已拥有祝福）\n使用 buff 来获取祝福\n使用 enhance 来强化祝福";

    public string Usage =>
        "用法：/rogue money [宇宙碎片数量]\n\n用法：/rogue buff [祝福ID/-1]\n\n用法：/rogue miracle [奇物ID]\n\n用法：/rogue enhance [祝福ID/-1]\n\n用法：/rogue unstuck - 脱离事件";

    public string PlayerGainedMoney => "玩家已获得 {0} 宇宙碎片";
    public string PlayerGainedAllItems => "玩家已获得所有{0}";
    public string PlayerGainedItem => "玩家已获得{0} {1}";
    public string PlayerEnhancedBuff => "玩家已强化祝福 {0}";
    public string PlayerEnhancedAllBuffs => "玩家已强化所有祝福";
    public string PlayerUnstuck => "玩家已脱离事件";
    public string NotFoundItem => "未找到 {0}!";
    public string PlayerNotInRogue => "玩家不在模拟宇宙中!";
}




public class SceneTextCHS
{
    public string Desc =>
        "管理玩家场景\n" +
        "提示：此组大多为调试使用，使用命令前，请确保你清楚你在做什么！\n" +
        "使用 prop 来设置道具状态，在Common/Enums/Scene/PropStateEnum.cs获取状态列表\n" +
        "使用 unlockall 来解锁场景内所有道具（即将所有能设置为open状态的道具设置为open状态），此命令有较大可能会导致游戏加载卡条约90%，使用 /scene reset <floorId> 来解决问题\n" +
        "使用 change 来进入指定场景，要获取EntryId，请访问 Resources/MapEntrance.json\n" +
        "使用 reload 来重新加载当前场景，并回到初始位置\n" +
        "使用 reset 来重置指定场景所有道具状态，要获取当前FloorId，请使用 /scene cur";

    public string Usage =>
        "用法：/scene group\n\n用法：/scene prop <组ID> <道具ID> <状态>\n\n用法：/scene remove <实体ID>\n\n用法：/scene unlockall\n\n用法：/scene unlockallgroup\n\n用法：/scene change <entryId>\n\n用法：/scene reload\n\n用法：/scene reset <floorId>\n\n用法：/scene fsv <名称> <值>\n\n用法：/scene gp <组ID> <名称> <值>\n\n用法：/scene cur\n\n用法：/scene near\n\n用法：/scene forward <距离>";

    public string LoadedGroups => "已加载组: {0}";
    public string PropStateChanged => "道具: {0} 的状态已设置为 {1}";
    public string PropNotFound => "未找到道具!";
    public string EntityRemoved => "实体 {0} 已被移除";
    public string EntityNotFound => "未找到实体!";
    public string AllPropsUnlocked => "所有道具已解锁!";
    public string SceneChanged => "已进入场景 {0}";
    public string SceneReloaded => "场景已重新加载!";
    public string SceneReset => "已重置场景 {0} 中所有道具状态！";
    public string CurrentScene => "当前场景Entry Id: {0}, Plane Id: {1}, Floor Id: {2}";
    public string FSVSet => "已将 {0} 设置为 {1}";
    public string Teleported => "已传送！";
}




public class MailTextCHS
{
    public string Desc => "管理玩家的邮件";
    public string Usage => "用法：/mail <发送者> <模板ID> <过期天数> _TITLE <标题> _CONTENT <内容> _ATTACH <物品ID> [x<数量>] [r<星魂/叠影>] [s<叠影>] [l<等级>] [p<晋阶>] [t<行迹等级|max>] ...\n用法：/mail all <发送者> <模板ID> <过期天数> _TITLE <标题> _CONTENT <内容> _ATTACH ...\n用法：/mail welcome [all]";
    public string MailSent => "邮件已发送!";
    public string MailSentWithAttachment => "带附件的邮件已发送!";
}




public class RaidTextCHS
{
    public string Desc => "管理玩家的任务临时场景";
    public string Usage => "用法：/raid leave\n\n用法：/raid reset <RaidID> <Level>";
    public string Leaved => "已离开临时场景!";
}




public class AccountTextCHS
{
    public string Desc => "管理账号\n注意：此命令未经测试，请谨慎使用！";
    public string Usage => "用法：/account create <用户名> <邮箱> [密码] [UID]\n用法：/account create <用户名> <邮箱> [--password <密码>] [--uid <UID>]\n用法：/account setpassword <用户名|UID|邮箱> <密码>\n用法：/account setemail <用户名|UID|邮箱> <邮箱> [verified:true|false]\n用法：/account verifyemail <用户名|UID|邮箱>\n用法：/account ban <用户名|UID|邮箱> <permanent|duration> [原因]\n用法：/account unban <用户名|UID|邮箱>\n用法：/account baninfo <用户名|UID|邮箱>\n用法：/account reset [用户名|UID|邮箱]\n用法：/account delete <用户名|UID|邮箱> confirm\n用法：/account remove <用户名|UID|邮箱> confirm\n用法：/account role <用户名|UID|邮箱> <角色>\n用法：/account setrole <用户名|UID|邮箱> <角色>\n用法：/account roles";
    public string SetPasswordUsage => "用法：/account setpassword <用户名|UID|邮箱> <密码>";
    public string SetEmailUsage => "用法：/account setemail [用户名|UID|邮箱] [邮箱] [verified:true|false]";
    public string VerifyEmailUsage => "用法：/account verifyemail [用户名|UID|邮箱]";
    public string BanUsage => "用法：/account ban [用户名|UID|邮箱] [permanent|duration] [原因]";
    public string BanDurationExamples => "时长示例：30m、2h、7d、1w。";
    public string UnbanUsage => "用法：/account unban [用户名|UID|邮箱]";
    public string BanInfoUsage => "用法：/account baninfo [用户名|UID|邮箱]";
    public string RoleUsage => "用法：/account role <用户名|UID|邮箱> <角色>";
    public string ResetUsage => "用法：/account reset [用户名|UID|邮箱]";
    public string AccountNotFound => "账号 {0} 不存在。";
    public string ResetStarted => "正在重置账号 {0}。你将被断开连接，重新登录后会重新开始。";
    public string ResetSuccess => "账号 {0} 已重置。";
    public string DeleteUsage => "用法：/account delete|remove <用户名|UID|邮箱> confirm";
    public string DeleteRequiresConfirm => "这会永久删除账号 {0} 及其所有玩家数据。请执行 /account delete {0} confirm 继续。";
    public string DeleteStarted => "正在删除账号 {0}。你将被断开连接。";
    public string DeleteSuccess => "账号 {0} 已删除。";
    public string InvalidUid => "无效UID参数！";
    public string CreateError => "出现内部错误 {0} ";
    public string CreateSuccess => "新账号 {0} 创建成功!";
    public string DuplicateAccount => "账号 {0} 已存在!";
    public string DuplicateUID => "UID {0} 已存在!";
    public string DataError => "新账号获取失败! {0}!";
}




public class UnstuckTextCHS
{
    public string Desc => "将离线玩家传送回默认场景。面向控制台/管理员使用，目标必须离线。";
    public string Usage => "用法：/unstuck <离线UID>";
    public string UnstuckSuccess => "已成功将该玩家传送回默认场景";
    public string UidNotExist => "该UID不存在！";
    public string PlayerIsOnline => "该玩家目前在线上！";
}




public class SetlevelTextCHS
{
    public string Desc => "设定玩家等级";
    public string Usage => "用法：/setlevel <等级 1-70>";
    public string SetlevelSuccess => "等级设定成功！";
}




public class GridTextCHS
{
    public string Desc => "管理货币战争内容，注意，此部分内容尚未完善，若有问题请及时反馈。\n此命令不一定会检查ID是否存在，若无效果，请检查参数是否正确";
    public string Usage => "用法：/grid gold [金币数量]\n\n" +
                           "用法：/grid role [角色ID] [角色星级]\n\n" +
                           "用法：/grid equip [装备ID]\n\n" +
                           "用法：/grid consumable [消耗品ID]\n\n" +
                           "用法：/grid orb [晶矿ID]\n\n" +
                           "用法：/grid section [章节ID] [关卡ID]";
    public string NotInGame => "不在货币战争中！";
    public string InvalidRole => "角色ID或星级不存在！";
    public string InvalidEquipment => "装备ID不存在！";
    public string InvalidOrb => "晶矿ID不存在！";
    public string InvalidConsumable => "消耗品ID不存在！";
    public string AddedRole => "已添加角色。";
    public string UpdateGold => "获得 {0} 个金币。";
    public string AddEquipment => "已添加 {0} 装备。";
    public string AddOrb => "已添加 {0} 晶矿。";
    public string AddConsumable => "已添加 {0} 消耗品。";
    public string EnterSection => "进入 {0}-{1}。";
}




public class JsonTextCHS
{
    public string Desc => "从 JSON 导入角色、遗器、光锥数据，或清空现有遗器和光锥。";
    public string Usage => "用法：/json\n\n用法：/json <文件名|序号|路径>\n\n用法：/json clear\n\n用法：/json export [name] [format=true|false]";
    public string ClearInventory => "已清空当前玩家的遗器和光锥。";
    public string FileNotFound => "未找到文件：{0}";
    public string ReadOrParseFailed => "读取或解析 JSON 失败：{0}";
    public string InvalidJsonContent => "JSON 内容无效或为空。";
    public string ImportSummary => "导入完成：{0}，角色 {1}，遗器 {2}，光锥 {3}。";
    public string NoFileFoundWithHint => "未找到可导入的 JSON 文件，请先将文件放到 Config/Json。";
    public string InvalidChoice => "无效序号，可选范围：1 ~ {0}";
    public string NoFileFound => "未找到可导入的 JSON 文件。";
    public string SearchedDirectories => "已搜索目录：";
    public string SearchedDirectoryItem => "- {0}";
    public string FoundFiles => "找到以下 JSON 文件：";
    public string FileListItem => "{0}. {1}";
    public string UsageSelectHint => "输入 /json [序号] 或 /json [文件名] 进行导入。";
    public string AvatarExcelNotFound => "未找到角色配置：{0}";

    public string ExportSuccess => "导出成功，文件：{0}";
}




public class AnnoTextCHS
{
    public string Desc => "发送服务器弹窗公告。";
    public string Usage =>
        "用法：/anno [有效时长] [显示间隔] <公告内容>\n\n" +
        "示例：\n" +
        "  /anno 10m 服务器即将重启\n" +
        "  /anno 2h 5m 双倍掉落已开启\n" +
        "  /anno @10001 10m 单人通知\n\n" +
        "选项：\n" +
        "  --duration <时间>    公告有效时长，例如：30s、10m、2h、3d\n" +
        "  --frequency <时间>   横幅再次显示间隔，例如：30s、10m、1h";
    public string Disabled => "公告功能未启用，请先在配置中开启 ServerOption.ServerAnnounce.EnableAnnounce。";
    public string InvalidDuration => "公告有效时长无效，请使用类似 30s、10m、2h 或 3d 的格式。";
    public string InvalidFrequency => "公告显示间隔无效，请使用类似 30s、10m、2h 或 3d 的格式。";
    public string Sent => "已向 {0} 名在线玩家发送弹窗消息。有效时长：{1}。显示间隔：{2}。";
}




public class CalyxTextCHS
{
    public string Desc => "切换花萼覆盖模式：开启时将拟造花萼替换为 freesr 文件中的关卡内容。";
    public string Usage => "用法：/calyx on\n用法：/calyx off";
    public string ToggleOn => "花萼覆盖已开启。";
    public string ToggleOff => "花萼覆盖已关闭。";
    public string MissingIndex => "请指定 freesr 文件序号。用法：/calyx on [序号]";
    public string InvalidMode => "未知模式 '{0}'。请使用 /calyx on 或 /calyx off。";
    public string LoadFailed => "加载 freesr 文件失败。";
    public string NoCachedData => "未找到已缓存的 freesr 数据。请先使用 /json <文件名> 或通过 srtools 网站同步导入。";
}




public class ClearTextCHS
{
    public string Desc => "从目标玩家背包中移除未装备的光锥或遗器。";
    public string Usage => "用法：/clear equipment\n\n用法：/clear relic";
    public string ClearEquipment => "已移除 {0} 个未装备光锥。";
    public string ClearRelic => "已移除 {0} 个未装备遗器。";
}




public class DebugTextCHS
{
    public string Desc => "调试战斗配置和自定义包加载。";
    public string Usage => "用法：/debug specific <StageID>\n\n用法：/debug monster <MonsterID>\n\n用法：/debug customP <PacketJsonPath>";
    public string InvalidStageId => "无效的关卡或怪物ID。";
    public string SetStageId => "调试战斗覆盖已更新。";
    public string CustomPacketFileNotFound => "未找到自定义包文件。";
    public string CustomPacketFileInvalid => "自定义包文件无效或为空。";
    public string CustomPacketFileLoaded => "自定义包队列已加载。";
}




public class RemoveTextCHS
{
    public string Desc => "管理员专用：移除已拥有角色或光锥。";
    public string Usage => "用法：/remove character <角色ID> confirm\n\n用法：/remove avatar <角色ID> confirm\n\n用法：/remove lightcone <唯一ID|物品ID> confirm\n\n用法：/remove equipment <唯一ID|物品ID> confirm\n\n添加 --dry-run 可预览将移除的内容。";
    public string RequiresConfirm => "这是破坏性操作。添加 confirm 执行，或使用 --dry-run 预览。";
    public string NotFound => "未找到 {0} {1}。";
    public string DryRun => "预览：将移除 {0} 个 {1}。";
    public string Success => "已移除 {0} 个 {1}。客户端可能需要重新登录才能完全刷新。";
}




public class WindyTextCHS
{
    public string Desc => "加载 Lua 字节码并发送给目标玩家。";
    public string Usage => "用法：/windy <相对于 Lua 文件夹的 lua 路径>";
    public string Loaded => "已从 Lua 脚本读取 BYTECODE：{0}";
    public string ReadError => "读取 Lua 脚本失败：{0}";
}

#endregion

#endregion

