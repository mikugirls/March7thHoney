using March7thHoney.GameServer.Server.Packet.Send.ChallengePeak;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.ChallengePeak;

[Opcode(CmdIds.TakeChallengePeakRewardCsReq)]
public class HandlerTakeChallengePeakRewardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TakeChallengePeakRewardCsReq.Parser.ParseFrom(data);
        var rewardIds = req.RewardId.Count > 0 ? req.RewardId : req.NormalRewardIdList;
        var rewardGroups = await connection.Player!.ChallengePeakManager!.TakeRewards((int)req.PeakGroupId, rewardIds);
        await connection.SendPacket(new PacketTakeChallengePeakRewardScRsp(req.PeakGroupId, rewardGroups));
    }
}
