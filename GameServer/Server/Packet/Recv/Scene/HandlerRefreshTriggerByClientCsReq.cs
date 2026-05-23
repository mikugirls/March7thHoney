using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.RefreshTriggerByClientCsReq)]
public class HandlerRefreshTriggerByClientCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RefreshTriggerByClientCsReq.Parser.ParseFrom(data);

        var player = connection.Player!;
        var ret = await player.SceneInstance!.TriggerSummonUnit((int)req.TriggerEntityId, req.TriggerName,
            req.TriggerTargetIdList.ToList());

        await connection.SendPacket(new PacketRefreshTriggerByClientScRsp(ret, req.TriggerName, req.TriggerEntityId));
    }
}
