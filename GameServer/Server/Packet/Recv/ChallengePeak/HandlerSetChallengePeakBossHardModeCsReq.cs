using March7thHoney.GameServer.Server.Packet.Send.ChallengePeak;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.ChallengePeak;

[Opcode(CmdIds.SetChallengePeakBossHardModeCsReq)]
public class HandlerSetChallengePeakBossHardModeCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetChallengePeakBossHardModeCsReq.Parser.ParseFrom(data);

        connection.Player!.ChallengePeakManager!.BossIsHard = req.IsHardMode;

        await connection.SendPacket(new PacketSetChallengePeakBossHardModeScRsp(req.PeakGroupId, req.IsHardMode));
    }
}
