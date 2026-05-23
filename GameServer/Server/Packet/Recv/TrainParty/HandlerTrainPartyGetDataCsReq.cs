using March7thHoney.GameServer.Server.Packet.Send.TrainParty;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.TrainParty;

[Opcode(CmdIds.TrainPartyGetDataCsReq)]
public class HandlerTrainPartyGetDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var player = connection.Player!;
        var manager = player.TrainPartyManager!;
        manager.EnsureDefaults();

        await connection.SendPacket(new PacketTrainPartyGetDataScRsp(player));
        await connection.SendPacket(new PacketTrainPartyBuildRoomScNotify(player));
        await connection.SendPacket(new PacketTrainWorldIdChangeScNotify(manager.GetWorldId()));
        await connection.SendPacket(new PacketTrainRefreshTimeNotify(manager.GetRefreshTime()));
    }
}
