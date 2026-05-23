using March7thHoney.GameServer.Server.Packet.Send.Tutorial;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Tutorial;

[Opcode(CmdIds.FinishTutorialCsReq)]
public class HandlerFinishTutorialCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = FinishTutorialCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        if (player.TutorialData!.Tutorials.TryGetValue((int)req.TutorialId, out var res))
            if (res != TutorialStatus.TutorialFinish)
                player.TutorialData!.Tutorials[(int)req.TutorialId] = TutorialStatus.TutorialFinish;

        await connection.SendPacket(new PacketFinishTutorialScRsp(req.TutorialId));
    }
}
