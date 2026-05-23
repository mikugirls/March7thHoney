using March7thHoney.Data;
using March7thHoney.Database.Lineup;
using March7thHoney.GameServer.Game.Battle;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.Proto;
using LineupInfo = March7thHoney.Database.Lineup.LineupInfo;

namespace March7thHoney.GameServer.Game.GridFight.Battle;

public static class GridFightBattleModule
{
    public static BattleInstance? StartBattle(PlayerInstance player, GridFightInstance gridFightInstance)
    {
        if (player.BattleInstance != null)
            return player.BattleInstance;

        var encounter = GridFightLevelResolver.Resolve(gridFightInstance);
        var ok = GameData.StageConfigData.TryGetValue((int)encounter.StageId, out var stageConfig);
        if (!ok)
            ok = GameData.StageConfigData.TryGetValue((int)GridFightLevelResolver.UnifiedStageId, out stageConfig);
        if (!ok)
            stageConfig = GameData.StageConfigData.Values.FirstOrDefault();
        if (stageConfig == null)
            return null;

        var foregroundIds = gridFightInstance.BuildForegroundAvatarIds();
        var backgroundIds = gridFightInstance.BuildBackgroundAvatarIds();

        var tempLineup = new LineupInfo
        {
            LineupType = (int)ExtraLineupType.LineupChessRogue,
            BaseAvatars = foregroundIds.Concat(backgroundIds)
                .Select(id => new LineupAvatarInfo { BaseAvatarId = id })
                .ToList(),
            AvatarData = player.AvatarManager?.AvatarData
        };

        var battle = new BattleInstance(player, tempLineup, [stageConfig])
        {
            WorldLevel = player.Data.WorldLevel,
            GridFightContext = gridFightInstance
        };

        player.BattleInstance = battle;
        return battle;
    }
}
