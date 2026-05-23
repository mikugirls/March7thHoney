using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.RankUpAvatarCsReq)]
public class HandlerRankUpAvatarCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RankUpAvatarCsReq.Parser.ParseFrom(data);
        await connection.Player!.InventoryManager!.RankUpAvatar((int)req.AvatarId, req.CostData);
        await connection.SendPacket(CmdIds.RankUpAvatarScRsp);
    }
}
