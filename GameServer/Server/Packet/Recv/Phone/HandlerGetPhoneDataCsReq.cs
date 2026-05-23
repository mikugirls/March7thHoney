using March7thHoney.GameServer.Server.Packet.Send.Phone;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Phone;

[Opcode(CmdIds.GetPhoneDataCsReq)]
public class HandlerGetPhoneDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetPhoneDataScRsp(connection.Player!));
    }
}
