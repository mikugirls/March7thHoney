using March7thHoney.Data;
using March7thHoney.Enums.Mission;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Mission;

public class PacketGetMissionDataScRsp : BasePacket
{
    public PacketGetMissionDataScRsp(PlayerInstance player) : base(CmdIds.GetMissionDataScRsp)
    {
        var trackMissionId = player.MissionManager!.Data.TrackingMainMissionId;
        if (trackMissionId <= 0 || player.MissionManager.GetMainMissionStatus(trackMissionId) != MissionPhaseEnum.Accept)
            trackMissionId = player.MissionManager.Data.RunningMainMissionIds.FirstOrDefault();
        player.MissionManager.Data.TrackingMainMissionId = trackMissionId;

        var proto = new GetMissionDataScRsp
        {
            TrackMissionId = (uint)trackMissionId
        };

        foreach (var mission in GameData.MainMissionData.Keys)
            if (player.MissionManager!.GetMainMissionStatus(mission) == MissionPhaseEnum.Accept)
                proto.MainMissionList.Add(new MainMission
                {
                    Id = (uint)mission,
                    Status = MissionStatus.MissionDoing
                });

        foreach (var mission in GameData.SubMissionInfoData.Keys.Concat(GameData.SubMissionData.Keys))
            if (player.MissionManager!.GetSubMissionStatus(mission) == MissionPhaseEnum.Accept)
                proto.MissionList.Add(new Proto.Mission
                {
                    Id = (uint)mission,
                    Status = MissionStatus.MissionDoing,
                    Progress = (uint)player.MissionManager!.GetMissionProgress(mission)
                });

        SetData(proto);
    }
}
