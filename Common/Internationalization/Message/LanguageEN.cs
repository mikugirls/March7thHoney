namespace March7thHoney.Internationalization.Message;

#region Root

public class LanguageEN
{
    public GameTextEN Game { get; } = new();
    public ServerTextEN Server { get; } = new();
    public WordTextEN Word { get; } = new(); 
}

#endregion

#region Layer 1




public class GameTextEN
{
    public CommandTextEN Command { get; } = new();
}




public class ServerTextEN
{
    public WebTextEN Web { get; } = new();
    public ServerInfoTextEN ServerInfo { get; } = new();
}




public class WordTextEN
{
    public string Rank => "Eidolon";
    public string Avatar => "Avatar";
    public string Material => "Material";
    public string Pet => "Pet";
    public string Relic => "Relic";
    public string Equipment => "Light Cone";
    public string Talent => "Trace";
    public string Banner => "Banner";
    public string VideoKeys => "Video Keys";
    public string Activity => "Activity";
    public string Buff => "Blessing";
    public string Miracle => "Curio";
    public string Unlock => "Luxury Item";
    public string TrainParty => "Train Party Item";

    
    public string Config => "Config File";
    public string Language => "Language";
    public string Log => "Log";
    public string GameData => "Game Data";
    public string Cache => "Resource Cache";
    public string Database => "Database";
    public string Command => "Command";
    public string WebServer => "Web Server";
    public string Plugin => "Plugin";
    public string Handler => "Packet Handler";
    public string Dispatch => "Global Dispatch";
    public string Game => "Game";
    public string Handbook => "Handbook";
    public string NotFound => "Not Found";
    public string Error => "Error";
    public string FloorInfo => "Floor File";
    public string FloorGroupInfo => "Floor Group File";
    public string FloorMissingResult => "Teleport & World Generation";
    public string FloorGroupMissingResult => "Teleport, Monster Battle & World Generation";
    public string Mission => "Mission";
    public string MissionInfo => "Mission File";
    public string SubMission => "Sub Mission";
    public string SubMissionInfo => "Sub Mission File";
    public string MazeSkill => "Maze Skill";
    public string MazeSkillInfo => "Maze Skill File";
    public string Dialogue => "Simulated Universe Event";
    public string DialogueInfo => "Simulated Universe Event File";
    public string Performance => "Performance";
    public string PerformanceInfo => "Performance File";
    public string RogueChestMap => "Simulated Universe Map";
    public string RogueChestMapInfo => "Simulated Universe Map File";
    public string ChessRogueRoom => "Simulated Universe DLC";
    public string ChessRogueRoomInfo => "Simulated Universe DLC File";
    public string SummonUnit => "Skill Summon";
    public string SummonUnitInfo => "Skill Summon File";
    public string RogueTournRoom => "Divergent Universe";
    public string RogueTournRoomInfo => "Divergent Universe Room File";
    public string TypesOfRogue => "Types of Simulated Universe";
    public string RogueMagicRoom => "Unknowable Domain";
    public string RogueMagicRoomInfo => "Unknowable Domain Room File";
    public string RogueDiceSurface => "Dice Surface Effect";
    public string RogueDiceSurfaceInfo => "Dice Surface Effect File";
    public string AdventureModifier => "AdventureModifier";
    public string AdventureModifierInfo => "AdventureModifier File";

    public string DatabaseAccount => "Database Account";
    public string Tutorial => "Tutorial";
}

#endregion

#region Layer 2

#region GameText




public class CommandTextEN
{
    public NoticeTextEN Notice { get; } = new();

