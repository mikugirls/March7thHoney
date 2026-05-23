using March7thHoney.GameServer.Game.Player;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Expedition;

public class PacketGetExpeditionDataScRsp : BasePacket
{
    public PacketGetExpeditionDataScRsp(PlayerInstance player) : base(CmdIds.GetExpeditionDataScRsp)
    {
        var manager = player.ExpeditionManager!;
        var proto = new GetExpeditionDataScRsp
        {
            KAMBBFDEBAM = manager.GetRefreshTime(),
            TotalExpeditionCount = manager.GetTotalExpeditionCount()
        };

        proto.NBKFAEDOGPG.Add(manager.GetAllExpeditionIds());
        proto.ExpeditionInfo.Add(manager.GetActiveExpeditionInfo());

        SetData(proto);
    }
}
