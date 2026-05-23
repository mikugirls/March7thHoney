using March7thHoney.Data;
using March7thHoney.Enums.Mission;
using March7thHoney.GameServer.Server.Packet.Send.HeartDial;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.HeartDial;

[Opcode(CmdIds.SubmitEmotionItemCsReq)]
public class HandlerSubmitEmotionItemCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SubmitEmotionItemCsReq.Parser.ParseFrom(data);

        GameData.HeartDialScriptData.TryGetValue((int)req.ScriptId, out var scriptData);
        if (scriptData != null)
        {
            var info = connection.Player!.HeartDialData!.ChangeScriptEmotion((int)req.ScriptId,
                scriptData.DefaultEmoType, HeartDialStepTypeEnum.UnLock);
            await connection.Player!.SendPacket(
                new PacketHeartDialScriptChangeScNotify(HeartDialUnlockStatus.UnlockAll, info));
            await connection.Player!.MissionManager!.HandleFinishType(MissionFinishTypeEnum.HeartDialScriptListStep);
        }

        await connection.SendPacket(new PacketSubmitEmotionItemScRsp(req.ScriptId));
    }
}
