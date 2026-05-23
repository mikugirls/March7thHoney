using March7thHoney.GameServer.Server.Packet.Send.Phone;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Phone;

[Opcode(CmdIds.SelectChatBubbleCsReq)]
public class HandlerSelectChatBubbleCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SelectChatBubbleCsReq.Parser.ParseFrom(data);

        connection.Player!.Data.ChatBubble = (int)req.BubbleId;

        await connection.SendPacket(new PacketSelectChatBubbleScRsp(req.BubbleId));
        await connection.Player.TrainCakeCatchManager!.BroadcastPlayerStateAsync();
    }
}
