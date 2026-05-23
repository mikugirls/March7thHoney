using March7thHoney.GameServer.Server.Packet.Send.Item;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Item;

[Opcode(CmdIds.GetBagCsReq)]
public class HandlerGetBagCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetBagScRsp(connection.Player!));
    }
}
