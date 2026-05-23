using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightGetDataCsReq)]
public class HandlerGridFightGetDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = GridFightGetDataCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketGridFightGetDataScRsp(connection.Player!));
    }
}
