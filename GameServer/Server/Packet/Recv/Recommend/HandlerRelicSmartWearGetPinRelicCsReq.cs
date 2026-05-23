using March7thHoney.GameServer.Server.Packet.Send.Recommend;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Recommend;

[Opcode(CmdIds.RelicSmartWearGetPinRelicCsReq)]
public class HandlerRelicSmartWearGetPinRelicCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RelicSmartWearGetPinRelicCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketRelicSmartWearGetPinRelicScRsp(req.AvatarId));
    }
}
