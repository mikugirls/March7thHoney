using March7thHoney.GameServer.Server.Packet.Send.JukeBox;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.JukeBox;

[Opcode(CmdIds.GetJukeboxDataCsReq)]
public class HandlerGetJukeboxDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetJukeboxDataScRsp(connection.Player!));
    }
}
