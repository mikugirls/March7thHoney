using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.DeactivateFarmElementCsReq)]
public class HandlerDeactivateFarmElementCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = DeactivateFarmElementCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;

        if (player.ActiveFarmElementEntityId == req.EntityId)
        {
            player.ActiveFarmElementEntityId = 0;
            player.FarmElementReturnPos = null;
            player.FarmElementReturnRot = null;
        }

        await connection.SendPacket(new PacketDeactivateFarmElementScRsp(req.EntityId));
    }
}
