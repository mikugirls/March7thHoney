using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Activity;

public class PacketStartTrialActivityScRsp : BasePacket
{
    public PacketStartTrialActivityScRsp(uint stageId) : base(CmdIds.StartTrialActivityScRsp)
    {
        var proto = new StartTrialActivityScRsp
        {
            StageId = stageId
        };

        SetData(proto);
    }
}
