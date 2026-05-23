using March7thHoney.Data;
using March7thHoney.Database;
using March7thHoney.Database.Challenge;
using March7thHoney.Database.Friend;
using March7thHoney.Database.Inventory;
using March7thHoney.GameServer.Game.Challenge.Definitions;
using March7thHoney.GameServer.Game.Challenge.Instances;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.GameServer.Server.Packet.Send.Challenge;
using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Proto;
using March7thHoney.Proto.ServerSide;
using Google.Protobuf;
using static March7thHoney.GameServer.Plugin.Event.PluginEvent;

namespace March7thHoney.GameServer.Game.Challenge;

public class ChallengeManager(PlayerInstance player) : BasePlayerManager(player)
{
    #region Properties

    public BaseChallengeInstance? ChallengeInstance { get; set; }

    public ChallengeData ChallengeData { get; } =
        DatabaseHelper.Instance!.GetInstanceOrCreateNew<ChallengeData>(player.Uid);

    #endregion

    #region Management

    public async ValueTask StartChallenge(int challengeId, ChallengeStoryBuffInfo? storyBuffs,
        ChallengeBossBuffInfo? bossBuffs)
    {
        if (!GameData.ChallengeConfigData.TryGetValue(challengeId, out var excel))
        {
            await Player.SendPacket(new PacketStartChallengeScRsp((uint)Retcode.RetChallengeNotExist));
            return;
        }

        if (excel.StageNum > 0 && !PrepareChallengeLineup(ExtraLineupType.LineupChallenge))
        {
            await Player.SendPacket(new PacketStartChallengeScRsp((uint)Retcode.RetChallengeLineupEmpty));
            return;
        }

        if (excel.StageNum >= 2 && !PrepareChallengeLineup(ExtraLineupType.LineupChallenge2))
        {
            await Player.SendPacket(new PacketStartChallengeScRsp((uint)Retcode.RetChallengeLineupEmpty));
            return;
        }

        var data = new ChallengeDataPb();
        BaseLegacyChallengeInstance instance;
        if (excel.IsBoss())
        {
            data.Boss = new ChallengeBossDataPb
            {
                ChallengeMazeId = (uint)excel.ID,
                CurStatus = 1,
                CurrentStage = 1,
                CurrentExtraLineup = ChallengeLineupTypePb.Challenge1
            };

            instance = new ChallengeBossInstance(Player, data);
        }
        else if (excel.IsStory())
        {
            data.Story = new ChallengeStoryDataPb
            {
                ChallengeMazeId = (uint)excel.ID,
                CurStatus = 1,
                CurrentStage = 1,
                CurrentExtraLineup = ChallengeLineupTypePb.Challenge1
            };

            instance = new ChallengeStoryInstance(Player, data);
        }
        else
        {
            data.Memory = new ChallengeMemoryDataPb
            {
                ChallengeMazeId = (uint)excel.ID,
                CurStatus = 1,
                CurrentStage = 1,
                CurrentExtraLineup = ChallengeLineupTypePb.Challenge1,
                RoundsLeft = (uint)excel.ChallengeCountDown
            };

            instance = new ChallengeMemoryInstance(Player, data);
        }

        ChallengeInstance = instance;

        await Player.LineupManager!.SetExtraLineup((ExtraLineupType)instance.GetCurrentExtraLineupType(), false);
        if (!await TryEnterChallengeScene(excel.MapEntranceID))
        {
            await Player.LineupManager.SetExtraLineup(ExtraLineupType.LineupNone, false);
            ChallengeInstance = null;
            await Player.SendPacket(new PacketStartChallengeScRsp((uint)Retcode.RetChallengeNotExist));
            return;
        }

        if (instance is ChallengeBossInstance boss)
        {
            if (boss.Config.MazeGroupID2 != 0 && boss.Config.MazeGroupID2 != boss.Config.MazeGroupID1)
                await Player.SceneInstance!.EntityLoader!.UnloadGroup(boss.Config.MazeGroupID2);

            if (boss.Config.MazeGroupID1 != 0)
                await Player.SceneInstance!.EntityLoader!.LoadGroup(boss.Config.MazeGroupID1, sendPacket: true);
        }

        await Player.SendPacket(new PacketChallengeLineupNotify((ExtraLineupType)instance.GetCurrentExtraLineupType()));
        await Player.SceneInstance!.SyncLineup();

        instance.SetStartPos(Player.Data.Pos!);
        instance.SetStartRot(Player.Data.Rot!);
        instance.SetSavedMp(Player.LineupManager.GetCurLineup()!.Mp);

        if (excel.IsStory() && storyBuffs != null)
        {
            instance.Data.Story.Buffs.Add(storyBuffs.BuffOne);
            instance.Data.Story.Buffs.Add(storyBuffs.BuffTwo);
        }

        if (bossBuffs != null)
        {
            instance.Data.Boss.Buffs.Add(bossBuffs.BuffOne);
            instance.Data.Boss.Buffs.Add(bossBuffs.BuffTwo);
        }

        InvokeOnPlayerEnterChallenge(Player, instance);

        await Player.SendPacket(new PacketStartChallengeScRsp(Player, sendScene: false));
        await Player.SendPacket(new PacketEnterSceneByServerScNotify(Player.SceneInstance!));

        SaveInstance(instance);
    }

