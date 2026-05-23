using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.GridFight;

public class PacketGridFightUpdatePosScRsp : BasePacket
{
    public PacketGridFightUpdatePosScRsp(IEnumerable<GridFightPosInfo>? posInfoList = null) : base(CmdIds.GridFightUpdatePosScRsp)
    {
        var proto = new GridFightUpdatePosScRsp();
        if (posInfoList != null)
            proto.GridFightPosInfoList.AddRange(posInfoList);
        SetData(proto);
    }
}
