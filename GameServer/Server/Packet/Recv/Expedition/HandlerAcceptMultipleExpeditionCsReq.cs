using March7thHoney.GameServer.Server.Packet.Send.Expedition;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Expedition;

[Opcode(CmdIds.AcceptMultipleExpeditionCsReq)]
public class HandlerAcceptMultipleExpeditionCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = AcceptMultipleExpeditionCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        var accepted = player.ExpeditionManager!.AcceptMultipleExpedition(req.Expedition);

        await connection.SendPacket(new PacketAcceptMultipleExpeditionScRsp(player, accepted));
    }
}