    public HeroTextEN Hero { get; } = new();
    public AvatarTextEN Avatar { get; } = new();
    public GiveTextEN Give { get; } = new();
    public GiveAllTextEN GiveAll { get; } = new();
    public LineupTextEN Lineup { get; } = new();
    public HelpTextEN Help { get; } = new();
    public KickTextEN Kick { get; } = new();
    public MissionTextEN Mission { get; } = new();
    public RelicTextEN Relic { get; } = new();
    public ReloadTextEN Reload { get; } = new();
    public RogueTextEN Rogue { get; } = new();
    public SceneTextEN Scene { get; } = new();
    public UnlockAllTextEN UnlockAll { get; } = new();
    public MailTextEN Mail { get; } = new();
    public RaidTextEN Raid { get; } = new();
    public AccountTextEN Account { get; } = new();
    public UnstuckTextEN Unstuck { get; } = new();
    public SetlevelTextEN Setlevel { get; } = new();
    public GridTextEN Grid { get; } = new();
    public JsonTextEN Json { get; } = new();
    public AnnoTextEN Anno { get; } = new();
    public CalyxTextEN Calyx { get; } = new();
    public ClearTextEN Clear { get; } = new();
    public DebugTextEN Debug { get; } = new();
    public RemoveTextEN Remove { get; } = new();
    public WindyTextEN Windy { get; } = new();
}

#endregion

#region ServerText




public class WebTextEN
{
    public string Maintain => "The server is under maintenance. Please try again later.";
}




public class ServerInfoTextEN
{
    public string Shutdown => "Shutting down...";
    public string CancelKeyPressed => "Cancel key (Ctrl + C) pressed. The server will shut down shortly...";
    public string StartingServer => "Starting March7thHoney...";
    public string CurrentVersion => "Current server-supported version: {0}";
    public string LoadingItem => "Loading {0}...";
    public string GeneratingItem => "Generating {0}...";
    public string WaitingItem => "Waiting for process {0} to complete...";
    public string RegisterItem => "Registered {0} {1}.";
    public string FailedToLoadItem => "Failed to load {0}.";
    public string NewClientSecretKey => "Client secret key does not exist. Generating a new client secret key.";
    public string FailedToInitializeItem => "Failed to initialize {0}.";
    public string FailedToReadItem => "Failed to read {0}. File {1}";
    public string GeneratedItem => "Generated {0}.";
    public string UpdatedItem => "Updated {0}.";
    public string LoadedItem => "Loaded {0}.";
    public string LoadedItems => "Loaded {0} {1}.";
    public string ServerRunning => "{0} server is listening on {1}";
    public string ServerStarted => "Startup complete! Took {0}s, Type 'help' for command help."; 
    public string MissionEnabled => "Mission system enabled. This feature is still under development and may not work as expected. If you encounter any bugs, please report them to the developers.";
    public string CacheLoadSkip => "Cache loading skipped.";

    public string ConfigMissing => "{0} is missing. Please check your resource folder: {1}. {2} may not be available.";
    public string PartialConfigMissing => "{0} is partially missing. Missing count: {1}. Sample paths: {2}";
    public string UnloadedItems => "Unloaded all {0}.";
    public string SaveDatabase => "Saved database. Took {0}s";
    public string WaitForAllDone => "Cannot enter the game yet. Please wait until all items are loaded before trying again.";

