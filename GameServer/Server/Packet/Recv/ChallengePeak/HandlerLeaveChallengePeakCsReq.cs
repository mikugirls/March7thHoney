using March7thHoney.Kcp;
using March7thHoney.Proto;
using March7thHoney.Util;
using static March7thHoney.GameServer.Plugin.Event.PluginEvent;

namespace March7thHoney.GameServer.Server.Packet.Recv.ChallengePeak;

[Opcode(CmdIds.LeaveChallengePeakCsReq)]
public class HandlerLeaveChallengePeakCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var player = connection.Player!;

        
        if (player.SceneInstance != null)
        {
            player.LineupManager!.SetExtraLineup(ExtraLineupType.LineupChallenge, []);

            InvokeOnPlayerQuitChallenge(player, player.ChallengeManager!.ChallengeInstance);

            player.ChallengeManager!.ChallengeInstance = null;
            player.ChallengeManager!.ClearInstance();

            
            player.LineupManager!.SetExtraLineup(ExtraLineupType.LineupNone, []);
            
            foreach (var avatar in player.LineupManager.GetCurLineup()!.AvatarData!.FormalAvatars)
                avatar.CurrentHp = 10000;

            var leaveEntryId = GameConstants.CHALLENGE_PEAK_ENTRANCE;
            if (player.SceneInstance.LeaveEntryId != 0) leaveEntryId = player.SceneInstance.LeaveEntryId;
            await player.EnterScene(leaveEntryId, 0, true);
        }

        await connection.SendPacket(CmdIds.LeaveChallengePeakScRsp);
    }
}
