using March7thHoney.GameServer.Server.Packet.Send.Phone;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Phone;

[Opcode(CmdIds.SetPersonalCardCsReq)]
public class HandlerSetPersonalCardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetPersonalCardCsReq.Parser.ParseFrom(data);

        connection.Player!.Data.PersonalCard = (int)req.Id;

        await connection.SendPacket(new PacketSetPersonalCardScRsp(req.Id));
        await connection.Player.TrainCakeCatchManager!.BroadcastPlayerStateAsync();
    }
}
