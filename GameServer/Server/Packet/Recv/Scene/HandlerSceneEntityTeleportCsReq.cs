using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.SceneEntityTeleportCsReq)]
public class HandlerSceneEntityTeleportCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SceneEntityTeleportCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        if (req.EntryId != player.Data.EntryId) await player.EnterScene((int)req.EntryId, 0, false);
        player.MoveTo(req.EntityMotion);

        await connection.SendPacket(new PacketSceneEntityTeleportScRsp(req.EntityMotion));
    }
}
