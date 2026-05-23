using March7thHoney.GameServer.Server.Packet.Send.Lineup;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Lineup;

[Opcode(CmdIds.SwitchLineupIndexCsReq)]
public class HandlerSwitchLineupIndexCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SwitchLineupIndexCsReq.Parser.ParseFrom(data);
        if (await connection.Player!.LineupManager!
                .SetCurLineup((int)req.Index)) 
            await connection.SendPacket(new PacketSwitchLineupIndexScRsp(req.Index));
    }
}
