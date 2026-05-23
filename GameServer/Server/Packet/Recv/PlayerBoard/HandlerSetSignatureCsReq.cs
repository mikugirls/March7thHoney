using March7thHoney.GameServer.Server.Packet.Send.PlayerBoard;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.PlayerBoard;

[Opcode(CmdIds.SetSignatureCsReq)]
public class HandlerSetSignatureCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetSignatureCsReq.Parser.ParseFrom(data);

        connection.Player!.Data.Signature = req.Signature;

        await connection.SendPacket(new PacketSetSignatureScRsp(req.Signature));
    }
}