    public string UnhandledException => "An unhandled exception occurred: {0}";
    public string LicenseFileNotFound => "License file not found: {0}";
    public string LicenseInvalid => "License invalid. Please get authorization from {0}";
    public string LicenseParseFailed => "License parse failed.";
    public string LicenseExpired => "License expired.";
    public string LicenseHwidMismatch => "License HWID mismatch.";
    public string LicenseCurrentHwid => "Current machine HWID: {0}";
    public string LicenseVerified => "License verified for {0}. Expires at: {1}";
    public string LicenseValidateFailed => "Failed to validate License.";
    public string PublicModeTrialActive => "Trial mode is active. {0} days remain in the trial period.";
    public string PublicModeBuildTimeUnavailable => "Trial mode cannot be used.";
    public string PublicModeEnabled => "Server is in public mode. Enabling additional security measures.";
    public string PublicModeValidationFailed => "License validation failed, program loading has been stopped.";
    public string HandbookGeneratedAt => "Handbook generated in {0}";
    public string HandbookSectionCommand => "Command";
    public string HandbookSectionAvatar => "Avatar";
    public string HandbookSectionItem => "Item";
    public string HandbookSectionStageId => "StageId";
    public string HandbookSectionMainMission => "MainMission";
    public string HandbookSectionSubMission => "SubMission";
    public string HandbookSectionRogueBuff => "RogueBuff";
    public string HandbookSectionRogueMiracle => "RogueMiracle";
    public string HandbookSectionCurrencyWarRole => "CurrencyWarRole";
    public string HandbookSectionCurrencyWarEquipment => "CurrencyWarEquipment";
    public string HandbookSectionCurrencyWarConsumable => "CurrencyWarConsumable";
    public string HandbookSectionCurrencyWarOrb => "CurrencyWarOrb";
    public string HandbookSectionRogueDiceSurface => "RogueDiceSurface";
    public string HandbookSectionRogueDialogue => "RogueDialogue";
    public string HandbookDescriptionLabel => "Description";
    public string HandbookUsageLabel => "Usage";
    public string HandbookAliasesLabel => "Aliases";
    public string HandbookPermissionLabel => "Permission";
    public string HandbookCommandNotesTitle => "Command usage notes:";
    public string HandbookCommandNoteTarget => "Use @<UID> before or after a command to target another player.";
    public string HandbookCommandNoteQuote => "Wrap arguments that contain spaces in quotes, for example \"Sender Name\".";
    public string HandbookCommandNoteCaseInsensitive => "Subcommands are case-insensitive.";
    public string HandbookCommandNoteRequiredOptional => "<value> is required. [value] is optional.";
    public string HandbookCommandNoteOptionStyle => "x<count>, l<level>, and r<rank> are option-style arguments.";
    public string HandbookCommandNoteLongOptionAliases => "Long option aliases are supported: --count, --amount, --level, --rank, --eidolon, --superimposition.";
}

#endregion

#endregion

#region Layer 3

#region CommandText




public class NoticeTextEN
{
    public string PlayerNotFound => "Player not found!";
    public string InvalidArguments => "Invalid arguments!";
    public string NoPermission => "You do not have permission to do that!";
    public string CommandNotFound => "Command not found! Type '/help' for help";
    public string TargetOffline => "Target {0}({1}) is offline! Clearing current target";
    public string TargetFound => "Target {0}({1}) found. Next command will target them by default.";
    public string TargetNotFound => "Target {0} not found!";
    public string InternalError => "An internal error occurred while processing the command!";
}




public class HeroTextEN
{
    public string Desc =>
        "Switch the main character's gender/form.\nWhen switching gender, genderId 1 represents male, 2 represents female.\nWhen switching form, 8001 represents Destruction Path, 8003 represents Preservation Path, 8005 represents Harmony Path.\nNote: Switching gender will clear all optional Paths and Traces, and this operation is irreversible!";

    public string Usage => "Usage: /hero gender <genderId: 1=male, 2=female>\n\nUsage: /hero type <typeId: 8001|8003|8005|8007|8009>";
    public string GenderNotSpecified => "Gender does not exist!";
    public string HeroTypeNotSpecified => "Hero type does not exist!";
    public string GenderChanged => "Gender changed!";
    public string HeroTypeChanged => "Hero type changed!";
}




public class UnlockAllTextEN
{
    public string Desc =>
        "Unlock all objects within the category.\n" +
        "Use /unlockall mission to complete all missions. You will be kicked after use, and may get stuck in the tutorial upon re-login. Use with caution.\n" +
        "Use /unlockall tutorial to unlock all tutorials. You will be kicked after use. Use for situations where the interface is stuck and you cannot act.\n" +
        "Use /unlockall rogue to unlock all types of Simulated Universe. You will be kicked after use. Recommended to use with /unlockall tutorial for better results.";

    public string Usage => "Usage: /unlockall <mission|quest|tutorial|rogue|challenge|grid>";
    public string UnlockedAll => "Unlocked/completed all {0}!";
}




