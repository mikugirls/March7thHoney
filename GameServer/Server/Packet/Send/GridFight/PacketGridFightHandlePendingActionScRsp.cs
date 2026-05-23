using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.GridFight;

public class PacketGridFightHandlePendingActionScRsp : BasePacket
{
    public PacketGridFightHandlePendingActionScRsp(uint queuePosition) : base(CmdIds.GridFightHandlePendingActionScRsp)
    {
        var proto = new GridFightHandlePendingActionScRsp
        {
            QueuePosition = queuePosition
        };
        SetData(proto);
    }
}
