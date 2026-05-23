using March7thHoney.GameServer.Game.Player;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Expedition;

public class PacketAcceptMultipleExpeditionScRsp : BasePacket
{
    public PacketAcceptMultipleExpeditionScRsp(PlayerInstance player, IEnumerable<ExpeditionInfo> accepted)
        : base(CmdIds.AcceptMultipleExpeditionScRsp)
    {
        var proto = new AcceptMultipleExpeditionScRsp
        {
            KAMBBFDEBAM = player.ExpeditionManager!.GetRefreshTime()
        };

        proto.AcceptMultiExpedition.Add(accepted);

        SetData(proto);
    }
}
