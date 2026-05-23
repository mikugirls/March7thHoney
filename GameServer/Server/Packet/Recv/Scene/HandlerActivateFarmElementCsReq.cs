using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.ActivateFarmElementCsReq)]
public class HandlerActivateFarmElementCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ActiveFarmElementCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;

        player.ActiveFarmElementEntityId = req.EntityId;
        player.FarmElementReturnPos = player.Data.Pos;
        player.FarmElementReturnRot = player.Data.Rot;

        await connection.SendPacket(new PacketActivateFarmElementScRsp(req.EntityId, req.WorldLevel));
    }
}