public class AvatarTextEN
{
    public string Desc => "Set properties for player's owned avatars.\nWhen setting trace level, setting to level X sets all trace nodes to level X. If greater than the node's max allowed level, sets to max level.\nNote: -1 means all owned avatars.";

    public string Usage =>
        "Usage: /avatar talent <AvatarID|-1> <trace level 0-10>\n\nUsage: /avatar get <AvatarID>\n\nUsage: /avatar rank <AvatarID|-1> <eidolon 0-6>\n\nUsage: /avatar level <AvatarID|-1> <level 1-80>\n\nUsage: /avatar path <AvatarID> <PathID>";

    public string InvalidLevel => "Invalid {0} level";
    public string AllAvatarsLevelSet => "Set all avatars' {0} level to {1}";
    public string AvatarLevelSet => "Set avatar {0}'s {1} level to {2}";
    public string AvatarNotFound => "Avatar does not exist!";
    public string AvatarGet => "Obtained avatar {0}!";
    public string AvatarFailedGet => "Failed to obtain avatar {0}!";
}




public class GiveTextEN
{
    public string Desc => "Give items to the player. Avatar IDs can be entered here, but traces, levels, and eidolons cannot be set.";
    public string Usage => "Usage: /give <ItemID> [x<count=1>] [l<level=1, 1-80>] [r<rank=1, 1-5>]";
    public string ItemNotFound => "Item not found!";
    public string GiveItem => "Gave @{0} {1} of item {2}";
}




public class GiveAllTextEN
{
    public string Desc =>
        "Give the player all items of the specified type.\navatar means characters, equipment means Light Cones, material means stackable materials, relic means relics, unlock means chat bubbles, phone wallpapers, avatars, train means Trailblazer's room contents, pet means pets, path means Paths for multi-Path characters.";

    public string Usage =>
        "Usage: /giveall avatar r<Eidolon> l<Level>\n\n" +
        "Usage: /giveall equipment|lightcone r<Superimposition> l<Level> x<Count>\n\n" +
        "Usage: /giveall material [x<count=1>]\n\n" +
        "Usage: /giveall relic l<Level> x<Count>\n\n" +
        "Usage: /giveall unlock\n\n" +
        "Usage: /giveall train\n\n" +
        "Usage: /giveall pet [x<count=1>]\n\n" +
        "Usage: /giveall path";

    public string GiveAllItems => "Gave all {0}, {1} each";
}




public class LineupTextEN
{
    public string Desc => "Manage the player's lineup.\nMaze points can only be obtained two at a time.";
    public string Usage => "Usage: /lineup mp [amount=2]\n\nUsage: /lineup heal";
    public string PlayerGainedMp => "Player gained {0} Maze Points";
    public string HealedAllAvatars => "Successfully healed all avatars in the current lineup";
}




public class HelpTextEN
{
    public string Desc => "Show help information. Console commands can target an online player with @<UID>.";
    public string Usage => "Usage: /help\n\nUsage: /help [command]\n\nTargeting: @<UID> <command> ... or <command> ... @<UID>";
    public string Commands => "Commands:";
    public string CommandPermission => "Required Permission: ";
    public string CommandAlias => "Command Aliases: ";
    public string CommandNotFound => "Command not found! Type '/help' for help";
}




public class KickTextEN
{
    public string Desc => "Kick a player";
    public string Usage => "Usage: /kick";
    public string PlayerKicked => "Player {0} has been kicked!";
}




public class MissionTextEN
{
    public string Desc =>
        "Manage player's missions.\n" +
        "Use pass to complete all currently ongoing missions. This command can cause severe lag. Try to use /mission finish instead.\n" +
        "Use finish [SubMissionID] to complete a specific sub mission. Please browse the handbook for SubMissionID.\n" +
        "Use finishmain [MainMissionID] to complete a specific main mission. Please browse the handbook for MainMissionID.\n" +
        "Use running <-all> to get tracked missions. Add '-all' to show all ongoing and potentially stuck missions. This may produce a long list, please review carefully.\n" +
        "Use reaccept [MainMissionID] to restart a specific main mission. Please browse the handbook for MainMissionID.";

