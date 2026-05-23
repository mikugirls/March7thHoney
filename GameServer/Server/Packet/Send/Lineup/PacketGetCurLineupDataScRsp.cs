using March7thHoney.GameServer.Game.Player;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Lineup;

public class PacketGetCurLineupDataScRsp : BasePacket
{
    public PacketGetCurLineupDataScRsp(PlayerInstance player) : base(CmdIds.GetCurLineupDataScRsp)
    {
        var data = new GetCurLineupDataScRsp
        {
            Lineup = player.LineupManager?.GetCurLineup()?.ToProto() ?? new LineupInfo()
        };

        SetData(data);
    }
}
