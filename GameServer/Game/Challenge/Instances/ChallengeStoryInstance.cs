using March7thHoney.Data;
using March7thHoney.Data.Excel;
using March7thHoney.Database.Friend;
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

public class ChallengeStoryInstance(PlayerInstance player, ChallengeDataPb data)
    : BaseLegacyChallengeInstance(player, data)
{
    #region Properties

    public override ChallengeConfigExcel Config { get; } =
        GameData.ChallengeConfigData[(int)data.Story.ChallengeMazeId];

    #endregion

    #region Serialization

    public override CurChallenge ToProto()
    {
        return new CurChallenge
        {
            ChallengeId = Data.Story.ChallengeMazeId,
            ExtraLineupType = (ExtraLineupType)Data.Story.CurrentExtraLineup,
            Status = (ChallengeStatus)Data.Story.CurStatus,
            StageInfo = new ChallengeCurBuffInfo
            {
                CurStoryBuffs = new ChallengeStoryBuffList
                {
                    BuffList = { Data.Story.Buffs }
                }
            },
            RoundCount = (uint)Config.ChallengeCountDown,
            ScoreId = Data.Story.ScoreStage1,
            ScoreTwo = Data.Story.ScoreStage2
        };
    }

    #endregion

    #region Setter & Getter

    public override uint GetStars()
    {
        return Data.Story.Stars;
    }

    public override uint GetScore1()
    {
        return Data.Story.ScoreStage1;
    }

    public override uint GetScore2()
    {
        return Data.Story.ScoreStage2;
    }

    public void SetCurrentExtraLineup(ExtraLineupType type)
    {
        Data.Story.CurrentExtraLineup = (ChallengeLineupTypePb)type;
    }

    public int GetTotalScore()
    {
        return (int)(Data.Story.ScoreStage1 + Data.Story.ScoreStage2);
    }

    public override int GetCurrentExtraLineupType()
    {
        return (int)Data.Story.CurrentExtraLineup;
    }

    public override void SetStartPos(Position pos)
    {
        Data.Story.StartPos = pos.ToVector3Pb();
    }

    public override void SetStartRot(Position rot)
    {
        Data.Story.StartRot = rot.ToVector3Pb();
    }

    public override void SetSavedMp(int mp)
    {
        Data.Story.SavedMp = (uint)mp;
    }

    public override Dictionary<int, List<ChallengeConfigExcel.ChallengeMonsterInfo>> GetStageMonsters()
    {
        return Data.Story.CurrentStage == 1
            ? Config.ChallengeMonsters1
            : Config.ChallengeMonsters2;
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

        if (Config.StoryExcel == null) return;
        
        
        
        battle.AddBattleTarget(1, 10003, GetTotalScore());

        foreach (var id in Config.StoryExcel.BattleTargetID!) battle.AddBattleTarget(5, id, GetTotalScore());

        if (Data.Story.Buffs.Count < Data.Story.CurrentStage) return;
        var buffId = Data.Story.Buffs[(int)(Data.Story.CurrentStage - 1)];
        battle.Buffs.Add(new MazeBuff((int)buffId, 1, -1)
        {
            WaveFlag = -1
        });
    }

    public override async ValueTask OnBattleEnd(BattleInstance battle, PVEBattleResultCsReq req)
    {
        
        var stageScore = (int)req.Stt.ChallengeScore - GetTotalScore();

        
        if (Data.Story.CurrentStage == 1)
            Data.Story.ScoreStage1 = (uint)stageScore;
        else
            Data.Story.ScoreStage2 = (uint)stageScore;

        switch (req.EndStatus)
        {
            case BattleEndStatus.BattleEndWin:
                
                long monsters = Player.SceneInstance!.Entities.Values.OfType<EntityMonster>().Count();

                if (monsters == 0) await AdvanceStage();

                
                Data.Story.SavedMp = (uint)Player.LineupManager!.GetCurLineup()!.Mp;
                break;
            case BattleEndStatus.BattleEndQuit:
                
                var lineup = Player.LineupManager!.GetCurLineup()!;
                lineup.Mp = (int)Data.Story.SavedMp;
                await Player.MoveTo(Data.Story.StartPos.ToPosition(), Data.Story.StartRot.ToPosition());
                await Player.SendPacket(new PacketSyncLineupNotify(lineup));
                break;
            default:
                
                if (req.Stt.EndReason == BattleEndReason.TurnLimit)
                {
                    await AdvanceStage();
                }
                else
                {
                    
                    Data.Story.CurStatus = (int)ChallengeStatus.ChallengeFailed;

                    
                    await Player.SendPacket(new PacketChallengeSettleNotify(this));
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

    private async ValueTask AdvanceStage()
    {
        if (Data.Story.CurrentStage >= Config.StageNum)
        {
            
            Data.Story.CurStatus = (int)ChallengeStatus.ChallengeFinish;
            Data.Story.Stars = CalculateStars();

            
            Player.ChallengeManager!.AddHistory((int)Data.Story.ChallengeMazeId, (int)GetStars(), GetTotalScore());

            
            await Player.SendPacket(new PacketChallengeSettleNotify(this));

            
            await Player.MissionManager!.HandleFinishType(MissionFinishTypeEnum.ChallengeFinish, this);

            
            Player.ChallengeManager.SaveBattleRecord(this);

            
            Player.FriendRecordData!.AddAndRemoveOld(new FriendDevelopmentInfoPb
            {
                DevelopmentType = (DevelopmentType)8,
                Params = { { "ChallengeId", (uint)Config.ID } }
            });
        }
        else
        {
            Data.Story.CurrentStage++;

            
            SetCurrentExtraLineup(ExtraLineupType.LineupChallenge2);
            await Player.LineupManager!.SetExtraLineup((ExtraLineupType)GetCurrentExtraLineupType());

            
            
            var entranceId = Config.MapEntranceID2 != 0 ? Config.MapEntranceID2 : Config.MapEntranceID;
            await Player.EnterScene(entranceId, 0, true);

            await Player.SendPacket(new PacketChallengeLineupNotify((ExtraLineupType)Data.Story.CurrentExtraLineup));

            Data.Story.SavedMp = (uint)Player.LineupManager.GetCurLineup()!.Mp;
            Data.Story.StartPos = Player.Data.Pos!.ToVector3Pb();
            Data.Story.StartRot = Player.Data.Rot!.ToVector3Pb();

            Player.ChallengeManager!.SaveInstance(this);
        }
    }

    #endregion
}
