using March7thHoney.GameServer.Game.GridFight;
using March7thHoney.GameServer.Game.GridFight.PendingAction;
using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightSelectRecommendEquipCsReq)]
public class HandlerGridFightSelectRecommendEquipCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GridFightSelectRecommendEquipCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        var inst = player.GridFightManager?.GridFightInstance;

        if (inst == null)
        {
            await connection.SendPacket(new PacketGridFightHandlePendingActionScRsp(req.QueuePosition));
            await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(player));
            return;
        }

        await GridFightPendingActionProcessor.Handle(connection, inst, req);
    }
}
