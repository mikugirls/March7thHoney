using March7thHoney.GameServer.Server.Packet.Send.Player;
using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.SetClientPausedCsReq)]
public class HandlerSetClientPausedCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetClientPausedCsReq.Parser.ParseFrom(data);
        var paused = req.Paused;
        await connection.SendPacket(new PacketSetClientPausedScRsp(paused));
        var player = connection.Player;
        if (player != null) await player.TrySendWelcomeAnnounce();
    }
}
