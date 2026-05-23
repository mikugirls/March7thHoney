using March7thHoney.GameServer.Server.Packet.Send.Pet;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Pet;

[Opcode(CmdIds.RecallPetCsReq)]
public class HandlerRecallPetCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RecallPetCsReq.Parser.ParseFrom(data);

        connection.Player!.Data.Pet = 0;

        await connection.SendPacket(new PacketRecallPetScRsp(req.SummonedPetId));
        await connection.Player.TrainCakeCatchManager!.BroadcastPlayerStateAsync();
    }
}
