using March7thHoney.GameServer.Server.Packet.Send.ContentPackage;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.ContentPackage;

[Opcode(CmdIds.ContentPackageGetDataCsReq)]
public class HandlerContentPackageGetDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ContentPackageGetDataCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(
            new PacketContentPackageGetDataScRsp()); 
    }
}
