using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Player;

[Opcode(CmdIds.ForceSyncGameStateFinishCSReq)]
public class HandlerForceSyncGameStateFinishCSReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = ForceSyncGameStateFinishCSReq.Parser.ParseFrom(data);

        var packet = new BasePacket((ushort)CmdIds.ForceSyncGameStateFinishScRsp);
        packet.SetData(new ForceSyncGameStateFinishScRsp());
        await connection.SendPacket(packet);
    }
}