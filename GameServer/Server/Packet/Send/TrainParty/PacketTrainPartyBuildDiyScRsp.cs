using March7thHoney.Database.TrainParty;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.TrainParty;

public class PacketTrainPartyBuildDiyScRsp : BasePacket
{
    public PacketTrainPartyBuildDiyScRsp(TrainAreaInfo? area) : base(CmdIds.TrainPartyBuildDiyScRsp)
    {
        var proto = area == null
            ? new TrainPartyBuildDiyScRsp
            {
                Retcode = (uint)Retcode.RetTrainPartyDiyTagNotMatch
            }
            : new TrainPartyBuildDiyScRsp
            {
                AreaId = (uint)area.AreaId,
                DynamicInfo =
                {
                    area.DynamicInfo.Select(x => new AreaDynamicInfo
                    {
                        DiceSlotId = (uint)x.Key,
                        DiyDynamicId = (uint)x.Value
                    })
                }
                
                
                
                
                
                
                
                
                
            };

        SetData(proto);
    }
}
