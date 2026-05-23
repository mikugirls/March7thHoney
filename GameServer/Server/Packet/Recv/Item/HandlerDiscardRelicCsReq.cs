using March7thHoney.Enums.Item;
using March7thHoney.GameServer.Server.Packet.Send.Item;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Item;

[Opcode(CmdIds.DiscardRelicCsReq)]
public class HandlerDiscardRelicCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = DiscardRelicCsReq.Parser.ParseFrom(data);
        var result =
            await connection.Player!.InventoryManager!.DiscardItems(req.RelicIds, req.Discarded,
                ItemMainTypeEnum.Relic);
        await connection.SendPacket(new PacketDiscardRelicScRsp(result, req.Discarded));
    }
}