    public string Usage =>
        "Usage: /mission pass\n\nUsage: /mission finish [SubMissionID]\n\nUsage: /mission running <-all>\n\nUsage: /mission reaccept [MainMissionID]\n\nUsage: /mission finishmain [MainMissionID]";

    public string AllMissionsFinished => "All missions finished!";
    public string AllRunningMissionsFinished => "Total {0} ongoing missions finished!";
    public string MissionFinished => "Mission {0} finished!";
    public string InvalidMissionId => "Invalid mission ID!";
    public string NoRunningMissions => "No ongoing missions!";

    public string RunningMissions => "Ongoing Missions:";
    public string PossibleStuckMissions => "Potentially Stuck Missions:";
    public string MainMission => "Main Mission";

    public string MissionReAccepted => "Re-accepted mission {0}!";
}




public class RelicTextEN
{
    public string Desc => "Manage player's relics.\nMain stat is optional, sub stats are optional, but at least one must exist.\nLevel restriction: 1 ≤ Level ≤ 9999.";

    public string Usage =>
        "Usage: /relic <RelicID> <MainAffixID> <SubAffixID1:SubAffixLevel> <SubAffixID2:SubAffixLevel> <SubAffixID3:SubAffixLevel> <SubAffixID4:SubAffixLevel> l<Level> x<Count>";

    public string RelicNotFound => "Relic not found!";
    public string InvalidMainAffixId => "Invalid main affix ID";
    public string InvalidSubAffixId => "Invalid sub affix ID";
    public string RelicGiven => "Gave player @{0} {1} relic(s) {2}, main stat {3}";
}




public class ReloadTextEN
{
    public string Desc => "Reload specified configuration.\nConfiguration names: banner, activity, videokey, plugin.";
    public string Usage => "Usage: /reload <banner|activity|videokey|plugin>";
    public string ConfigReloaded => "Configuration {0} reloaded!";
}




public class RogueTextEN
{
    public string Desc => "Manage player's Simulated Universe data.\n-1 means all blessings (owned blessings).\nUse buff to obtain blessings.\nUse enhance to enhance blessings.";

    public string Usage =>
        "Usage: /rogue money [Cosmic Fragment amount]\n\nUsage: /rogue buff [BlessingID/-1]\n\nUsage: /rogue miracle [CurioID]\n\nUsage: /rogue enhance [BlessingID/-1]\n\nUsage: /rogue unstuck - Leave event";

    public string PlayerGainedMoney => "Player gained {0} Cosmic Fragments";
    public string PlayerGainedAllItems => "Player gained all {0}";
    public string PlayerGainedItem => "Player gained {0} {1}";
    public string PlayerEnhancedBuff => "Player enhanced blessing {0}";
    public string PlayerEnhancedAllBuffs => "Player enhanced all blessings";
    public string PlayerUnstuck => "Player left the event";
    public string NotFoundItem => "{0} not found!";
    public string PlayerNotInRogue => "Player is not in Simulated Universe!";
}




public class SceneTextEN
{
    public string Desc =>
        "Manage player's scene.\n" +
        "Note: Most of these are for debugging. Before using commands, ensure you know what you are doing!\n" +
        "Use prop to set prop state. Get state list from Common/Enums/Scene/PropStateEnum.cs.\n" +
        "Use unlockall to unlock all props in the scene (set all props that can be set to open state to open). This command may cause the game to load stuck at about 90%. Use /scene reset <floorId> to solve.\n" +
        "Use change to enter a specific scene. To get EntryId, visit Resources/MapEntrance.json.\n" +
        "Use reload to reload the current scene and return to the starting position.\n" +
        "Use reset to reset all prop states in a specified scene. To get current FloorId, use /scene cur.";

