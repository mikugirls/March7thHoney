using March7thHoney.Kcp;
using March7thHoney.Proto;
using March7thHoney.GameServer.Server.Packet.Send.TalkEvent;

namespace March7thHoney.GameServer.Server.Packet.Recv.TalkEvent;

[Opcode(CmdIds.GetFirstTalkNpcCsReq)]
public class HandlerGetFirstTalkNpcCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetFirstTalkNpcCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketGetFirstTalkNpcScRsp(req));
    }
}
