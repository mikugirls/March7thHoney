using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Lineup;

[Opcode(CmdIds.ReplaceLineupCsReq)]
public class HandlerReplaceLineupCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ReplaceLineupCsReq.Parser.ParseFrom(data);
        await connection.Player!.LineupManager!.ReplaceLineup(req);
        await connection.SendPacket(CmdIds.ReplaceLineupScRsp);
    }
}
