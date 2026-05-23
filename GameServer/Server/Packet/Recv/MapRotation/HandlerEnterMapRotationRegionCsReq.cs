using March7thHoney.GameServer.Server.Packet.Send.MapRotation;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.MapRotation;

[Opcode(CmdIds.EnterMapRotationRegionCsReq)]
public class HandlerEnterMapRotationRegionCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = EnterMapRotationRegionCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketEnterMapRotationRegionScRsp(req.Motion));
    }
}
