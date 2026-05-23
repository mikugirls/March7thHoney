using March7thHoney.Database.Inventory;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Expedition;

public class PacketTakeMultipleExpeditionRewardScRsp : BasePacket
{
    public PacketTakeMultipleExpeditionRewardScRsp(PlayerInstance player, Retcode retcode, List<ItemData>? rewards = null)
        : base(CmdIds.TakeMultipleExpeditionRewardScRsp)
    {
        var proto = new TakeMultipleExpeditionRewardScRsp
        {
            KAMBBFDEBAM = player.ExpeditionManager!.GetRefreshTime(),
            Retcode = (uint)retcode
        };

        if (rewards != null && rewards.Count > 0)
        {
            var itemList = new ItemList();
            itemList.ItemList_.Add(rewards.Select(x => x.ToProto()));
            proto.HALFGLGLDLO.Add(itemList);
        }

        proto.ANMHKDANNDL.Add(player.ExpeditionManager!.GetActiveExpeditionInfo().Select(x => x.Id));

        SetData(proto);
    }
}