    public void AddHistory(int challengeId, int stars, int score)
    {
        if (stars <= 0) return;

        if (!ChallengeData.History.ContainsKey(challengeId))
            ChallengeData.History[challengeId] = new ChallengeHistoryData(Player.Uid, challengeId);
        var info = ChallengeData.History[challengeId];

        
        info.SetStars(stars);
        info.Score = score;
    }

    public async ValueTask<List<TakenChallengeRewardInfo>?> TakeRewards(int groupId)
    {
        
        if (!GameData.ChallengeGroupData.ContainsKey(groupId)) return null;
        var challengeGroup = GameData.ChallengeGroupData[groupId];

        if (!GameData.ChallengeRewardData.ContainsKey(challengeGroup.RewardLineGroupID)) return null;
        var challengeRewardLine = GameData.ChallengeRewardData[challengeGroup.RewardLineGroupID];

        var totalStars = 0;
        foreach (var challengeExcel in GameData.ChallengeConfigData.Values)
        {
            if (challengeExcel.GroupID != groupId) continue;

            if (ChallengeData.History.TryGetValue(challengeExcel.ID, out var ch))
            {
                ch.GroupId = challengeExcel.GroupID;
                totalStars += ch.GetTotalStars();
            }
        }

        
        var rewardInfos = new List<TakenChallengeRewardInfo>();
        var data = new List<ItemData>();

        
        foreach (var challengeReward in challengeRewardLine)
        {
            
            if (totalStars < challengeReward.StarCount) continue;

            
            if (!ChallengeData.TakenRewards.ContainsKey(groupId))
                ChallengeData.TakenRewards[groupId] = new ChallengeGroupReward(Player.Uid, groupId);
            var reward = ChallengeData.TakenRewards[groupId];

            
            if (reward.HasTakenReward(challengeReward.StarCount)) continue;

            
            if (!GameData.RewardDataData.ContainsKey(challengeReward.RewardID)) continue;
            var rewardExcel = GameData.RewardDataData[challengeReward.RewardID];

            
            var proto = new TakenChallengeRewardInfo
            {
                StarCount = (uint)challengeReward.StarCount,
                Reward = new ItemList()
            };

            if (rewardExcel.Hcoin > 0)
            {
                var hcoinData = new ItemData { ItemId = 1, Count = rewardExcel.Hcoin };
                proto.Reward.ItemList_.Add(hcoinData.ToProto());
                data.Add(hcoinData);
            }

            foreach (var item in rewardExcel.GetItems())
            {
                var itemData = new ItemData
                {
                    ItemId = item.Item1,
                    Count = item.Item2
                };

                proto.Reward.ItemList_.Add(itemData.ToProto());
                data.Add(itemData);
            }

            
            reward.SetTakenReward(challengeReward.StarCount);
            rewardInfos.Add(proto);
        }

        
        await Player.InventoryManager!.AddItems(data);
        return rewardInfos;
    }

    public void SaveInstance(BaseChallengeInstance instance)
    {
        ChallengeData.ChallengeInstance = Convert.ToBase64String(instance.Data.ToByteArray());
    }

    public void ClearInstance()
    {
        ChallengeData.ChallengeInstance = null;
        ChallengeInstance = null;
    }

