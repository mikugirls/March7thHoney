using March7thHoney.Data;
using March7thHoney.Database.Avatar;
using March7thHoney.GameServer.Game.Battle;
using March7thHoney.GameServer.Game.Lineup;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.Proto;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Game.GridFight.Battle;

public static class GridFightBattleProtoBuilder
{
    public static List<AvatarLineupData> HandleProto(BattleInstance battle, GridFightInstance inst, SceneBattleInfo proto)
    {
        var enc = GridFightLevelResolver.Resolve(inst);
        var player = battle.Player;
        var foregroundData = new List<AvatarLineupData>();

        var collection = new PlayerDataCollection(player.Data, player.InventoryManager!.Data, battle.Lineup);

        foreach (var (roleId, _) in inst.ResolveForegroundRoles())
        {
            var resolved = ResolveBattleAvatar(player, roleId);
            var avatar = resolved.Avatar;
            if (avatar == null) continue;
            proto.BattleAvatarList.Add(avatar.ToBattleProto(collection, resolved.AvatarType));
            foregroundData.Add(new AvatarLineupData(avatar, resolved.AvatarType));
        }

        proto.MonsterWaveList.Clear();
        
        
        var monsterWorldLevel = (uint)Math.Max(1, battle.Player.Data.WorldLevel - 1);
        var wave = new SceneMonsterWave
        {
            BattleStageId = (uint)battle.StageId,
            BattleWaveId = 1,
            MonsterParam = new SceneMonsterWaveParam
            {
                EliteGroup = enc.EliteGroupId,
                BDCCEFHMFHO = monsterWorldLevel
            }
        };
        foreach (var spec in enc.Monsters)
        {
            var sceneMonster = new SceneMonster
            {
                MonsterId = spec.MonsterId,
                ExtraInfo = new MEHAOMGBOMC
                {
                    AFCMOOFGBPK = new DLGEGGCHCID { RoleStar = spec.RoleStar }
                }
            };
            foreach (var itemId in spec.DropItemIds)
            {
                sceneMonster.ExtraInfo.AFCMOOFGBPK.PGNMDJIIKJB.Add(new LHPPIAKKFME
                {
                    BGKDAMDFFKH = GridFightDropType.HiolcnpoponMkppcdpchie,
                    JJFFLMCCCMM = itemId,
                    Num = 1
                });
            }
            wave.MonsterList.Add(sceneMonster);
        }
        proto.MonsterWaveList.Add(wave);

        foreach (var buffId in enc.BindingBuffs)
            battle.Buffs.Add(new MazeBuff((int)buffId, 1, -1) { WaveFlag = -1 });

        foreach (var beId in enc.BattleEvents)
            battle.BattleEvents.TryAdd((int)beId, new BattleEventInstance((int)beId, 0, 100000));

        return foregroundData;
    }

    internal static (BaseAvatarInfo? Avatar, AvatarType AvatarType) ResolveBattleAvatar(PlayerInstance player, uint roleId)
    {
        if (!GameData.GridFightRoleBasicInfoData.TryGetValue(roleId, out var basicInfo))
            return (null, AvatarType.AvatarTrialType);

        var formal = player.AvatarManager?.GetFormalAvatar((int)basicInfo.AvatarID);
        if (formal != null)
            return (formal, AvatarType.AvatarFormalType);

        var trial = player.AvatarManager?.GetTrialAvatarByWorldLevel((int)basicInfo.SpecialAvatarID, player.Data.WorldLevel);
        return (trial, AvatarType.AvatarTrialType);
    }

}
