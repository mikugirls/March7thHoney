using March7thHoney.GameServer.Server.Packet.Send.Chat;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Chat;

[Opcode(CmdIds.GetChatFriendHistoryCsReq)]
public class HandlerGetChatFriendHistoryCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var history = connection.Player!.FriendManager!.FriendData.ChatHistory;

        await connection.SendPacket(new PacketGetChatFriendHistoryScRsp(history));
    }
}
