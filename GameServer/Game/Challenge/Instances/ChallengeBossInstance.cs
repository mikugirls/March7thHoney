using March7thHoney.Data;
using March7thHoney.Data.Excel;
using March7thHoney.Database.Friend;
using March7thHoney.Enums.Item;
using March7thHoney.Enums.Mission;
using March7thHoney.GameServer.Game.Battle;
using March7thHoney.GameServer.Game.Challenge.Definitions;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.GameServer.Game.Scene.Entity;
using March7thHoney.GameServer.Server.Packet.Send.Challenge;
using March7thHoney.GameServer.Server.Packet.Send.Lineup;
using March7thHoney.Proto;
using March7thHoney.Proto.ServerSide;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Game.Challenge.Instances;

public class ChallengeBossInstance(PlayerInstance player, ChallengeDataPb data)
    : BaseLegacyChallengeInstance(player, data)
{
    #region Properties

    public override ChallengeConfigExcel Config { get; } = GameData.ChallengeConfigData[(int)data.Boss.ChallengeMazeId];

    #endregion

    #region Setter & Getter

    public override uint GetStars()
    {
        return Data.Boss.Stars;
    }

    public override uint GetScore1()
    {
        return Data.Boss.ScoreStage1;
    }

    public override uint GetScore2()
    {
        return Data.Boss.ScoreStage2;
    }

    public void SetCurrentExtraLineup(ExtraLineupType type)
    {
        Data.Boss.CurrentExtraLineup = (ChallengeLineupTypePb)type;
    }

    public int GetTotalScore()
    {
        return (int)(Data.Boss.ScoreStage1 + Data.Boss.ScoreStage2);
    }

    public override int GetCurrentExtraLineupType()
    {
        return (int)Data.Boss.CurrentExtraLineup;
    }

    public override void SetStartPos(Position pos)
    {
        Data.Boss.StartPos = pos.ToVector3Pb();
    }

    public override void SetStartRot(Position rot)
    {
        Data.Boss.StartRot = rot.ToVector3Pb();
    }

    public override void SetSavedMp(int mp)
    {
        Data.Boss.SavedMp = (uint)mp;
    }

    public override Dictionary<int, List<ChallengeConfigExcel.ChallengeMonsterInfo>> GetStageMonsters()
    {
        return Data.Boss.CurrentStage == 1
            ? Config.ChallengeMonsters1
            : Config.ChallengeMonsters2;
    }

    #endregion

    #region Serialization

    public override CurChallenge ToProto()
    {
        return new CurChallenge
        {
            ChallengeId = Data.Boss.ChallengeMazeId,
            ExtraLineupType = (ExtraLineupType)Data.Boss.CurrentExtraLineup,
            Status = (ChallengeStatus)Data.Boss.CurStatus,
            StageInfo = new ChallengeCurBuffInfo
            {
                CurBossBuffs = new ChallengeBossBuffList
                {
                    BuffList = { Data.Boss.Buffs },
                    ChallengeBossConst = 1
                }
            },
            RoundCount = (uint)Config.ChallengeCountDown,
            ScoreId = Data.Boss.ScoreStage1,
            ScoreTwo = Data.Boss.ScoreStage2
        };
    }

    public override ChallengeStageInfo ToStageInfo()
    {
        var proto = new ChallengeStageInfo
        {
            BossInfo = new ChallengeBossInfo
            {
                FirstNode = new ChallengeBossSingleNodeInfo
                {
                    BuffId = Data.Boss.Buffs[0]
                },
                SecondNode = new ChallengeBossSingleNodeInfo
                {
                    BuffId = Data.Boss.Buffs[1]
                },
                Unk1 = true
            }
        };

        foreach (var lineupAvatar in Player.LineupManager?.GetExtraLineup(ExtraLineupType.LineupChallenge)
                     ?.BaseAvatars ?? [])
        {
            var avatar = Player.AvatarManager?.GetFormalAvatar(lineupAvatar.BaseAvatarId);
            if (avatar == null) continue;
            proto.BossInfo.FirstLineup.Add((uint)avatar.AvatarId);
            var equip = Player.InventoryManager?.GetItem(0, avatar.GetCurPathInfo().EquipId,
                ItemMainTypeEnum.Equipment);
            if (equip != null)
                proto.BossInfo.ChallengeAvatarEquipmentMap.Add((uint)avatar.AvatarId,
                    equip.ToChallengeEquipmentProto());

            var relicProto = new ChallengeBossAvatarRelicInfo();

            foreach (var relicUniqueId in avatar.GetCurPathInfo().Relic)
            {
                var relic = Player.InventoryManager?.GetItem(0, relicUniqueId.Value, ItemMainTypeEnum.Relic);
                if (relic == null) continue;
                relicProto.AvatarRelicSlotMap.Add((uint)relicUniqueId.Key, relic.ToChallengeRelicProto());
            }

            proto.BossInfo.ChallengeAvatarRelicMap.Add((uint)avatar.AvatarId, relicProto);
        }

        foreach (var lineupAvatar in Player.LineupManager?.GetExtraLineup(ExtraLineupType.LineupChallenge2)
                     ?.BaseAvatars ?? [])
        {
            var avatar = Player.AvatarManager?.GetFormalAvatar(lineupAvatar.BaseAvatarId);
            if (avatar == null) continue;
            proto.BossInfo.SecondLineup.Add((uint)avatar.AvatarId);
            var equip = Player.InventoryManager?.GetItem(0, avatar.GetCurPathInfo().EquipId,
                ItemMainTypeEnum.Equipment);
            if (equip != null)
                proto.BossInfo.ChallengeAvatarEquipmentMap.Add((uint)avatar.AvatarId,
                    equip.ToChallengeEquipmentProto());

            var relicProto = new ChallengeBossAvatarRelicInfo();

            foreach (var relicUniqueId in avatar.GetCurPathInfo().Relic)
            {
                var relic = Player.InventoryManager?.GetItem(0, relicUniqueId.Value, ItemMainTypeEnum.Relic);
                if (relic == null) continue;
                relicProto.AvatarRelicSlotMap.Add((uint)relicUniqueId.Key, relic.ToChallengeRelicProto());
            }

            proto.BossInfo.ChallengeAvatarRelicMap.Add((uint)avatar.AvatarId, relicProto);
        }

        return proto;
    }

    #endregion

    #region Handlers

    public override void OnBattleStart(BattleInstance battle)
    {
        base.OnBattleStart(battle);

        battle.RoundLimit = Config.ChallengeCountDown;

        battle.Buffs.Add(new MazeBuff(Config.MazeBuffID, 1, -1)
        {
            WaveFlag = -1
        });

        battle.AddBattleTarget(1, 90004, 0);
        battle.AddBattleTarget(1, 90005, 0);

        if (Data.Boss.Buffs.Count < Data.Boss.CurrentStage) return;
        var buffId = Data.Boss.Buffs[(int)(Data.Boss.CurrentStage - 1)];
        battle.Buffs.Add(new MazeBuff((int)buffId, 1, -1)
        {
            WaveFlag = -1
        });
    }

    public override async ValueTask OnBattleEnd(BattleInstance battle, PVEBattleResultCsReq req)
    {
        
        var stageScore = 0;
        foreach (var battleTarget in req.Stt.BattleTargetInfo[1].BattleTargetList_)
            stageScore += (int)battleTarget.Progress;

        
        if (Data.Boss.CurrentStage == 1)
            Data.Boss.ScoreStage1 = (uint)stageScore;
        else
            Data.Boss.ScoreStage2 = (uint)stageScore;

        switch (req.EndStatus)
        {
            case BattleEndStatus.BattleEndWin:
                
                long monsters = Player.SceneInstance!.Entities.Values.OfType<EntityMonster>().Count();

                if (monsters == 0) await AdvanceStage(req);

                
                Data.Boss.SavedMp = (uint)Player.LineupManager!.GetCurLineup()!.Mp;
                break;
            case BattleEndStatus.BattleEndQuit:
                
                var lineup = Player.LineupManager!.GetCurLineup()!;
                lineup.Mp = (int)Data.Boss.SavedMp;
                await Player.MoveTo(Data.Boss.StartPos.ToPosition(), Data.Boss.StartRot.ToPosition());
                await Player.SendPacket(new PacketSyncLineupNotify(lineup));
                break;
            default:
                
                if (req.Stt.EndReason == BattleEndReason.TurnLimit)
                {
                    await AdvanceStage(req);
                }
                else
                {
                    
                    Data.Boss.CurStatus = (int)ChallengeStatus.ChallengeFailed;

                    
                    await Player.SendPacket(new PacketChallengeBossPhaseSettleNotify(this));
                }

                break;
        }
    }

    public uint CalculateStars()
    {
        var targets = Config.ChallengeTargetID!;
        var stars = 0u;

        for (var i = 0; i < targets.Count; i++)
        {
            if (!GameData.ChallengeTargetData.ContainsKey(targets[i])) continue;

            var target = GameData.ChallengeTargetData[targets[i]];

            switch (target.ChallengeTargetType)
            {
                case ChallengeTargetExcel.ChallengeType.TOTAL_SCORE:
                    if (GetTotalScore() >= target.ChallengeTargetParam1) stars += 1u << i;
                    break;
            }
        }

        return Math.Min(stars, 7);
    }

    private async ValueTask AdvanceStage(PVEBattleResultCsReq req)
    {
        if (Data.Boss.CurrentStage >= Config.StageNum)
        {
            
            Data.Boss.CurStatus = (int)ChallengeStatus.ChallengeFinish;
            Data.Boss.Stars = CalculateStars();

            
            Player.ChallengeManager!.AddHistory((int)Data.Boss.ChallengeMazeId, (int)GetStars(), GetTotalScore());

            
            await Player.SendPacket(new PacketChallengeBossPhaseSettleNotify(this, req.Stt.BattleTargetInfo[1]));
            await Player.SendPacket(new PacketChallengeSettleNotify(this));

            
            await Player.MissionManager!.HandleFinishType(MissionFinishTypeEnum.ChallengeFinish, this);

            
            Player.ChallengeManager.SaveBattleRecord(this);

            
            Player.FriendRecordData!.AddAndRemoveOld(new FriendDevelopmentInfoPb
            {
                DevelopmentType = (DevelopmentType)9,
                Params = { { "ChallengeId", (uint)Config.ID } }
            });
        }
        else
        {
            await Player.SendPacket(new PacketChallengeBossPhaseSettleNotify(this, req.Stt.BattleTargetInfo[1]));
        }
    }

    public async ValueTask NextPhase()
    {
        
        Data.Boss.CurrentStage++;

        
        await Player.SceneInstance!.EntityLoader!.UnloadGroup(Config.MazeGroupID1);
        
        await Player.SceneInstance!.EntityLoader!.LoadGroup(Config.MazeGroupID2);

        
        SetCurrentExtraLineup(ExtraLineupType.LineupChallenge2);
        await Player.LineupManager!.SetExtraLineup((ExtraLineupType)GetCurrentExtraLineupType());
        await Player.SendPacket(new PacketChallengeLineupNotify((ExtraLineupType)GetCurrentExtraLineupType()));
        await Player.SceneInstance!.SyncLineup();

        Data.Boss.SavedMp = (uint)Player.LineupManager.GetCurLineup()!.Mp;

        
        if (Config.MapEntranceID2 != 0)
        {
            await Player.EnterScene(Config.MapEntranceID2, 0, false);
            Data.Boss.StartPos = Player.Data.Pos!.ToVector3Pb();
            Data.Boss.StartRot = Player.Data.Rot!.ToVector3Pb();
            await Player.SceneInstance!.EntityLoader!.LoadGroup(Config.MazeGroupID2);
        }
        else
        {
            await Player.MoveTo(Data.Boss.StartPos.ToPosition(), Data.Boss.StartRot.ToPosition());
        }

        Player.ChallengeManager!.SaveInstance(this);
    }

    #endregion
}
