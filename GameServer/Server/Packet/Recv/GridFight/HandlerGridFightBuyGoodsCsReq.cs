using March7thHoney.GameServer.Game.GridFight.Sync;
using March7thHoney.GameServer.Game.GridFight;
using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightBuyGoodsCsReq)]
public class HandlerGridFightBuyGoodsCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GridFightBuyGoodsCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        var service = new GridFightService(player);
        var (roleId, roleUniqueId, pos, goldDelta, mergedRemoved, mergedKeepUid, mergedNewStar) = service.BuyGoods(req.BuyGoodsIndexList);

        var rsp = new BasePacket((ushort)CmdIds.CEFIMADBIBH);
        rsp.SetData(new CEFIMADBIBH());
        await connection.SendPacket(rsp);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(player, GridFightSyncKind.BuyGoods,
            (roleId, roleUniqueId, pos, goldDelta, mergedRemoved, mergedKeepUid, mergedNewStar)));
    }
}
