using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Battle;

[Opcode(CmdIds.PVEBattleResultCsReq)]
public class HandlerPVEBattleResultCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = PVEBattleResultCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        await player.BattleManager!.EndBattle(req);
    }
}
