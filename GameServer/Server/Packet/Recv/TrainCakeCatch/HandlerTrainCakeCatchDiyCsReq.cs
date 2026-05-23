using March7thHoney.GameServer.Server.Packet.Send.TrainCakeCatch;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.TrainCakeCatch;

[Opcode(CmdIds.TrainCakeCatchDiyCsReq)]
public class HandlerTrainCakeCatchDiyCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TrainCakeCatchDiyCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        var diy = player.TrainCakeCatchManager!.ApplyDiy(req.PEOECDDANNP ?? new FOFNOBHLKGO());
        await connection.SendPacket(new PacketTrainCakeCatchDiyScRsp(diy));

        
        
        await player.TrainCakeCatchManager.BroadcastRoomSnapshotAsync();
    }
}
