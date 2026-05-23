using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Activity;

public class PacketTakeRewardScRsp : BasePacket
{
    public PacketTakeRewardScRsp() : base(CmdIds.TakeRewardScRsp)
    {
        var proto = new TakeRewardScRsp
        {
            Retcode = (uint)Retcode.RetSucc,
            Reward = new ItemList
            {
                ItemList_ =
                {
                    new March7thHoney.Proto.Item { ItemId = 1, Num = 1600 },
                    new March7thHoney.Proto.Item { ItemId = 201, Num = 10 }
                }
            }
        };

        SetData(proto);
    }
}
