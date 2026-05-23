using March7thHoney.GameServer.Server.Packet.Send.Phone;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Phone;

[Opcode(CmdIds.SelectPhoneCaseCsReq)]
public class HandlerSelectPhoneCaseCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SelectPhoneCaseCsReq.Parser.ParseFrom(data);

        connection.Player!.Data.PhoneCase = (int)req.PhoneCaseId;

        await connection.SendPacket(new PacketSelectPhoneCaseScRsp(req.PhoneCaseId));
    }
}

