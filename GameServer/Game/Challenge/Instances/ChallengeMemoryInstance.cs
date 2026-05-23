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

public class ChallengeMemoryInstance(PlayerInstance player, ChallengeDataPb data)
    : BaseLegacyChallengeInstance(player, data)
{
    #region Properties

    public override ChallengeConfigExcel Config { get; } =
        GameData.ChallengeConfigData[(int)data.Memory.ChallengeMazeId];

    #endregion

    #region Serialization

    public override CurChallenge ToProto()
    {
        return new CurChallenge
        {
            ChallengeId = Data.Memory.ChallengeMazeId,
            DeadAvatarNum = Data.Memory.DeadAvatarNum,
            ExtraLineupType = (ExtraLineupType)Data.Memory.CurrentExtraLineup,
            Status = (ChallengeStatus)Data.Memory.CurStatus,
            StageInfo = new ChallengeCurBuffInfo(),
            RoundCount = (uint)(Config.ChallengeCountDown - Data.Memory.RoundsLeft)
        };
    }

    #endregion

    #region Getter & Setter

    public void SetCurrentExtraLineup(ExtraLineupType type)
    {
        Data.Memory.CurrentExtraLineup = (ChallengeLineupTypePb)type;
    }

    public override Dictionary<int, List<ChallengeConfigExcel.ChallengeMonsterInfo>> GetStageMonsters()
    {
        return Data.Memory.CurrentStage == 1 ? Config.ChallengeMonsters1 : Config.ChallengeMonsters2;
    }

    public override uint GetStars()
    {
        return Data.Memory.Stars;
    }

    public override int GetCurrentExtraLineupType()
    {
        return (int)Data.Memory.CurrentExtraLineup;
    }

    public override void SetStartPos(Position pos)
    {
        Data.Memory.StartPos = pos.ToVector3Pb();
    }

    public override void SetStartRot(Position rot)
    {
        Data.Memory.StartRot = rot.ToVector3Pb();
    }

    public override void SetSavedMp(int mp)
    {
        Data.Memory.SavedMp = (uint)mp;
    }

    #endregion

    #region Handlers

    public override void OnBattleStart(BattleInstance battle)
    {
        base.OnBattleStart(battle);

        battle.RoundLimit = (int)Data.Memory.RoundsLeft;

        battle.Buffs.Add(new MazeBuff(Config.MazeBuffID, 1, -1)
        {
            WaveFlag = -1
        });
    }

    public override async ValueTask OnBattleEnd(BattleInstance battle, PVEBattleResultCsReq req)
    {
        switch (req.EndStatus)
        {
            case BattleEndStatus.BattleEndWin:
                
                foreach (var avatar in battle.Lineup.AvatarData!.FormalAvatars)
                    if (avatar.CurrentHp <= 0)
                        Data.Memory.DeadAvatarNum++;

                
                long monsters = Player.SceneInstance!.Entities.Values.OfType<EntityMonster>().Count();

                if (monsters == 0) await AdvanceStage();

                
                Data.Memory.RoundsLeft = Math.Min(Math.Max(Data.Memory.RoundsLeft - req.Stt.RoundCnt, 1),
                    Data.Memory.RoundsLeft);

                
                Data.Memory.SavedMp = (uint)Player.LineupManager!.GetCurLineup()!.Mp;
                break;
            case BattleEndStatus.BattleEndQuit:
                
                var lineup = Player.LineupManager!.GetCurLineup()!;
                lineup.Mp = (int)Data.Memory.SavedMp;
                await Player.MoveTo(Data.Memory.StartPos.ToPosition(), Data.Memory.StartRot.ToPosition());
                await Player.SendPacket(new PacketSyncLineupNotify(lineup));
                break;
            default:
                
                
                Data.Memory.CurStatus = (int)ChallengeStatus.ChallengeFailed;

                
                await Player.SendPacket(new PacketChallengeSettleNotify(this));

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
                case ChallengeTargetExcel.ChallengeType.ROUNDS_LEFT:
                    if (Data.Memory.RoundsLeft >= target.ChallengeTargetParam1) stars += 1u << i;
                    break;
                case ChallengeTargetExcel.ChallengeType.DEAD_AVATAR:
                    if (Data.Memory.DeadAvatarNum == 0) stars += 1u << i;
                    break;
            }
        }

        return Math.Min(stars, 7);
    }

    private async ValueTask AdvanceStage()
    {
        if (Data.Memory.CurrentStage >= Config.StageNum)
        {
            
            Data.Memory.CurStatus = (int)ChallengeStatus.ChallengeFinish;
            Data.Memory.Stars = CalculateStars();

            
            Player.ChallengeManager!.AddHistory((int)Data.Memory.ChallengeMazeId, (int)Data.Memory.Stars, 0);

            
            await Player.SendPacket(new PacketChallengeSettleNotify(this));

            
            await Player.MissionManager!.HandleFinishType(MissionFinishTypeEnum.ChallengeFinish, this);

            
            Player.ChallengeManager.SaveBattleRecord(this);

            
            Player.FriendRecordData!.AddAndRemoveOld(new FriendDevelopmentInfoPb
            {
                DevelopmentType = (DevelopmentType)7,
                Params = { { "ChallengeId", (uint)Config.ID } }
            });
        }
        else
        {
            Data.Memory.CurrentStage++;

            SetCurrentExtraLineup(ExtraLineupType.LineupChallenge2);
            await Player.LineupManager!.SetExtraLineup((ExtraLineupType)GetCurrentExtraLineupType());

            var entranceId = Config.MapEntranceID2 != 0 ? Config.MapEntranceID2 : Config.MapEntranceID;
            await Player.EnterScene(entranceId, 0, true);

            await Player.SendPacket(new PacketChallengeLineupNotify((ExtraLineupType)Data.Memory.CurrentExtraLineup));

            Data.Memory.SavedMp = (uint)Player.LineupManager.GetCurLineup()!.Mp;
            Data.Memory.StartPos = Player.Data.Pos!.ToVector3Pb();
            Data.Memory.StartRot = Player.Data.Rot!.ToVector3Pb();

            Player.ChallengeManager!.SaveInstance(this);
        }
    }

    #endregion
}
