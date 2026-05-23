using March7thHoney.GameServer.Server.Packet.Send.Friend;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Friend;

[Opcode(CmdIds.SetFriendMarkCsReq)]
public class HandlerSetFriendMarkCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetFriendMarkCsReq.Parser.ParseFrom(data);

        connection.Player!.FriendManager!.MarkFriend((int)req.Uid, req.KBNKNAHGPHG);

        await connection.SendPacket(new PacketSetFriendMarkScRsp(req.Uid, req.KBNKNAHGPHG));
    }
}

