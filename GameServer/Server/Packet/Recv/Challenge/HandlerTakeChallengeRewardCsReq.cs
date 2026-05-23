using March7thHoney.GameServer.Server.Packet.Send.Challenge;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Challenge;

[Opcode(CmdIds.TakeChallengeRewardCsReq)]
public class HandlerTakeChallengeRewardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TakeChallengeRewardCsReq.Parser.ParseFrom(data);

        var rewardInfos = await connection.Player!.ChallengeManager!.TakeRewards((int)req.GroupId)!;
        await connection.SendPacket(new PacketTakeChallengeRewardScRsp((int)req.GroupId, rewardInfos));
    }
}
