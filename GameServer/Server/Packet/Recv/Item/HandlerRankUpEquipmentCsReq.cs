using March7thHoney.GameServer.Server.Packet.Send.Item;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Item;

[Opcode(CmdIds.RankUpEquipmentCsReq)]
public class HandlerRankUpEquipmentCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RankUpEquipmentCsReq.Parser.ParseFrom(data);
        var retcode = await connection.Player!.InventoryManager!.RankUpEquipment((int)req.EquipmentUniqueId, req.CostData);
        await connection.SendPacket(new PacketRankUpEquipmentScRsp(retcode));
    }
}
