using March7thHoney.GameServer.Game.DailyActive;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.DailyActive;

public class PacketGetDailyActiveInfoScRsp : BasePacket
{
    public PacketGetDailyActiveInfoScRsp() : base(CmdIds.GetDailyActiveInfoScRsp)
    {
        var proto = new GetDailyActiveInfoScRsp
        {
            DailyActivePoint = DailyActiveDefaults.FixedPoint
        };
        proto.DailyActiveLevelList.AddRange(DailyActiveDefaults.CreateLevels(true));
        proto.DailyActiveQuestIdList.AddRange(DailyActiveDefaults.QuestIds);
        SetData(proto);
    }
}
