using March7thHoney.Enums.Mission;
using March7thHoney.GameServer.Server.Packet.Send.HeartDial;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.HeartDial;

[Opcode(CmdIds.FinishEmotionDialoguePerformanceCsReq)]
public class HandlerFinishEmotionDialoguePerformanceCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = FinishEmotionDialoguePerformanceCsReq.Parser.ParseFrom(data);

        var player = connection.Player!;
        await player.MissionManager!.HandleFinishType(MissionFinishTypeEnum.HeartDialDialoguePerformanceFinish,
            $"HeartDial_{req.DialogueId}");

        await connection.SendPacket(new PacketFinishEmotionDialoguePerformanceScRsp(req.ScriptId, req.DialogueId));
    }
}
