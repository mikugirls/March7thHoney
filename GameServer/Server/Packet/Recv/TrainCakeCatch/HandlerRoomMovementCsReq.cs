using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.TrainCakeCatch;

[Opcode(CmdIds.MKLFLKDHLIC)]
public class HandlerRoomMovementCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = MKLFLKDHLIC.Parser.ParseFrom(data);
        var player = connection.Player;
        if (player?.TrainCakeCatchManager == null)
            return;

        await player.TrainCakeCatchManager.HandleRoomMovement(req);
    }
}
