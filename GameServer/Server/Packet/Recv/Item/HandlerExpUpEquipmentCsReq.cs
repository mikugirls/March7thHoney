using March7thHoney.GameServer.Server.Packet.Send.Item;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Item;

[Opcode(CmdIds.ExpUpEquipmentCsReq)]
public class HandlerExpUpEquipmentCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ExpUpEquipmentCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        var (retcode, returnItem) =
            await player.InventoryManager!.LevelUpEquipment((int)req.EquipmentUniqueId, req.CostData);

        await connection.SendPacket(new PacketExpUpEquipmentScRsp(retcode, returnItem));
    }
}
