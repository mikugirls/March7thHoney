using March7thHoney.GameServer.Server.Packet.Send.Avatar;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.MarkAvatarCsReq)]
public class HandlerMarkAvatarCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = MarkAvatarCsReq.Parser.ParseFrom(data);

        var avatar = await connection.Player!.MarkAvatar((int)req.AvatarId, req.IsMarked);
        await connection.SendPacket(new PacketMarkAvatarScRsp(avatar));
    }
}
