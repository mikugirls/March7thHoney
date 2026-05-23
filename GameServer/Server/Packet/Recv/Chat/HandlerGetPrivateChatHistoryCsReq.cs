using March7thHoney.GameServer.Server.Packet.Send.Chat;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Chat;

[Opcode(CmdIds.GetPrivateChatHistoryCsReq)]
public class HandlerGetPrivateChatHistoryCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetPrivateChatHistoryCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(
            new PacketGetPrivateChatHistoryScRsp(req.ContactSide, req.TargetSide, connection.Player!));
    }
}
