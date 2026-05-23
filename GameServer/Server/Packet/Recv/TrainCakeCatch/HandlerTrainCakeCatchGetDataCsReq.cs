using March7thHoney.GameServer.Server.Packet.Send.TrainCakeCatch;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.TrainCakeCatch;

[Opcode(CmdIds.TrainCakeCatchGetDataCsReq)]
public class HandlerTrainCakeCatchGetDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var player = connection.Player!;
        player.TrainCakeCatchManager!.EnsureDefaults();
        await player.TrainCakeCatchManager!.UnlockAllPerformanceQuests();
        await connection.SendPacket(new PacketTrainCakeCatchGetDataScRsp(player));
    }
}
