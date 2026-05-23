using March7thHoney.Kcp;
using March7thHoney.Proto;
using March7thHoney.Util;
using static March7thHoney.GameServer.Plugin.Event.PluginEvent;

namespace March7thHoney.GameServer.Server.Packet.Recv.Challenge;

[Opcode(CmdIds.LeaveChallengeCsReq)]
public class HandlerLeaveChallengeCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var player = connection.Player!;

        
        if (player.SceneInstance != null)
        {
            
            await player.ForceQuitBattle();

            
            player.LineupManager!.SetExtraLineup(ExtraLineupType.LineupChallenge, []);
            player.LineupManager.SetExtraLineup(ExtraLineupType.LineupChallenge2, []);

            InvokeOnPlayerQuitChallenge(player, player.ChallengeManager!.ChallengeInstance);

            player.ChallengeManager!.ChallengeInstance = null;
            player.ChallengeManager!.ClearInstance();

            
            player.LineupManager.SetExtraLineup(ExtraLineupType.LineupNone, []);
            
            foreach (var avatar in player.LineupManager.GetCurLineup()!.AvatarData!.FormalAvatars)
                avatar.CurrentHp = 10000;

            var leaveEntryId = GameConstants.CHALLENGE_ENTRANCE;
            if (player.SceneInstance.LeaveEntryId != 0) leaveEntryId = player.SceneInstance.LeaveEntryId;
            await player.EnterScene(leaveEntryId, 0, true);
        }

        await connection.SendPacket(CmdIds.LeaveChallengeScRsp);
    }
}
