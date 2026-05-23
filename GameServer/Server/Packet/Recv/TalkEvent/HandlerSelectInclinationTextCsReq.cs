using March7thHoney.GameServer.Server.Packet.Send.TalkEvent;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.TalkEvent;

[Opcode(CmdIds.SelectInclinationTextCsReq)]
public class HandlerSelectInclinationTextCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SelectInclinationTextCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(new PacketSelectInclinationTextScRsp(req.TalkSentenceId));
    }
}