    public string Usage =>
        "Usage: /scene group\n\nUsage: /scene prop <GroupID> <PropID> <State>\n\nUsage: /scene remove <EntityID>\n\nUsage: /scene unlockall\n\nUsage: /scene unlockallgroup\n\nUsage: /scene change <entryId>\n\nUsage: /scene reload\n\nUsage: /scene reset <floorId>\n\nUsage: /scene fsv <Name> <Value>\n\nUsage: /scene gp <GroupID> <Name> <Value>\n\nUsage: /scene cur\n\nUsage: /scene near\n\nUsage: /scene forward <Distance>";

    public string LoadedGroups => "Loaded groups: {0}";
    public string PropStateChanged => "Prop: {0} state set to {1}";
    public string PropNotFound => "Prop not found!";
    public string EntityRemoved => "Entity {0} removed";
    public string EntityNotFound => "Entity not found!";
    public string AllPropsUnlocked => "All props unlocked!";
    public string SceneChanged => "Entered scene {0}";
    public string SceneReloaded => "Scene reloaded!";
    public string SceneReset => "Reset all prop states in scene {0}!";
    public string CurrentScene => "Current scene Entry Id: {0}, Plane Id: {1}, Floor Id: {2}";
    public string FSVSet => "Set {0} to {1}";
    public string Teleported => "Teleported!";
}




public class MailTextEN
{
    public string Desc => "Manage player's mail";
    public string Usage => "Usage: /mail <Sender> <TemplateID> <ExpireDays> _TITLE <Title> _CONTENT <Content> _ATTACH <ItemID> [x<count>] [r<rank/eidolon>] [s<superimposition>] [l<level>] [p<promotion>] [t<talentLevel|max>] ...\nUsage: /mail all <Sender> <TemplateID> <ExpireDays> _TITLE <Title> _CONTENT <Content> _ATTACH ...\nUsage: /mail welcome [all]";
    public string MailSent => "Mail sent!";
    public string MailSentWithAttachment => "Mail with attachment sent!";
}




public class RaidTextEN
{
    public string Desc => "Manage player's mission temporary scene";
    public string Usage => "Usage: /raid leave\n\nUsage: /raid reset <RaidID> <Level>";
    public string Leaved => "Left temporary scene!";
}




public class AccountTextEN
{
    public string Desc => "Manage accounts.\nNote: This command is untested. Use with caution!";
    public string Usage => "Usage: /account create <Username> <Email> [Password] [UID]\nUsage: /account create <Username> <Email> [--password <Password>] [--uid <UID>]\nUsage: /account setpassword <Username|UID|Email> <Password>\nUsage: /account setemail <Username|UID|Email> <Email> [verified:true|false]\nUsage: /account verifyemail <Username|UID|Email>\nUsage: /account ban <Username|UID|Email> <permanent|duration> [reason]\nUsage: /account unban <Username|UID|Email>\nUsage: /account baninfo <Username|UID|Email>\nUsage: /account reset [Username|UID|Email]\nUsage: /account delete <Username|UID|Email> confirm\nUsage: /account remove <Username|UID|Email> confirm\nUsage: /account role <Username|UID|Email> <Role>\nUsage: /account setrole <Username|UID|Email> <Role>\nUsage: /account roles";
    public string SetPasswordUsage => "Usage: /account setpassword <Username|UID|Email> <Password>";
    public string SetEmailUsage => "Usage: /account setemail [Username|UID|Email] [Email] [verified:true|false]";
    public string VerifyEmailUsage => "Usage: /account verifyemail [Username|UID|Email]";
    public string BanUsage => "Usage: /account ban [Username|UID|Email] [permanent|duration] [reason]";
    public string BanDurationExamples => "Duration examples: 30m, 2h, 7d, 1w.";
    public string UnbanUsage => "Usage: /account unban [Username|UID|Email]";
    public string BanInfoUsage => "Usage: /account baninfo [Username|UID|Email]";
    public string RoleUsage => "Usage: /account role <Username|UID|Email> <Role>";
    public string ResetUsage => "Usage: /account reset [Username|UID|Email]";
    public string AccountNotFound => "Account {0} does not exist.";
    public string ResetStarted => "Resetting account {0}. You will be disconnected and can reconnect to start fresh.";
    public string ResetSuccess => "Account {0} has been reset.";
    public string DeleteUsage => "Usage: /account delete|remove <Username|UID|Email> confirm";
    public string DeleteRequiresConfirm => "This will permanently delete account {0} and all player data. Run /account delete {0} confirm to continue.";
    public string DeleteStarted => "Deleting account {0}. You will be disconnected.";
    public string DeleteSuccess => "Account {0} has been deleted.";
    public string InvalidUid => "Invalid UID parameter!";
    public string CreateError => "Internal error occurred {0} ";
    public string CreateSuccess => "New account {0} created successfully!";
    public string DuplicateAccount => "Account {0} already exists!";
    public string DuplicateUID => "UID {0} already exists!";
    public string DataError => "Failed to get new account data! {0}!";
}




