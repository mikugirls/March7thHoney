using March7thHoney.Enums.Item;
using March7thHoney.GameServer.Server.Packet.Send.Item;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Item;

[Opcode(CmdIds.LockEquipmentCsReq)]
public class HandlerLockEquipmentCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = LockEquipmentCsReq.Parser.ParseFrom(data);
        var result =
            await connection.Player!.InventoryManager!.LockItems(req.EquipmentIdList, req.IsLocked,
                ItemMainTypeEnum.Equipment);
        await connection.SendPacket(new PacketLockEquipmentScRsp(result));
    }
}

