using March7thHoney.GameServer.Server.Packet.Send.Mission;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Mission;

[Opcode(CmdIds.UpdateTrackMainMissionIdCsReq)]
public class HandlerUpdateTrackMainMissionIdCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = UpdateTrackMainMissionCsReq.Parser.ParseFrom(data);

        var missionData = connection.Player!.MissionManager!.Data;
        var prev = missionData.TrackingMainMissionId;
        var target = ResolveTrackMissionId(req, missionData, prev);
        missionData.TrackingMainMissionId = target;

        await connection.SendPacket(new PacketUpdateTrackMainMissionIdScRsp(prev,
            missionData.TrackingMainMissionId));
    }

    private static int ResolveTrackMissionId(UpdateTrackMainMissionCsReq req, March7thHoney.Database.Quests.MissionData data,
        int prev)
    {
        if (req.TrackMissionId > 1) return (int)req.TrackMissionId;
        if (req.GNPDGLGBIMM > 1) return (int)req.GNPDGLGBIMM;
        if (prev > 0 && data.RunningMainMissionIds.Contains(prev)) return prev;
        return data.RunningMainMissionIds.Count > 0 ? data.RunningMainMissionIds[0] : 0;
    }
}