public class UnstuckTextEN
{
    public string Desc => "Teleport an offline player back to the default scene. Console/admin oriented; the target must be offline.";
    public string Usage => "Usage: /unstuck <offline UID>";
    public string UnstuckSuccess => "Successfully teleported the player back to the default scene";
    public string UidNotExist => "This UID does not exist!";
    public string PlayerIsOnline => "The player is currently online!";
}




public class SetlevelTextEN
{
    public string Desc => "Set player level";
    public string Usage => "Usage: /setlevel <level 1-70>";
    public string SetlevelSuccess => "Level set successfully!";
}




public class GridTextEN
{
    public string Desc => "Manage Aetherium War content. Note: This part is not yet fully developed. Please report any issues promptly.\nThis command may not check if IDs exist. If it has no effect, please check if parameters are correct.";
    public string Usage => "Usage: /grid gold [Gold Amount]\n\n" +
                           "Usage: /grid role [RoleID] [Role Star]\n\n" +
                           "Usage: /grid equip [EquipmentID]\n\n" +
                           "Usage: /grid consumable [ConsumableID]\n\n" +
                           "Usage: /grid orb [OrbID]\n\n" +
                           "Usage: /grid section [ChapterID] [SectionID]";
    public string NotInGame => "Not in Aetherium War!";
    public string InvalidRole => "Role ID or Star does not exist!";
    public string InvalidEquipment => "Equipment ID does not exist!";
    public string InvalidOrb => "Orb ID does not exist!";
    public string InvalidConsumable => "Consumable ID does not exist!";
    public string AddedRole => "Role added.";
    public string UpdateGold => "Gained {0} Gold.";
    public string AddEquipment => "Added {0} equipment.";
    public string AddOrb => "Added {0} orb.";
    public string AddConsumable => "Added {0} consumable.";
    public string EnterSection => "Entered {0}-{1}.";
}




public class JsonTextEN
{
    public string Desc => "Import avatar/relic/light cone data from JSON, or clear existing relics and light cones.";
    public string Usage => "Usage: /json\n\nUsage: /json <filename|index|path>\n\nUsage: /json clear\n\nUsage: /json export [name] [format=true|false]";
    public string ClearInventory => "Cleared relics and light cones for the current player.";
    public string FileNotFound => "File not found: {0}";
    public string ReadOrParseFailed => "Failed to read or parse JSON: {0}";
    public string InvalidJsonContent => "JSON content is invalid or empty.";
    public string ImportSummary => "Import finished: {0}, avatars {1}, relics {2}, light cones {3}.";
    public string NoFileFoundWithHint => "No importable JSON files found. Put files in Config/Json first.";
    public string InvalidChoice => "Invalid index. Available range: 1 ~ {0}";
    public string NoFileFound => "No importable JSON files found.";
    public string SearchedDirectories => "Searched directories:";
    public string SearchedDirectoryItem => "- {0}";
    public string FoundFiles => "Found the following JSON files:";
    public string FileListItem => "{0}. {1}";
    public string UsageSelectHint => "Use /json [index] or /json [filename] to import.";
    public string AvatarExcelNotFound => "Avatar config not found: {0}";

