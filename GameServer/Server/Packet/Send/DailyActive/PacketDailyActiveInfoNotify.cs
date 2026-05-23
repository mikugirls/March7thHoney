using March7thHoney.GameServer.Game.DailyActive;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.DailyActive;

public class PacketDailyActiveInfoNotify : BasePacket
{
    public PacketDailyActiveInfoNotify() : base(CmdIds.DailyActiveInfoNotify)
    {
        var proto = new DailyActiveInfoNotify
        {
            DailyActivePoint = DailyActiveDefaults.FixedPoint
        };
        proto.DailyActiveLevelList.AddRange(DailyActiveDefaults.CreateLevels(true));
        proto.DailyActiveQuestIdList.AddRange(DailyActiveDefaults.QuestIds);
        SetData(proto);
    }
}
