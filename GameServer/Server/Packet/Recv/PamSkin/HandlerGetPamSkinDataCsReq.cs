using March7thHoney.GameServer.Server.Packet.Send.PamSkin;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.PamSkin;

[Opcode(CmdIds.GetPamSkinDataCsReq)]
public class HandlerGetPamSkinDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetPamSkinDataScRsp(connection.Player!));
    }
}
