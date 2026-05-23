using March7thHoney.GameServer.Game.GridFight.Sync;
using March7thHoney.GameServer.Game.GridFight;
using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightSummonProjectionCsReq)]
public class HandlerGridFightSummonProjectionCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = GridFightSummonProjectionCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(connection.Player!, GridFightSyncKind.NoOp));
    }
}
