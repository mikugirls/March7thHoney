using March7thHoney.GameServer.Server.Packet.Send.Shop;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Shop;

[Opcode(CmdIds.GetShopListCsReq)]
public class HandlerGetShopListCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetShopListCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(new PacketGetShopListScRsp(req.ShopType));
    }
}
