using March7thHoney.Data;
using March7thHoney.Database.Avatar;
using March7thHoney.GameServer.Game.Battle;
using March7thHoney.GameServer.Game.GridFight;
using March7thHoney.GameServer.Game.GridFight.Battle;
using March7thHoney.GameServer.Game.Lineup;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.GridFight;

public class PacketGridFightEnterBattleStageScRsp : BasePacket
{
    public PacketGridFightEnterBattleStageScRsp(GridFightInstance? inst, BattleInstance? battle) : base(CmdIds.GridFightEnterBattleStageScRsp)
    {
        var proto = new GridFightEnterBattleStageScRsp();
        if (inst == null || battle == null)
            proto.Retcode = (uint)Retcode.RetFail;
        else
            proto.BattleInfo = BuildBattleInfo(inst, battle);
        SetData(proto);
    }

    private static SceneBattleInfo BuildBattleInfo(GridFightInstance inst, BattleInstance battle)
    {
        var info = battle.ToProto();
        var enc = GridFightLevelResolver.Resolve(inst);

        
        
        var placedCount = (uint)inst.UniqueIdByPos.Count(kv => kv.Key is >= 1 and <= 13 && kv.Value != 0);
        info.AFCMOOFGBPK = new OGEOMDJIAGI
        {
            BBDOCJGAEEJ = inst.DivisionId,
            LFKBMDHKPFI = enc.PenaltyBonusRuleId,
            ANBBPPHBCJH = placedCount,
            HDCAEIKAPND = placedCount,
            
            OOPPKDAFFDG = inst.GetEffectiveEnemyDifficultyLevel(),
            Season = inst.Season,
            IsOverlock = inst.IsOverLock,
            GridFightLineupHp = inst.LineupHp,
            OIHHKOJFHFG = new OMBNHGAJANJ
            {
                BNLHIMHFGDK = 1,
                DCPKPNLKGMM = inst.CurrentChapterId,
                NDOCIKPLKIF = inst.NDOCIKPLKIF,
                SectionId = inst.SectionId
            }
        };

        
        foreach (var augmentId in inst.ActiveAugmentIds)
            info.AFCMOOFGBPK.SyncAugmentInfo.Add(new HLPCOGPKBFJ { AugmentId = augmentId });

        foreach (var portalBuffId in inst.ActivePortalBuffIds)
            info.AFCMOOFGBPK.GridFightPortalBuffList.Add(new MMDJJDEJMMN { PortalBuffId = portalBuffId });

        foreach (var (roleId, _) in inst.UniqueIdByPos
                     .OrderBy(kv => kv.Key)
                     .Where(kv => kv.Key > 0 && kv.Key <= 13 && kv.Value != 0 && inst.RoleByUniqueId.ContainsKey(kv.Value))
                     .Select(kv => (RoleId: inst.RoleByUniqueId[kv.Value], Pos: kv.Key)))
        {
            if (!GameData.GridFightRoleBasicInfoData.TryGetValue(roleId, out var basicInfo)) continue;
            foreach (var savedValue in basicInfo.RoleSavedValueList)
                info.AFCMOOFGBPK.OGHGLMGJGEM[savedValue] = 0;
        }
        var player = battle.Player;
        var collection = new PlayerDataCollection(player.Data, player.InventoryManager!.Data, battle.Lineup);
        foreach (var (roleId, _) in inst.ResolveBackgroundRoles())
        {
            var resolved = GridFightBattleProtoBuilder.ResolveBattleAvatar(player, roleId);
            var avatar = resolved.Avatar;
            if (avatar == null) continue;
            var ba = avatar.ToBattleProto(collection, resolved.AvatarType);
            ba.Id = (uint)(GameData.GridFightRoleBasicInfoData.TryGetValue(roleId, out var bi) ? bi.AvatarID : roleId);
            info.AFCMOOFGBPK.PIDIGFGKAMK.Add(ba);
        }

        foreach (var trait in inst.CheckTrait())
            info.AFCMOOFGBPK.GridFightTraitInfo.Add(trait);

        foreach (var (pos, uniqueId) in inst.UniqueIdByPos.OrderBy(kv => kv.Key))
        {
            if (pos == 0 || pos > 13) continue;
            if (uniqueId == 0 || !inst.RoleByUniqueId.TryGetValue(uniqueId, out var roleId)) continue;
            var avatarId = GameData.GridFightRoleBasicInfoData.TryGetValue(roleId, out var bi) ? bi.AvatarID : roleId;

            var roleInfo = new JAJOBJJPINN
            {
                RoleId = roleId,
                AvatarId = avatarId,
                Pos = pos,
                UniqueId = uniqueId,
                RoleStar = inst.RoleStarByUniqueId.GetValueOrDefault(uniqueId, 1u),
                GJEHIGGNIAP = new IFDFHPAMHCL()
            };
            roleInfo.ConvertPropertyToFixpoint.Add(32, 180);
            roleInfo.ConvertPropertyToFixpoint.Add(33, 480);
            roleInfo.ConvertPropertyToFixpoint.Add(34, 440);
            roleInfo.ConvertPropertyToFixpoint.Add(44, 304);
            roleInfo.ConvertPropertyToFixpoint.Add(49, 180);
            roleInfo.ConvertPropertyToFixpoint.Add(52, 240);
            roleInfo.ConvertPropertyToFixpoint.Add(53, 480);
            roleInfo.ConvertPropertyToFixpoint.Add(57, 240);
            roleInfo.ConvertPropertyToFixpoint.Add(59, 450);
            roleInfo.ConvertPropertyToFixpoint.Add(1013, 100);
            roleInfo.ConvertPropertyToFixpoint.Add(1014, 100);
            roleInfo.ConvertPropertyToFixpoint.Add(1019, 100);
            roleInfo.ConvertPropertyToFixpoint.Add(1025, 100);

            if (inst.EquipUniqueIdsByRoleUniqueId.TryGetValue(uniqueId, out var dressed))
            {
                foreach (var equipUid in dressed)
                {
                    var equip = inst.Equipments.FirstOrDefault(e => e.UniqueId == equipUid);
                    if (equip == null) continue;
                    roleInfo.HHIJFHKJEPL.Add(new APCNLFANPEP
                    {
                        GridFightEquipmentId = equip.GridFightEquipmentId,
                        UniqueId = equip.UniqueId
                    });
                }
            }

            info.AFCMOOFGBPK.GridGameRoleList.Add(roleInfo);
        }

        return info;
    }
}
