using March7thHoney.GameServer.Game.Player;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Lineup;

public class PacketGetAllLineupDataScRsp : BasePacket
{
    public PacketGetAllLineupDataScRsp(PlayerInstance player) : base(CmdIds.GetAllLineupDataScRsp)
    {
        var proto = new GetAllLineupDataScRsp
        {
            CurIndex = (uint)player.LineupManager!.LineupData.CurLineup
        };
        foreach (var lineup in player.LineupManager.GetAllLineup()) proto.LineupList.Add(lineup.ToProto());

        SetData(proto);
    }
}
