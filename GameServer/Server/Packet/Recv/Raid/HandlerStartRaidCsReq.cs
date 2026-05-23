using March7thHoney.GameServer.Server.Packet.Send.Raid;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Raid;

[Opcode(CmdIds.StartRaidCsReq)]
public class HandlerStartRaidCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = StartRaidCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;

        var record = await player.RaidManager!.EnterRaid((int)req.RaidId, (int)req.WorldLevel,
            req.AvatarList.Select(x => (int)x).ToList(),
            req.IsSave == 1);

        if (record == null)
            await connection.SendPacket(new PacketStartRaidScRsp(Retcode.RetReqParaInvalid));
        else
            await connection.SendPacket(new PacketStartRaidScRsp(record, player));
    }
}
