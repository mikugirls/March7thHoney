using March7thHoney.GameServer.Server.Packet.Send.Message;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Message;

[Opcode(CmdIds.GetNpcStatusCsReq)]
public class HandlerGetNpcStatusCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetNpcStatusScRsp(connection.Player!));
    }
}
