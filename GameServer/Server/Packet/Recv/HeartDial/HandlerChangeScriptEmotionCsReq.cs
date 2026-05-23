using March7thHoney.Enums.Mission;
using March7thHoney.GameServer.Server.Packet.Send.HeartDial;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.HeartDial;

[Opcode(CmdIds.ChangeScriptEmotionCsReq)]
public class HandlerChangeScriptEmotionCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ChangeScriptEmotionCsReq.Parser.ParseFrom(data);

        connection.Player!.HeartDialData!.ChangeScriptEmotion((int)req.ScriptId,
            (HeartDialEmoTypeEnum)req.TargetEmotionType);

        await connection.SendPacket(new PacketChangeScriptEmotionScRsp(req.ScriptId, req.TargetEmotionType));
    }
}
