using March7thHoney.GameServer.Server.Packet.Send.Raid;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Raid;

[Opcode(CmdIds.GetRaidInfoCsReq)]
public class HandlerGetRaidInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetRaidInfoScRsp(connection.Player!));
    }
}
