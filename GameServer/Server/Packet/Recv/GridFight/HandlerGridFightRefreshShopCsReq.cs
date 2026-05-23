using March7thHoney.GameServer.Game.GridFight.Sync;
using March7thHoney.GameServer.Game.GridFight;
using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightRefreshShopCsReq)]
public class HandlerGridFightRefreshShopCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = GridFightRefreshShopCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        _ = new GridFightService(player).RefreshShop();

        var rsp = new BasePacket((ushort)CmdIds.CMJCEEDCIAN);
        rsp.SetData(new CMJCEEDCIAN());
        await connection.SendPacket(rsp);
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(player, GridFightSyncKind.RefreshShop));
    }
}
