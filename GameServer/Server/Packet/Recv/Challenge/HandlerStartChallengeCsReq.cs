using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Challenge;

[Opcode(CmdIds.StartChallengeCsReq)]
public class HandlerStartChallengeCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = StartChallengeCsReq.Parser.ParseFrom(data);

        ChallengeStoryBuffInfo? storyBuffInfo = null;
        if (req.StageInfo is { StoryInfo: not null })
            storyBuffInfo = req.StageInfo.StoryInfo;

        ChallengeBossBuffInfo? bossBuffInfo = null;
        if (req.StageInfo != null && req.StageInfo.BossInfo != null) bossBuffInfo = req.StageInfo.BossInfo;

        
        
        
        
        var firstLineup = req.BKNKLEOCJNO.Count > 0
            ? req.BKNKLEOCJNO.Select(x => (int)x.Id).Where(x => x > 0).ToList()
            : req.FirstLineup.Select(x => (int)x).Where(x => x > 0).ToList();
        if (firstLineup.Count > 0)
            await connection.Player!.LineupManager!.ReplaceLineup(0, firstLineup, ExtraLineupType.LineupChallenge);

        var secondLineup = req.ABNDFKFIKCI.Count > 0
            ? req.ABNDFKFIKCI.Select(x => (int)x.Id).Where(x => x > 0).ToList()
            : req.SecondLineup.Select(x => (int)x).Where(x => x > 0).ToList();
        if (secondLineup.Count > 0)
            await connection.Player!.LineupManager!.ReplaceLineup(0, secondLineup, ExtraLineupType.LineupChallenge2);

        await connection.Player!.ChallengeManager!.StartChallenge((int)req.ChallengeId, storyBuffInfo, bossBuffInfo);
    }
}
