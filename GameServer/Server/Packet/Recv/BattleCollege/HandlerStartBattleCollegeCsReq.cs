using March7thHoney.GameServer.Server.Packet.Send.BattleCollege;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.BattleCollege;

[Opcode(CmdIds.StartBattleCollegeCsReq)]
public class HandlerStartBattleCollegeCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = StartBattleCollegeCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        var resp = player.BattleManager?.StartBattleCollege((int)req.Id);
        if (resp != null)
            await connection.SendPacket(new PacketStartBattleCollegeScRsp(req.Id, resp.Value.Item1, resp.Value.Item2));
        else
            await connection.SendPacket(new PacketStartBattleCollegeScRsp(req.Id, Retcode.RetWaitLogin, null));
    }
}
