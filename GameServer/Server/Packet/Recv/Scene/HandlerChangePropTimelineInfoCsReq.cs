using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.ChangePropTimelineInfoCsReq)]
public class HandlerChangePropTimelineInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ChangePropTimelineInfoCsReq.Parser.ParseFrom(data);

        await connection.Player!.SetPropTimeline((int)req.PropEntityId, req.TimelineInfo);
        await connection.SendPacket(new PacketChangePropTimelineInfoScRsp(req.PropEntityId));
    }
}
