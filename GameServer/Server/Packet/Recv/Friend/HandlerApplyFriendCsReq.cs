using March7thHoney.GameServer.Server.Packet.Send.Friend;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Friend;

[Opcode(CmdIds.ApplyFriendCsReq)]
public class HandlerApplyFriendCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ApplyFriendCsReq.Parser.ParseFrom(data);

        var ret = await connection.Player!.FriendManager!.AddFriend((int)req.Uid);

        await connection.SendPacket(new PacketApplyFriendScRsp(ret, req.Uid));
    }
}
