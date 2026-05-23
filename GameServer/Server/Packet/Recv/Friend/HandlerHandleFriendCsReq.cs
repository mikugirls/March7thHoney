using March7thHoney.Database.Player;
using March7thHoney.GameServer.Server.Packet.Send.Friend;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Friend;

[Opcode(CmdIds.HandleFriendCsReq)]
public class HandlerHandleFriendCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = HandleFriendCsReq.Parser.ParseFrom(data);

        PlayerData? playerData = null;
        if (req.IsAccept)
            playerData = await connection.Player!.FriendManager!.ConfirmAddFriend((int)req.Uid);
        else
            await connection.Player!.FriendManager!.RefuseAddFriend((int)req.Uid);

        if (playerData != null)
            await connection.SendPacket(new PacketHandleFriendScRsp(req.Uid, req.IsAccept, playerData));
        else
            await connection.SendPacket(new PacketHandleFriendScRsp(req.Uid, req.IsAccept));
    }
}
