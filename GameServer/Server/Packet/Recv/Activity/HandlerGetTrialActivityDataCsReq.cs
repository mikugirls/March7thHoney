using March7thHoney.GameServer.Server.Packet.Send.Activity;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Activity;

[Opcode(CmdIds.GetTrialActivityDataCsReq)]
public class HandlerGetTrialActivityDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetTrialActivityDataCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(new PacketGetTrialActivityDataScRsp(connection.Player!));
    }
}
