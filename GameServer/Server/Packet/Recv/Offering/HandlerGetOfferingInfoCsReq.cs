using March7thHoney.Database.Inventory;
using March7thHoney.GameServer.Server.Packet.Send.Offering;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Offering;

[Opcode(CmdIds.GetOfferingInfoCsReq)]
public class HandlerGetOfferingInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetOfferingInfoCsReq.Parser.ParseFrom(data);

        List<OfferingTypeData> dataList = [];
        dataList.AddRange(req.OfferingIdList.Select(id => connection.Player!.OfferingManager!.GetOfferingData((int)id))
            .OfType<OfferingTypeData>());

        await connection.SendPacket(new PacketGetOfferingInfoScRsp(dataList));
    }
}
