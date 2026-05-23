using March7thHoney.GameServer.Server.Packet.Send.Gacha;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Gacha;

[Opcode(CmdIds.GetGachaInfoCsReq)]
public class HandlerGetGachaInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetGachaInfoScRsp(connection.Player!));
    }
}
