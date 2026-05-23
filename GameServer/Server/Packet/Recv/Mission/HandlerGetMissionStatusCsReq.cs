using March7thHoney.GameServer.Server.Packet.Send.Mission;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Mission;

[Opcode(CmdIds.GetMissionStatusCsReq)]
public class HandlerGetMissionStatusCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetMissionStatusCsReq.Parser.ParseFrom(data);
        if (req != null) await connection.SendPacket(new PacketGetMissionStatusScRsp(req, connection.Player!));
    }
}
