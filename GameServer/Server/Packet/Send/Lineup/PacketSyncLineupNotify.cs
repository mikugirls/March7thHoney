using March7thHoney.Kcp;
using March7thHoney.Proto;
using LineupInfo = March7thHoney.Database.Lineup.LineupInfo;

namespace March7thHoney.GameServer.Server.Packet.Send.Lineup;

public class PacketSyncLineupNotify : BasePacket
{
    public PacketSyncLineupNotify(LineupInfo info, SyncLineupReason reason = SyncLineupReason.SyncReasonNone) : base(
        CmdIds.SyncLineupNotify)
    {
        var proto = new SyncLineupNotify
        {
            Lineup = info.ToProto()
        };

        if (reason != SyncLineupReason.SyncReasonNone) proto.ReasonList.Add(reason);

        SetData(proto);
    }
}
