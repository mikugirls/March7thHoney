using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.InteractPropCsReq)]
public class HandlerInteractPropCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = InteractPropCsReq.Parser.ParseFrom(data);
        
        
        var interactId = req.InteractId2 != 0 ? (int)req.InteractId2 : (int)req.InteractId;
        var prop = await connection.Player!.InteractProp((int)req.PropEntityId, interactId);
        await connection.SendPacket(new PacketInteractPropScRsp(prop));
    }
}
