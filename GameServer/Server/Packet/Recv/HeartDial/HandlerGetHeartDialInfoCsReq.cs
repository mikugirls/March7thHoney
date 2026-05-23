using March7thHoney.GameServer.Server.Packet.Send.HeartDial;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.HeartDial;

[Opcode(CmdIds.GetHeartDialInfoCsReq)]
public class HandlerGetHeartDialInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetHeartDialInfoScRsp(connection.Player!));
    }
}
