using March7thHoney.Data;
using March7thHoney.Data.Config.Scene;
using March7thHoney.Enums.Scene;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.GameServer.Game.Scene;
using March7thHoney.GameServer.Game.Scene.Entity;
using March7thHoney.Proto.ServerSide;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Game.Challenge;

public class ChallengeEntityLoader(SceneInstance scene, PlayerInstance player) : SceneEntityLoader(scene)
{
    public PlayerInstance Player = player;

    public override async ValueTask LoadEntity()
    {
        if (Scene.IsLoaded) return;

        
        if (Player.ChallengeManager!.ChallengeInstance == null) return;
        var instance = Player.ChallengeManager.ChallengeInstance;
        LoadGroups.SafeAddRange(Scene.FloorInfo!.Groups.Keys.ToList());

        
        var stages = instance.GetStageMonsters();

        foreach (var stage in stages)
        {
            Scene.FloorInfo.Groups.TryGetValue(stage.Key, out var groupData);
            if (groupData != null) await LoadGroup(groupData);
        }

        Scene.LeaveEntryId = instance.Data.ChallengeTypeCase switch
        {
            
            ChallengeDataPb.ChallengeTypeOneofCase.Boss => GameConstants.CHALLENGE_BOSS_ENTRANCE,
            ChallengeDataPb.ChallengeTypeOneofCase.Memory => GameConstants.CHALLENGE_ENTRANCE,
            ChallengeDataPb.ChallengeTypeOneofCase.Peak => GameConstants.CHALLENGE_PEAK_ENTRANCE,
            ChallengeDataPb.ChallengeTypeOneofCase.Story => GameConstants.CHALLENGE_STORY_ENTRANCE,
            _ => Scene.LeaveEntryId
        };

        foreach (var group in Scene.FloorInfo.Groups.Values)
        {
            
            if (group.LoadSide != GroupLoadSideEnum.Server) continue;

            
            if (group.PropList.Count > 0 && group.MonsterList.Count == 0) await LoadGroup(group);
        }

        Scene.IsLoaded = true;
    }

    public override async ValueTask<EntityMonster?> LoadMonster(MonsterInfo info, GroupInfo group,
        bool sendPacket = false)
    {
        if (info.IsClientOnly || info.IsDelete) return null;

        
        if (Player.ChallengeManager!.ChallengeInstance == null) return null;
        var instance = Player.ChallengeManager.ChallengeInstance;

        
        var stages = instance.GetStageMonsters();

        if (!stages.TryGetValue(group.Id, out var challengeMonsters)) return null;

        
        if (challengeMonsters.All(x => x.ConfigId != info.ID)) return null;
        var challengeMonsterInfo = challengeMonsters.First(x => x.ConfigId == info.ID);

        
        if (!GameData.NpcMonsterDataData.TryGetValue(challengeMonsterInfo.NpcMonsterId, out var npcMonsterExcel))
            return null;

        
        var entity = new EntityMonster(Scene, info.ToPositionProto(), info.ToRotationProto(), group.Id, info.ID,
            npcMonsterExcel, info)
        {
            EventId = challengeMonsterInfo.EventId,
            CustomStageId = challengeMonsterInfo.EventId
        };

        await Scene.AddEntity(entity, sendPacket);

        return entity;
    }

    public override async ValueTask<EntityNpc?> LoadNpc(NpcInfo info, GroupInfo group, bool sendPacket = false)
    {
        await System.Threading.Tasks.Task.CompletedTask;
        return null;
    }
}
