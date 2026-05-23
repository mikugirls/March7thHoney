using March7thHoney.GameServer.Game.GridFight;
using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightStartGamePlayCsReq)]
public class HandlerGridFightStartGamePlayCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GridFightStartGamePlayCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        _ = new GridFightService(player).StartAndPrepare(req.Season, req.DivisionId, req.IsOverlock);
        await connection.SendPacket(new PacketINHDFEIOBNK(player));
    }
}