    public void ResurrectInstance()
    {
        if (ChallengeData.ChallengeInstance == null) return;
        var protoByte = Convert.FromBase64String(ChallengeData.ChallengeInstance);
        var proto = ChallengeDataPb.Parser.ParseFrom(protoByte);

        if (proto != null)
            ChallengeInstance = proto.ChallengeTypeCase switch
            {
                ChallengeDataPb.ChallengeTypeOneofCase.Memory => new ChallengeMemoryInstance(Player, proto),
                ChallengeDataPb.ChallengeTypeOneofCase.Peak => null,
                ChallengeDataPb.ChallengeTypeOneofCase.Story => new ChallengeStoryInstance(Player, proto),
                ChallengeDataPb.ChallengeTypeOneofCase.Boss => new ChallengeBossInstance(Player, proto),
                _ => null
            };
        else
            ChallengeData.ChallengeInstance = null;
    }

    public void SaveBattleRecord(BaseLegacyChallengeInstance inst)
    {
        switch (inst)
        {
            case ChallengeMemoryInstance memory:
            {
                Player.FriendRecordData!.ChallengeGroupStatistics.TryAdd((uint)memory.Config.GroupID,
                    new ChallengeGroupStatisticsPb
                    {
                        GroupId = (uint)memory.Config.GroupID
                    });
                var stats = Player.FriendRecordData.ChallengeGroupStatistics[(uint)memory.Config.GroupID];

                stats.MemoryGroupStatistics ??= [];

                var starCount = 0u;
                for (var i = 0; i < 3; i++) starCount += (memory.Data.Memory.Stars & (1 << i)) != 0 ? 1u : 0u;

                if (stats.MemoryGroupStatistics.GetValueOrDefault((uint)memory.Config.ID)?.Stars >
                    starCount) return; 


                var pb = new MemoryGroupStatisticsPb
                {
                    RoundCount = (uint)(memory.Config.ChallengeCountDown - memory.Data.Memory.RoundsLeft),
                    Stars = starCount,
                    RecordId = Player.FriendRecordData!.NextRecordId++,
                    Level = memory.Config.Floor
                };

                List<ExtraLineupType> lineupTypes =
                [
                    ExtraLineupType.LineupChallenge
                ];

                if (memory.Config.StageNum >= 2)
                    lineupTypes.Add(ExtraLineupType.LineupChallenge2);

                foreach (var type in lineupTypes)
                {
                    var lineup = Player.LineupManager!.GetExtraLineup(type);
                    if (lineup == null) continue;

                    var index = 0u;
                    var lineupPb = new List<ChallengeAvatarInfoPb>();

                    foreach (var avatar in lineup.BaseAvatars ?? [])
                    {
                        var formalAvatar = Player.AvatarManager!.GetFormalAvatar(avatar.BaseAvatarId);
                        if (formalAvatar == null) continue;

                        lineupPb.Add(new ChallengeAvatarInfoPb
                        {
                            Index = index++,
                            Id = (uint)formalAvatar.BaseAvatarId,
                            AvatarType = AvatarType.AvatarFormalType,
                            Level = (uint)formalAvatar.Level
                        });
                    }

                    pb.Lineups.Add(lineupPb);
                }

                stats.MemoryGroupStatistics[(uint)memory.Config.ID] = pb;
                break;
            }
            case ChallengeStoryInstance story:
            {
                Player.FriendRecordData!.ChallengeGroupStatistics.TryAdd((uint)story.Config.GroupID,
                    new ChallengeGroupStatisticsPb
                    {
                        GroupId = (uint)story.Config.GroupID
                    });
                var stats = Player.FriendRecordData.ChallengeGroupStatistics[(uint)story.Config.GroupID];

                stats.StoryGroupStatistics ??= [];

                var starCount = 0u;
                for (var i = 0; i < 3; i++) starCount += (story.Data.Story.Stars & (1 << i)) != 0 ? 1u : 0u;

                if (stats.StoryGroupStatistics.GetValueOrDefault((uint)story.Config.ID)?.Stars >
                    starCount) return; 

                var pb = new StoryGroupStatisticsPb
                {
                    Stars = starCount,
                    RecordId = Player.FriendRecordData!.NextRecordId++,
                    Level = story.Config.Floor,
                    BuffOne = story.Data.Story.Buffs.Count > 0 ? story.Data.Story.Buffs[0] : 0,
                    BuffTwo = story.Data.Story.Buffs.Count > 1 ? story.Data.Story.Buffs[1] : 0,
                    Score = (uint)story.GetTotalScore()
                };

                List<ExtraLineupType> lineupTypes =
                [
                    ExtraLineupType.LineupChallenge
                ];

                if (story.Config.StageNum >= 2)
                    lineupTypes.Add(ExtraLineupType.LineupChallenge2);

                foreach (var type in lineupTypes)
                {
                    var lineup = Player.LineupManager!.GetExtraLineup(type);
                    if (lineup == null) continue;

                    var index = 0u;
                    var lineupPb = new List<ChallengeAvatarInfoPb>();

                    foreach (var avatar in lineup.BaseAvatars ?? [])
                    {
                        var formalAvatar = Player.AvatarManager!.GetFormalAvatar(avatar.BaseAvatarId);
                        if (formalAvatar == null) continue;

                        lineupPb.Add(new ChallengeAvatarInfoPb
                        {
                            Index = index++,
                            Id = (uint)formalAvatar.BaseAvatarId,
                            AvatarType = AvatarType.AvatarFormalType,
                            Level = (uint)formalAvatar.Level
                        });
                    }

                    pb.Lineups.Add(lineupPb);
                }

                stats.StoryGroupStatistics[(uint)story.Config.ID] = pb;
                break;
            }
            case ChallengeBossInstance boss:
            {
                Player.FriendRecordData!.ChallengeGroupStatistics.TryAdd((uint)boss.Config.GroupID,
                    new ChallengeGroupStatisticsPb
                    {
                        GroupId = (uint)boss.Config.GroupID
                    });
                var stats = Player.FriendRecordData.ChallengeGroupStatistics[(uint)boss.Config.GroupID];

                stats.BossGroupStatistics ??= [];

                var starCount = 0u;
                for (var i = 0; i < 3; i++) starCount += (boss.Data.Boss.Stars & (1 << i)) != 0 ? 1u : 0u;

                if (stats.BossGroupStatistics.GetValueOrDefault((uint)boss.Config.ID)?.Stars >
                    starCount) return; 

                var pb = new BossGroupStatisticsPb
                {
                    Stars = starCount,
                    RecordId = Player.FriendRecordData!.NextRecordId++,
                    Level = boss.Config.Floor,
                    BuffOne = boss.Data.Boss.Buffs.Count > 0 ? boss.Data.Boss.Buffs[0] : 0,
                    BuffTwo = boss.Data.Boss.Buffs.Count > 1 ? boss.Data.Boss.Buffs[1] : 0,
                    Score = (uint)boss.GetTotalScore()
                };

                List<ExtraLineupType> lineupTypes =
                [
                    ExtraLineupType.LineupChallenge
                ];

                if (boss.Config.StageNum >= 2)
                    lineupTypes.Add(ExtraLineupType.LineupChallenge2);

                foreach (var type in lineupTypes)
                {
                    var lineup = Player.LineupManager!.GetExtraLineup(type);
                    if (lineup == null) continue;

                    var index = 0u;
                    var lineupPb = new List<ChallengeAvatarInfoPb>();

                    foreach (var avatar in lineup.BaseAvatars ?? [])
                    {
                        var formalAvatar = Player.AvatarManager!.GetFormalAvatar(avatar.BaseAvatarId);
                        if (formalAvatar == null) continue;

                        lineupPb.Add(new ChallengeAvatarInfoPb
                        {
                            Index = index++,
                            Id = (uint)formalAvatar.BaseAvatarId,
                            AvatarType = AvatarType.AvatarFormalType,
                            Level = (uint)formalAvatar.Level
                        });
                    }

                    pb.Lineups.Add(lineupPb);
                }

                stats.BossGroupStatistics[(uint)boss.Config.ID] = pb;
                break;
            }
        }
    }

    private bool PrepareChallengeLineup(ExtraLineupType lineupType)
    {
        var lineup = Player.LineupManager!.GetExtraLineup(lineupType);
        if (lineup == null) return false;

        var avatars = Player.LineupManager.GetAvatarsFromTeam((int)lineupType + 10);
        if (avatars.Count == 0) return false;

        foreach (var avatar in avatars)
        {
            avatar.AvatarInfo.SetCurHp(10000, true);
            avatar.AvatarInfo.SetCurSp(5000, true);
        }

        lineup.Mp = Player.LineupManager.GetMaxMp();
        return true;
    }

    private async ValueTask<bool> TryEnterChallengeScene(int mapEntranceId)
    {
        if (mapEntranceId <= 0 || !GameData.MapEntranceData.ContainsKey(mapEntranceId))
            return false;

        try
        {
            var changed = await Player.EnterScene(mapEntranceId, 0, false);
            return changed || Player.Data.EntryId == mapEntranceId;
        }
        catch
        {
            return false;
        }
    }

    #endregion
}


