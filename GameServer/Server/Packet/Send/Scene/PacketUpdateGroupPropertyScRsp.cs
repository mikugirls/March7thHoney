using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Scene;

public class PacketUpdateGroupPropertyScRsp : BasePacket
{
    public PacketUpdateGroupPropertyScRsp(Retcode code) : base(CmdIds.UpdateGroupPropertyScRsp)
    {
        var proto = new UpdateGroupPropertyScRsp
        {
            Retcode = (uint)code
        };

        SetData(proto);
    }

    public PacketUpdateGroupPropertyScRsp(GroupPropertyRefreshData data, UpdateGroupPropertyCsReq req) : base(
        CmdIds.UpdateGroupPropertyScRsp)
    {
        var proto = new UpdateGroupPropertyScRsp
        {
            DimensionId = req.DimensionId,
            FloorId = req.FloorId,
            GroupId = (uint)data.GroupId,
            ELKOCIJNABK = data.NewValue,
            FNAODNGJAMM = data.OldValue,
            ELNCJFFJFIH = data.PropertyName
        };

        SetData(proto);
    }
}

