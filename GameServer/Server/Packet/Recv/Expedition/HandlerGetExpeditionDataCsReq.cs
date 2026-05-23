using March7thHoney.GameServer.Server.Packet.Send.Expedition;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Expedition;

[Opcode(CmdIds.GetExpeditionDataCsReq)]
public class HandlerGetExpeditionDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetExpeditionDataScRsp(connection.Player!));
    }
}
