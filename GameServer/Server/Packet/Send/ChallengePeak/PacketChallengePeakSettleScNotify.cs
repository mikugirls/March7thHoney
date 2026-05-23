using March7thHoney.GameServer.Game.Challenge.Instances;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.ChallengePeak;

public class PacketChallengePeakSettleScNotify : BasePacket
{
    public PacketChallengePeakSettleScNotify(ChallengePeakInstance inst, List<uint> targetIdList) : base(
        CmdIds.ChallengePeakSettleScNotify)
    {
        var proto = new ChallengePeakSettleScNotify
        {
            CyclesUsed = inst.Data.Peak.RoundCnt,
            IsWin = inst.IsWin,
            PeakId = inst.Data.Peak.CurrentPeakLevelId,
            FinishedTargetList = { targetIdList },
            HardModeHasPassed = inst is { IsWin: true, Config.BossExcel: not null } && inst.Data.Peak.IsHard
        };

        SetData(proto);
    }
}
