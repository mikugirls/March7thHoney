using March7thHoney.Data;
using March7thHoney.Data.Excel;
using March7thHoney.Database.Friend;
using March7thHoney.Enums.Mission;
using March7thHoney.GameServer.Game.Battle;
using March7thHoney.GameServer.Game.Challenge.Definitions;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.GameServer.Game.Scene.Entity;
using March7thHoney.GameServer.Server.Packet.Send.ChallengePeak;
using March7thHoney.GameServer.Server.Packet.Send.Lineup;
using March7thHoney.Proto;
using March7thHoney.Proto.ServerSide;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Game.Challenge.Instances;

public class ChallengePeakInstance(PlayerInstance player, ChallengeDataPb data) : BaseChallengeInstance(player, data)
{
    #region Setter & Getter

    public override Dictionary<int, List<ChallengeConfigExcel.ChallengeMonsterInfo>> GetStageMonsters()
    {
        if (!Data.Peak.IsHard || Config.BossExcel == null) return Config.ChallengeMonsters;

        Dictionary<int, List<ChallengeConfigExcel.ChallengeMonsterInfo>> monsters = [];

        var groupId = Config.GetMazeGroupId();
        monsters.Add(groupId, []);


        var curConfId = 200000;
        foreach (var eventId in Config.BossExcel.HardEventIDList)
        {
            
            if (!GameData.StageConfigData.TryGetValue(eventId, out var stage)) continue;

            var monsterId = stage.MonsterList.LastOrDefault()?.Monster0 ?? 0;
            if (!GameData.MonsterConfigData.TryGetValue(monsterId, out var monsterConf)) continue;
            if (!GameData.MonsterTemplateConfigData.TryGetValue(monsterConf.MonsterTemplateID, out var template)) continue;

            var npcMonsterId = template.NPCMonsterList.Take(2).LastOrDefault(0);
            if (!GameData.NpcMonsterDataData.ContainsKey(npcMonsterId)) continue;

            monsters[groupId].Add(new ChallengeConfigExcel.ChallengeMonsterInfo(++curConfId, npcMonsterId,
                    eventId));
        }

        return monsters;
    }

    #endregion

    #region Properties

    public ChallengePeakConfigExcel Config { get; } =
        GameData.ChallengePeakConfigData[(int)data.Peak.CurrentPeakLevelId];

    public List<int> AllBattleTargets { get; } = [];
    public bool IsWin { get; private set; }

    #endregion

    

    

    #region Handlers

    public override void OnBattleStart(BattleInstance battle)
    {
        base.OnBattleStart(battle);

        foreach (var peakBuff in Data.Peak.Buffs)
            battle.Buffs.Add(new MazeBuff((int)peakBuff, 1, -1)
            {
                WaveFlag = -1
            });

        if (Data.Peak.IsHard && Config.BossExcel != null)
        {
            var excel = GameData.BattleTargetConfigData.GetValueOrDefault(Config.BossExcel.HardTarget);
            if (excel != null)
            {
                battle.AddBattleTarget(5, excel.ID, 0, excel.TargetParam);
                AllBattleTargets.Add(excel.ID);
            }
        }
        else
        {
            foreach (var targetId in Config.NormalTargetList)
            {
                var excel = GameData.BattleTargetConfigData.GetValueOrDefault(targetId);
                if (excel != null)
                {
                    battle.AddBattleTarget(5, excel.ID, 0, excel.TargetParam);
                    AllBattleTargets.Add(excel.ID);
                }
            }
        }
    }

    public override async ValueTask OnBattleEnd(BattleInstance battle, PVEBattleResultCsReq req)
    {
        switch (req.EndStatus)
        {
            case BattleEndStatus.BattleEndWin:
                
                long monsters = Player.SceneInstance!.Entities.Values.OfType<EntityMonster>().Count();

                if (monsters == 0)
                {
                    Data.Peak.CurStatus = (int)ChallengeStatus.ChallengeFinish;
                    var res = CalculateStars(req);
                    Data.Peak.Stars = res.Item1;
                    Data.Peak.RoundCnt = req.Stt.RoundCnt;
                    IsWin = true;

                    await Player.SendPacket(new PacketChallengePeakSettleScNotify(this, res.Item2));

                    
                    await Player.MissionManager!.HandleFinishType(MissionFinishTypeEnum.ChallengePeakBattleFinish,
                        this);

                    await Player.ChallengePeakManager!.SaveHistory(this, res.Item2);

                    
                    Player.FriendRecordData!.AddAndRemoveOld(new FriendDevelopmentInfoPb
                    {
                        DevelopmentType = DevelopmentType.KghodpfjgliMjfldkhkdab,
                        Params = { { "PeakLevelId", (uint)Config.ID } }
                    });
                }

                
                Data.Peak.SavedMp = (uint)Player.LineupManager!.GetCurLineup()!.Mp;
                break;
            case BattleEndStatus.BattleEndQuit:
                
                var lineup = Player.LineupManager!.GetCurLineup()!;
                lineup.Mp = (int)Data.Peak.SavedMp;
                if (Data.Peak.StartPos != null && Data.Peak.StartRot != null)
                    await Player.MoveTo(Data.Peak.StartPos.ToPosition(), Data.Peak.StartRot.ToPosition());
                await Player.SendPacket(new PacketSyncLineupNotify(lineup));
                break;
            default:
                
                
                Data.Peak.CurStatus = (int)ChallengeStatus.ChallengeFailed;

                
                await Player.SendPacket(new PacketChallengePeakSettleScNotify(this, []));

                break;
        }
    }

    public (uint, List<uint>) CalculateStars(PVEBattleResultCsReq req)
    {
        var targets = AllBattleTargets;
        var stars = 0u;

        List<uint> finishedIds = [];
        foreach (var targetId in targets)
        {
            var target = req.Stt.BattleTargetInfo[5].BattleTargetList_.FirstOrDefault(x => x.Id == targetId);
            if (target == null) continue;
            var excel = GameData.BattleTargetConfigData.GetValueOrDefault(targetId);
            if (excel == null) continue;

            if (target.Progress <= excel.TargetParam)
            {
                stars += 1u;
                finishedIds.Add((uint)targetId);
            }
        }

        if (Data.Peak.IsHard && Config.BossExcel != null) stars = 3;

        return (Math.Min(stars, 3), finishedIds);
    }

    #endregion
}