    public string ExportSuccess => "Export successful, file: {0}";
}




public class AnnoTextEN
{
    public string Desc => "Send a server popup announcement.";
    public string Usage =>
        "Usage: /anno [duration] [frequency] <announcement content>\n\n" +
        "Examples:\n" +
        "  /anno 10m Server restart soon\n" +
        "  /anno 2h 5m Double drops are live\n" +
        "  /anno @10001 10m Private notice\n\n" +
        "Options:\n" +
        "  --duration <time>    How long the announcement remains valid. Examples: 30s, 10m, 2h, 3d\n" +
        "  --frequency <time>   How often the banner can reappear. Examples: 30s, 10m, 1h";
    public string Disabled => "Announcement feature is disabled. Enable ServerOption.ServerAnnounce.EnableAnnounce in config first.";
    public string InvalidDuration => "Invalid announcement duration. Use values like 30s, 10m, 2h, or 3d.";
    public string InvalidFrequency => "Invalid announcement frequency. Use values like 30s, 10m, 2h, or 3d.";
    public string Sent => "Sent popup message to {0} online player(s). Duration: {1}. Frequency: {2}.";
}




public class CalyxTextEN
{
    public string Desc => "Toggle calyx override mode that replaces calyx battles with freesr file content.";
    public string Usage => "Usage: /calyx on\nUsage: /calyx off";
    public string ToggleOn => "Calyx override is ON.";
    public string ToggleOff => "Calyx override is OFF.";
    public string MissingIndex => "Please provide a freesr file index. Usage: /calyx on [index]";
    public string InvalidMode => "Unknown mode '{0}'. Use /calyx on or /calyx off.";
    public string LoadFailed => "Failed to load freesr file.";
    public string NoCachedData => "No cached freesr data. Run /json <file> or sync via srtools first.";
}




public class ClearTextEN
{
    public string Desc => "Remove unequipped light cones or relics from the target player's inventory.";
    public string Usage => "Usage: /clear equipment\n\nUsage: /clear relic";
    public string ClearEquipment => "Removed {0} unequipped light cone(s).";
    public string ClearRelic => "Removed {0} unequipped relic(s).";
}




public class DebugTextEN
{
    public string Desc => "Debug battle setup and custom packet loading.";
    public string Usage => "Usage: /debug specific <StageID>\n\nUsage: /debug monster <MonsterID>\n\nUsage: /debug customP <PacketJsonPath>";
    public string InvalidStageId => "Invalid stage or monster ID.";
    public string SetStageId => "Debug battle override updated.";
    public string CustomPacketFileNotFound => "Custom packet file not found.";
    public string CustomPacketFileInvalid => "Custom packet file is invalid or empty.";
    public string CustomPacketFileLoaded => "Custom packet queue loaded.";
}




public class RemoveTextEN
{
    public string Desc => "Admin-only removal for owned characters and light cones.";
    public string Usage => "Usage: /remove character <AvatarID> confirm\n\nUsage: /remove avatar <AvatarID> confirm\n\nUsage: /remove lightcone <UniqueId|ItemID> confirm\n\nUsage: /remove equipment <UniqueId|ItemID> confirm\n\nAdd --dry-run to preview the removal.";
    public string RequiresConfirm => "This is destructive. Add confirm to run it, or --dry-run to preview.";
    public string NotFound => "{0} {1} was not found.";
    public string DryRun => "Dry run: would remove {0} {1}(s).";
    public string Success => "Removed {0} {1}(s). Reconnect may be required for the client to fully refresh.";
}




public class WindyTextEN
{
    public string Desc => "Load and send Lua bytecode to the target player.";
    public string Usage => "Usage: /windy <lua path relative to the Lua folder>";
    public string Loaded => "Read BYTECODE from Lua script: {0}";
    public string ReadError => "Error reading Lua script: {0}";
}

#endregion

#endregion

