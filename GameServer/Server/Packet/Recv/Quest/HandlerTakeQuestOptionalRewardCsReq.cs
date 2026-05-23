using March7thHoney.GameServer.Server.Packet.Send.Quest;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Quest;

[Opcode(CmdIds.TakeQuestOptionalRewardCsReq)]
public class HandlerTakeQuestOptionalRewardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TakeQuestOptionalRewardCsReq.Parser.ParseFrom(data);
        var (retcode, rewards) =
            await connection.Player!.QuestManager!.TakeQuestOptionalReward((int)req.QuestId, (int)req.OptionalRewardId);

        await connection.SendPacket(new PacketTakeQuestOptionalRewardScRsp(req.QuestId, retcode, rewards));
    }
}
