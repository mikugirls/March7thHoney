using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.DiceCombat;

[Opcode(CmdIds.DiceCombatMainPageRollDiceCsReq)]
public class HandlerDiceCombatMainPageRollDiceCsReq : Handler
{
    public override Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        return Task.CompletedTask;
    }
}
