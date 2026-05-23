using March7thHoney.GameServer.Server.Packet.Send.Item;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Item;

[Opcode(CmdIds.ComposeItemCsReq)]
public class HandlerComposeItemCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ComposeItemCsReq.Parser.ParseFrom(data);

        var costData = new List<ItemCost>();
        if (req.ComposeItemList != null)
            costData = [.. req.ComposeItemList.ItemList];

        var item = await connection.Player!.InventoryManager!.ComposeItem(
            (int)req.ComposeId, (int)req.Count, costData);
        if (item == null)
        {
            await connection.SendPacket(new PacketComposeItemScRsp());
            return;
        }

        await connection.SendPacket(new PacketComposeItemScRsp(req.ComposeId, req.Count, item));
    }
}
