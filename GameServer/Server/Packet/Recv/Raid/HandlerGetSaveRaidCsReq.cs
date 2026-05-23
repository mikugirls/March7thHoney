using March7thHoney.GameServer.Server.Packet.Send.Raid;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Raid;

[Opcode(CmdIds.GetSaveRaidCsReq)]
public class HandlerGetSaveRaidCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetSaveRaidCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(
            new PacketGetSaveRaidScRsp(connection.Player!, (int)req.RaidId, (int)req.WorldLevel));
    }
}
