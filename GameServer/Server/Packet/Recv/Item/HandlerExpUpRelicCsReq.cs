using March7thHoney.GameServer.Server.Packet.Send.Item;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Item;

[Opcode(CmdIds.ExpUpRelicCsReq)]
public class HandlerExpUpRelicCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ExpUpRelicCsReq.Parser.ParseFrom(data);

        var left = await connection.Player!.InventoryManager!.LevelUpRelic((int)req.RelicUniqueId, req.CostData);

        await connection.SendPacket(new PacketExpUpRelicScRsp(left));
    }
}
