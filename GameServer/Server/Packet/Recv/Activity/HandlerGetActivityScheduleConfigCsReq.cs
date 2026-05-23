using March7thHoney.GameServer.Server.Packet.Send.Activity;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Activity;

[Opcode(CmdIds.GetActivityScheduleConfigCsReq)]
public class HandlerGetActivityScheduleConfigCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetActivityScheduleConfigScRsp(connection.Player!));
    }
}
