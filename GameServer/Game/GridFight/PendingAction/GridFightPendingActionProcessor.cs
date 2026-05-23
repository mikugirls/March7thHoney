using March7thHoney.Data;
using March7thHoney.GameServer.Game.GridFight.Sync;
using March7thHoney.GameServer.Game.GridFight.Battle;
using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.GameServer.Server;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Game.GridFight.PendingAction;

public static class GridFightPendingActionProcessor
{
    public static async System.Threading.Tasks.Task Handle(Connection connection, GridFightInstance inst, GridFightSelectRecommendEquipCsReq req)
    {
        var ackPos = req.QueuePosition;
        if (req.PortalBuffRerollAction != null)
        {
            await HandlePortalBuffReroll(connection, inst, ackPos);
            return;
        }
        if (req.PortalBuffAction != null)
        {
            await HandlePortalBuffSelect(connection, inst, ackPos, req.PortalBuffAction.SelectPortalBuffId);
            return;
        }
        if (req.RerollAugmentAction != null)
        {
            await HandleAugmentReroll(connection, inst, ackPos, req.RerollAugmentAction.AugmentId);
            return;
        }
        if (req.AugmentAction != null)
        {
            await HandleAugmentSelect(connection, inst, ackPos, req.AugmentAction.AugmentId);
            return;
        }
        if (req.RerollSupplyAction != null)
        {
            await HandleSupplyReroll(connection, inst, ackPos);
            return;
        }
        if (req.SupplyAction != null)
        {
            await HandleSupplySelect(connection, inst, ackPos, req.SupplyAction.SelectSupplyIndexes);
            return;
        }
        if (req.HGKMHOOFNDA != null)
        {
            await HandleEliteBranchReroll(connection, inst, ackPos);
            return;
        }
        if (req.EliteBranchAction != null)
        {
            await HandleEliteBranchSelect(connection, inst, ackPos, req.EliteBranchAction.EliteBranchId);
            return;
        }
        if (req.RoundBeginAction != null)
        {
            await HandleRoundBegin(connection, inst, ackPos);
            return;
        }
        if (req.ReturnPreparationAction != null)
        {
            await HandleReturnPreparation(connection, inst, ackPos);
            return;
        }

        inst.QueuePosition = ackPos + 1;
        inst.PendingAction = new GridFightPendingAction
        {
            QueuePosition = inst.QueuePosition,
            RoundBeginAction = new GridFightRoundBeginActionInfo()
        };
        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(connection.Player!, GridFightSyncKind.PendingAdvance, (ackPos, inst.QueuePosition)));
    }

    private static async System.Threading.Tasks.Task HandlePortalBuffReroll(Connection connection, GridFightInstance inst, uint ackPos)
    {
        var newBuffList = inst.RollPortalBuffs(exclude: inst.CurrentPortalBuffOffer.ToList());
        inst.QueuePosition = ackPos;
        inst.PendingAction = new GridFightPendingAction
        {
            QueuePosition = ackPos,
            PortalBuffAction = new GridFightPortalBuffActionInfo { FCHPJKAIBHB = 1, GridFightPortalBuffList = { newBuffList } }
        };

        var notify = new GridFightSeasonHandBookNotify { HandbookGridFightPortalInfo = new GridFightHandBookPortalInfo() };
        foreach (var buffId in newBuffList) notify.HandbookGridFightPortalInfo.PELJLONLDNM.Add(buffId);
        await connection.SendPacket(new PacketGridFightSeasonHandBookNotify(notify));
        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));

        var sync = new GridFightSyncUpdateResultScNotify();
        var pendingSection = new GridFightSyncResultData();
        pendingSection.UpdateDynamicList.Add(new GridFightSyncData { PendingAction = inst.PendingAction });
        sync.SyncResultDataList.Add(pendingSection);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(sync));
    }

    private static async System.Threading.Tasks.Task HandlePortalBuffSelect(Connection connection, GridFightInstance inst, uint ackPos, uint buffId)
    {
        if (buffId > 0 && !inst.ActivePortalBuffIds.Contains(buffId)) inst.ActivePortalBuffIds.Add(buffId);
        inst.ClearPortalBuffOffer();

        uint requiredTrait = 0;
        if (GameData.GridFightPortalBuffData.TryGetValue(buffId, out var buffData)
            && buffData.PortalGameRefTrait.Count > 0)
        {
            requiredTrait = buffData.PortalGameRefTrait[0];
        }
        inst.MaterializeInitialBenchTeam(requiredTrait);
        var encounter = GridFightLevelResolver.Resolve(inst);
        inst.ConfigureNextBattle(encounter.StageId, encounter.Monsters.Select(m => m.MonsterId));

        var nextPos = ackPos + 1;
        inst.QueuePosition = nextPos;
        inst.PendingAction = new GridFightPendingAction
        {
            QueuePosition = nextPos,
            RoundBeginAction = new GridFightRoundBeginActionInfo()
        };

        var handbook = new GridFightSeasonHandBookNotify { HandbookGridFightPortalInfo = new GridFightHandBookPortalInfo() };
        handbook.HandbookGridFightPortalInfo.GridFightPortalBuffList.Add(buffId);
        await connection.SendPacket(new PacketGridFightSeasonHandBookNotify(handbook));
        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));

        var sync = new GridFightSyncUpdateResultScNotify();

        var grantedEquipIds = new List<uint>();
        if (GameData.GridFightPortalBuffData.TryGetValue(buffId, out var portalBuffData))
        {
            
            foreach (var bonusId in portalBuffData.ShowBonusIDList)
            {
                if (GameData.GridFightEquipmentData.ContainsKey(bonusId))
                    grantedEquipIds.Add(bonusId);
            }
        }
        if (grantedEquipIds.Count > 0)
        {
            var sec0 = new GridFightSyncResultData { GridUpdateSrc = GridFightUpdateSrcType.LnpfefkjdhpPnejelgglej };
            sec0.SyncEffectParamList.Add(buffId);
            sec0.SyncEffectParamList.Add(0u);
            var addItemInfo = new GridFightGameItemSyncInfo();
            foreach (var equipId in grantedEquipIds)
            {
                var equip = new GridFightEquipmentInfo
                {
                    GridFightEquipmentId = equipId,
                    Source = 1,
                    UniqueId = inst.AllocEquipUniqueId()
                };
                inst.Equipments.Add(equip);
                addItemInfo.GridFightEquipmentList.Add(equip);
            }
            sec0.UpdateDynamicList.Add(new GridFightSyncData { AddGameItemInfo = addItemInfo });
            sync.SyncResultDataList.Add(sec0);
        }

        var sec2 = new GridFightSyncResultData { GridUpdateSrc = GridFightUpdateSrcType.LnpfefkjdhpHndkhmefaal };
        sec2.UpdateDynamicList.Add(new GridFightSyncData { PortalServerDataUpdate = new GridFightPortalServerDataUpdate { PortalBuffId = buffId } });
        sync.SyncResultDataList.Add(sec2);

        var sec3 = new GridFightSyncResultData { GridUpdateSrc = GridFightUpdateSrcType.LnpfefkjdhpBjdeaahibge };
        sec3.UpdateDynamicList.Add(new GridFightSyncData { ItemValue = inst.Gold });
        foreach (var (avatarId, pos, uniqueId, component) in inst.RolledBenchRoles)
        {
            sec3.UpdateDynamicList.Add(new GridFightSyncData
            {
                AddRoleInfo = new GridGameRoleInfo { Id = avatarId, Pos = pos, RoleStar = 1, UniqueId = uniqueId, GridFightValueInitComponent = { [component] = 0 } }
            });
        }
        sync.SyncResultDataList.Add(sec3);

        var sec4 = new GridFightSyncResultData();
        sec4.UpdateDynamicList.Add(new GridFightSyncData { FinishPendingActionPos = ackPos });
        sec4.UpdateDynamicList.Add(new GridFightSyncData { SyncLockInfo = new GridFightLockInfo() });
        sync.SyncResultDataList.Add(sec4);

        var sec5 = new GridFightSyncResultData();
        sec5.UpdateDynamicList.Add(new GridFightSyncData
        {
            LevelSyncInfo = new GridFightLevelSyncInfo
            {
                DCPKPNLKGMM = inst.CurrentChapterId,
                SectionId = inst.SectionId,
                GridFightLayerInfo = new GridFightLayerInfo
                {
                    RouteInfo = new GridFightRouteInfo
                    {
                        FightCampId = 20,
                        EliteBranchId = 1,
                        RouteEncounterList =
                        {
                            new GridFightEncounterInfo
                            {
                                LFKBMDHKPFI = inst.BattleComponent.StageId,
                                MonsterWaveList =
                                {
                                    new GridEncounterMonsterWave
                                    {
                                        IGMMPDDCJIN = 1,
                                        PPOEDDFFEKK =
                                        {
                                            inst.BattleComponent.MonsterIds.Select(x => new PJLBDMPEKFP { MonsterId = x, RoleStar = 1 })
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        });
        sync.SyncResultDataList.Add(sec5);

        var sec6 = new GridFightSyncResultData();
        sec6.UpdateDynamicList.Add(new GridFightSyncData
        {
            SyncLockInfo = new GridFightLockInfo
            {
                LockReason = GridFightLockReason.DfofffceffoKjmjdbjmbmc,
                LockType = GridFightLockType.PjbmhhnlclbEhfhdgpocnh
            }
        });
        sec6.UpdateDynamicList.Add(new GridFightSyncData { PendingAction = inst.PendingAction });
        sync.SyncResultDataList.Add(sec6);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(sync));
    }

    private static async System.Threading.Tasks.Task HandleRoundBegin(Connection connection, GridFightInstance inst, uint ackPos)
    {
        inst.RotateShop();
        var nextPos = ackPos + 1;
        var next = inst.BuildSectionEntryPending(nextPos);
        inst.QueuePosition = next.QueuePosition;
        inst.PendingAction = next;
        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));

        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(connection.Player!, GridFightSyncKind.PendingAdvance, (ackPos, next.QueuePosition)));
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(connection.Player!, GridFightSyncKind.RefreshShop));
    }

    private static GridFightSyncUpdateResultScNotify BuildEliteBranchRouteSync(GridFightInstance inst)
    {
        var notify = new GridFightSyncUpdateResultScNotify();
        var routeSec = new GridFightSyncResultData();
        var routeInfo = new GridFightRouteInfo { FightCampId = 1u };
        foreach (var opt in inst.CurrentEliteBranchOptions)
        {
            var enc = new GridFightEncounterInfo
            {
                BAGCBHFJIMN = opt.EncounterId,
                LFKBMDHKPFI = opt.PenaltyRuleId
            };
            if (opt.DifficultyTier > 1) enc.GDOEOGMJDAO = (opt.DifficultyTier - 1) * 10u;
            var isItemReward = opt.RewardItemId >= 1000;
            var wave = new GridEncounterMonsterWave { IGMMPDDCJIN = 1 };
            foreach (var mid in opt.MonsterIds)
                wave.PPOEDDFFEKK.Add(new PJLBDMPEKFP { MonsterId = mid, RoleStar = 1 });
            enc.MonsterWaveList.Add(wave);
            var drop = new GridFightDropInfo();
            var dropItem = new LHPPIAKKFME
            {
                BGKDAMDFFKH = isItemReward ? GridFightDropType.HiolcnpoponBikhoegfefd : GridFightDropType.HiolcnpoponHhnbgnfbdho,
                Num = opt.RewardCount
            };
            if (isItemReward) dropItem.JJFFLMCCCMM = opt.RewardItemId;
            drop.PIBLJLBCKJL.Add(dropItem);
            enc.LMLAOPMDCCA = drop;
            routeInfo.RouteEncounterList.Add(enc);
        }
        routeSec.UpdateDynamicList.Add(new GridFightSyncData
        {
            LevelSyncInfo = new GridFightLevelSyncInfo
            {
                DCPKPNLKGMM = inst.CurrentChapterId,
                GridFightLayerInfo = new GridFightLayerInfo { RouteInfo = routeInfo }
            }
        });
        notify.SyncResultDataList.Add(routeSec);
        return notify;
    }

    private static async System.Threading.Tasks.Task HandleReturnPreparation(Connection connection, GridFightInstance inst, uint ackPos)
    {
        var nextPos = ackPos + 1;
        inst.QueuePosition = nextPos;
        inst.PendingAction = null;
        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(connection.Player!, GridFightSyncKind.PendingAdvance, (ackPos, nextPos)));

        if (GridFightLevelResolver.IsCombatNode(inst) && inst.LastEliteBranchConsumedSection != inst.SectionId)
        {
            var encounter = GridFightLevelResolver.Resolve(inst);
            inst.ConfigureNextBattle(encounter.StageId, encounter.Monsters.Select(m => m.MonsterId));
        }
    }

    private static async System.Threading.Tasks.Task HandleAugmentReroll(Connection connection, GridFightInstance inst, uint ackPos, uint augmentId)
    {
        if (inst.CurrentAugmentOffer.Contains(augmentId))
        {
            var idx = inst.CurrentAugmentOffer.IndexOf(augmentId);
            var pool = March7thHoney.GameServer.Game.GridFight.GridFightManager.AugmentPoolKD;
            var rng = Random.Shared;
            uint replacement;
            var attempts = 0;
            do { replacement = pool[rng.Next(pool.Length)]; attempts++; }
            while (inst.CurrentAugmentOffer.Contains(replacement) && attempts < 32);
            inst.CurrentAugmentOffer[idx] = replacement;
            if (inst.CurrentAugmentReroll > 0) inst.CurrentAugmentReroll--;
        }

        inst.PendingAction = new GridFightPendingAction
        {
            QueuePosition = ackPos,
            AugmentAction = BuildAugmentPendingInfo(inst.CurrentAugmentOffer)
        };

        var sync = new GridFightSyncUpdateResultScNotify();
        var sec = new GridFightSyncResultData();
        sec.UpdateDynamicList.Add(new GridFightSyncData { PendingAction = inst.PendingAction });
        sync.SyncResultDataList.Add(sec);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(sync));
        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));
    }

    private static async System.Threading.Tasks.Task HandleAugmentSelect(Connection connection, GridFightInstance inst, uint ackPos, uint augmentId)
    {
        var grantedRoleId = 0u;
        var grantedRoleUniqueId = 0u;
        uint grantedPos = 0;
        string? grantedComponent = null;
        if (augmentId > 0)
        {
            var rng = Random.Shared;
            var rolePool = GameData.GridFightRoleBasicInfoData.Values
                .Where(r => r.IsInPool && (r.SeasonID == inst.Season || r.SeasonID == 0))
                .Where(r => r.AvatarID >= 1000 && r.AvatarID < 2000)
                .Where(r => r.RoleSavedValueList.Count > 0)
                .ToList();
            if (rolePool.Count > 0)
            {
                var pick = rolePool[rng.Next(rolePool.Count)];
                grantedRoleId = pick.AvatarID;
                grantedComponent = pick.RoleSavedValueList[0];
                grantedRoleUniqueId = inst.AllocRoleUniqueId();
                for (uint p = 14; p <= 18; p++)
                {
                    if (inst.UniqueIdByPos.ContainsKey(p)) continue;
                    grantedPos = p; break;
                }
                if (grantedPos == 0) grantedPos = 14;
                inst.RoleByUniqueId[grantedRoleUniqueId] = grantedRoleId;
                inst.RoleStarByUniqueId[grantedRoleUniqueId] = 1;
                inst.UniqueIdByPos[grantedPos] = grantedRoleUniqueId;
            }
        }

        inst.LastAugmentConsumedSection = inst.SectionId;
        inst.ClearAugmentOffer();
        if (augmentId > 0 && !inst.ActiveAugmentIds.Contains(augmentId))
            inst.ActiveAugmentIds.Add(augmentId);

        var nextPos = ackPos + 1;
        inst.QueuePosition = nextPos;
        inst.PendingAction = new GridFightPendingAction
        {
            QueuePosition = nextPos,
            ReturnPreparationAction = new GridFightReturnPreparationActionInfo()
        };

        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));

        var sync = new GridFightSyncUpdateResultScNotify();
        if (grantedRoleId > 0)
        {
            var roleSec = new GridFightSyncResultData { GridUpdateSrc = GridFightUpdateSrcType.LnpfefkjdhpLfnkhcbhmpd };
            roleSec.SyncEffectParamList.Add(augmentId);
            var roleInfo = new GridGameRoleInfo
            {
                Id = grantedRoleId,
                Pos = grantedPos,
                UniqueId = grantedRoleUniqueId,
                RoleStar = 1
            };
            if (grantedComponent != null) roleInfo.GridFightValueInitComponent[grantedComponent] = 0;
            roleSec.UpdateDynamicList.Add(new GridFightSyncData { AddRoleInfo = roleInfo });
            sync.SyncResultDataList.Add(roleSec);
        }

        
        if (augmentId > 0)
        {
            var augSec = new GridFightSyncResultData { GridUpdateSrc = GridFightUpdateSrcType.LnpfefkjdhpKcefflkabgh };
            augSec.UpdateDynamicList.Add(new GridFightSyncData
            {
                AugmentSyncInfo = new GridFightGameAugmentAdd
                {
                    UpdateAugmentInfo = new GridGameAugmentInfo
                    {
                        AugmentId = augmentId,
                        MHMLMKDFJLN = true
                    }
                }
            });
            sync.SyncResultDataList.Add(augSec);
        }

        var finishSec = new GridFightSyncResultData();
        finishSec.UpdateDynamicList.Add(new GridFightSyncData { FinishPendingActionPos = ackPos });
        finishSec.UpdateDynamicList.Add(new GridFightSyncData { SyncLockInfo = new GridFightLockInfo() });
        sync.SyncResultDataList.Add(finishSec);
        var nextSec = new GridFightSyncResultData();
        nextSec.UpdateDynamicList.Add(new GridFightSyncData { PendingAction = inst.PendingAction });
        sync.SyncResultDataList.Add(nextSec);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(sync));
    }

    private static async System.Threading.Tasks.Task HandleSupplyReroll(Connection connection, GridFightInstance inst, uint ackPos)
    {
        inst.RollSupplies();
        if (inst.CurrentSupplyReroll > 0) inst.CurrentSupplyReroll--;

        var info = new GridFightSupplyActionInfo
        {
            FCHPJKAIBHB = inst.CurrentSupplyReroll,
            CGFLMCHMBHL = 1
        };
        foreach (var (roleId, equipId) in inst.CurrentSupplyOffer)
        {
            var sup = new GridFightSupplyRoleInfo { RoleId = roleId };
            sup.GridFightItemList.Add(equipId);
            info.SupplyRoleInfoList.Add(sup);
        }
        inst.PendingAction = new GridFightPendingAction { QueuePosition = ackPos, SupplyAction = info };

        var sync = new GridFightSyncUpdateResultScNotify();
        var sec = new GridFightSyncResultData();
        sec.UpdateDynamicList.Add(new GridFightSyncData { PendingAction = inst.PendingAction });
        sync.SyncResultDataList.Add(sec);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(sync));
        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));
    }

    private static async System.Threading.Tasks.Task HandleSupplySelect(Connection connection, GridFightInstance inst, uint ackPos, IList<uint> indexes)
    {
        var grantedRoleId = 0u;
        var grantedRoleUniqueId = 0u;
        var grantedEquipId = 0u;
        var grantedEquipUniqueId = 0u;
        uint grantedPos = 0;
        string? grantedComponent = null;
        var pickIndex = indexes.Count > 0 ? (int)indexes[0] - 1 : -1;
        if (pickIndex >= 0 && pickIndex < inst.CurrentSupplyOffer.Count)
        {
            (grantedRoleId, grantedEquipId) = inst.CurrentSupplyOffer[pickIndex];
            grantedRoleUniqueId = inst.AllocRoleUniqueId();
            grantedEquipUniqueId = inst.AllocEquipUniqueId();
            for (uint p = 14; p <= 18; p++)
            {
                if (inst.UniqueIdByPos.ContainsKey(p)) continue;
                grantedPos = p; break;
            }
            if (grantedPos == 0) grantedPos = 14;
            inst.RoleByUniqueId[grantedRoleUniqueId] = grantedRoleId;
            inst.RoleStarByUniqueId[grantedRoleUniqueId] = 1;
            inst.UniqueIdByPos[grantedPos] = grantedRoleUniqueId;
            inst.Equipments.Add(new GridFightEquipmentInfo
            {
                GridFightEquipmentId = grantedEquipId,
                Source = 1,
                UniqueId = grantedEquipUniqueId
            });
            if (GameData.GridFightRoleBasicInfoData.TryGetValue(grantedRoleId, out var roleExcel)
                && roleExcel.RoleSavedValueList.Count > 0)
                grantedComponent = roleExcel.RoleSavedValueList[0];
        }

        inst.LastSupplyConsumedSection = inst.SectionId;
        inst.ClearSupplyOffer();
        inst.SectionId++;
        if (GridFightLevelResolver.IsCombatNode(inst))
        {
            var nextEncounter = GridFightLevelResolver.Resolve(inst);
            inst.ConfigureNextBattle(nextEncounter.StageId, nextEncounter.Monsters.Select(m => m.MonsterId));
        }
        inst.RotateShop();

        var nextPos = ackPos + 1;
        inst.QueuePosition = nextPos;
        inst.PendingAction = new GridFightPendingAction
        {
            QueuePosition = nextPos,
            ReturnPreparationAction = new GridFightReturnPreparationActionInfo()
        };

        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));

        var sync = new GridFightSyncUpdateResultScNotify();
        if (grantedEquipId > 0)
        {
            var equipSec = new GridFightSyncResultData { GridUpdateSrc = GridFightUpdateSrcType.LnpfefkjdhpFkfklefkbbm };
            equipSec.SyncEffectParamList.Add(indexes.Count > 0 ? indexes[0] : 0u);
            var addItem = new GridFightGameItemSyncInfo();
            addItem.GridFightEquipmentList.Add(new GridFightEquipmentInfo
            {
                GridFightEquipmentId = grantedEquipId,
                Source = 1,
                UniqueId = grantedEquipUniqueId
            });
            equipSec.UpdateDynamicList.Add(new GridFightSyncData { AddGameItemInfo = addItem });
            sync.SyncResultDataList.Add(equipSec);
        }
        if (grantedRoleId > 0)
        {
            var roleSec = new GridFightSyncResultData { GridUpdateSrc = GridFightUpdateSrcType.LnpfefkjdhpFkfklefkbbm };
            roleSec.SyncEffectParamList.Add(indexes.Count > 0 ? indexes[0] : 0u);
            var roleInfo = new GridGameRoleInfo
            {
                Id = grantedRoleId,
                Pos = grantedPos,
                UniqueId = grantedRoleUniqueId,
                RoleStar = 1
            };
            if (grantedComponent != null) roleInfo.GridFightValueInitComponent[grantedComponent] = 0;
            roleSec.UpdateDynamicList.Add(new GridFightSyncData { AddRoleInfo = roleInfo });
            sync.SyncResultDataList.Add(roleSec);
        }
        var finishSec = new GridFightSyncResultData();
        finishSec.UpdateDynamicList.Add(new GridFightSyncData { FinishPendingActionPos = ackPos });
        finishSec.UpdateDynamicList.Add(new GridFightSyncData { SyncLockInfo = new GridFightLockInfo() });
        sync.SyncResultDataList.Add(finishSec);

        var levelSec = new GridFightSyncResultData();
        var routeInfo = new GridFightRouteInfo { EliteBranchId = 1, FightCampId = inst.CampId };
        if (inst.BattleComponent.MonsterIds.Count > 0)
        {
            var encounterInfo = new GridFightEncounterInfo { LFKBMDHKPFI = inst.BattleComponent.StageId };
            var wave = new GridEncounterMonsterWave { IGMMPDDCJIN = 1 };
            foreach (var mid in inst.BattleComponent.MonsterIds)
                wave.PPOEDDFFEKK.Add(new PJLBDMPEKFP { MonsterId = mid, RoleStar = 1 });
            encounterInfo.MonsterWaveList.Add(wave);
            routeInfo.RouteEncounterList.Add(encounterInfo);
        }
        levelSec.UpdateDynamicList.Add(new GridFightSyncData
        {
            LevelSyncInfo = new GridFightLevelSyncInfo
            {
                DCPKPNLKGMM = inst.CurrentChapterId,
                SectionId = inst.SectionId,
                GridFightLayerInfo = new GridFightLayerInfo { RouteInfo = routeInfo }
            }
        });
        sync.SyncResultDataList.Add(levelSec);

        var nextSec = new GridFightSyncResultData();
        nextSec.UpdateDynamicList.Add(new GridFightSyncData { PendingAction = inst.PendingAction });
        sync.SyncResultDataList.Add(nextSec);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(sync));
    }

    private static GridFightAugmentActionInfo BuildAugmentPendingInfo(IEnumerable<uint> augmentIds)
    {
        var info = new GridFightAugmentActionInfo();
        foreach (var aid in augmentIds)
            info.PendingAugmentInfoList.Add(new GridFightPendingAugmentInfo { AugmentId = aid, ALJBADEOPAH = 1 });
        return info;
    }

    private static async System.Threading.Tasks.Task HandleEliteBranchReroll(Connection connection, GridFightInstance inst, uint ackPos)
    {
        inst.RollEliteBranchOptions();
        if (inst.CurrentEliteBranchReroll > 0) inst.CurrentEliteBranchReroll--;

        inst.PendingAction = new GridFightPendingAction
        {
            QueuePosition = ackPos,
            EliteBranchAction = new GridFightEliteBranchActionInfo { FCHPJKAIBHB = inst.CurrentEliteBranchReroll }
        };
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(BuildEliteBranchRouteSync(inst)));
        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));
    }

    private static async System.Threading.Tasks.Task HandleEliteBranchSelect(Connection connection, GridFightInstance inst, uint ackPos, uint eliteBranchId)
    {
        var idx = (int)eliteBranchId - 1;
        if (idx < 0 || idx >= inst.CurrentEliteBranchOptions.Count) idx = 0;
        var chosen = inst.CurrentEliteBranchOptions.Count > 0 ? inst.CurrentEliteBranchOptions[idx] : null;
        if (chosen != null)
        {
            inst.ConfigureNextBattle(chosen.StageId, chosen.MonsterIds);
            
            inst.LastEncounterQuality = chosen.DifficultyTier;
            inst.LastEncounterAppliedSection = inst.SectionId;
            inst.LastEncounterAppliedChapter = inst.CurrentChapterId;
        }

        inst.LastEliteBranchConsumedSection = inst.SectionId;

        var nextPos = ackPos + 1;
        inst.QueuePosition = nextPos;
        inst.PendingAction = new GridFightPendingAction
        {
            QueuePosition = nextPos,
            ReturnPreparationAction = new GridFightReturnPreparationActionInfo()
        };

        await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(ackPos));

        var sync = new GridFightSyncUpdateResultScNotify();
        var pickSec = new GridFightSyncResultData();
        pickSec.UpdateDynamicList.Add(new GridFightSyncData
        {
            EliteBranchSyncInfo = new GridFightEliteBranchSyncInfo { EliteBranchId = eliteBranchId }
        });
        sync.SyncResultDataList.Add(pickSec);

        var finishSec = new GridFightSyncResultData();
        finishSec.UpdateDynamicList.Add(new GridFightSyncData { FinishPendingActionPos = ackPos });
        finishSec.UpdateDynamicList.Add(new GridFightSyncData { SyncLockInfo = new GridFightLockInfo() });
        sync.SyncResultDataList.Add(finishSec);

        if (chosen != null)
        {
            var committedSec = new GridFightSyncResultData();
            var routeInfo = new GridFightRouteInfo { FightCampId = 1u, EliteBranchId = eliteBranchId };
            var enc = new GridFightEncounterInfo
            {
                BAGCBHFJIMN = chosen.EncounterId,
                LFKBMDHKPFI = chosen.PenaltyRuleId
            };
            if (chosen.DifficultyTier > 1) enc.GDOEOGMJDAO = (chosen.DifficultyTier - 1) * 10u;
            var wave = new GridEncounterMonsterWave { IGMMPDDCJIN = 1 };
            foreach (var mid in chosen.MonsterIds)
                wave.PPOEDDFFEKK.Add(new PJLBDMPEKFP { MonsterId = mid, RoleStar = 1 });
            enc.MonsterWaveList.Add(wave);
            routeInfo.RouteEncounterList.Add(enc);
            committedSec.UpdateDynamicList.Add(new GridFightSyncData
            {
                LevelSyncInfo = new GridFightLevelSyncInfo
                {
                    DCPKPNLKGMM = inst.CurrentChapterId,
                    SectionId = inst.SectionId,
                    GridFightLayerInfo = new GridFightLayerInfo { RouteInfo = routeInfo }
                }
            });
            sync.SyncResultDataList.Add(committedSec);
        }

        inst.ClearEliteBranchOptions();

        var nextSec = new GridFightSyncResultData();
        nextSec.UpdateDynamicList.Add(new GridFightSyncData
        {
            SyncLockInfo = new GridFightLockInfo
            {
                LockReason = GridFightLockReason.DfofffceffoKjmjdbjmbmc,
                LockType = GridFightLockType.PjbmhhnlclbEhfhdgpocnh
            }
        });
        nextSec.UpdateDynamicList.Add(new GridFightSyncData { PendingAction = inst.PendingAction });
        sync.SyncResultDataList.Add(nextSec);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(sync));
    }
}
