using March7thHoney.Enums.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Scene;

public class PacketGroupStateChangeScNotify : BasePacket
{
    public PacketGroupStateChangeScNotify(int entryId, int groupId, PropStateEnum propState) : base(
        CmdIds.GroupStateChangeScNotify)
    {
        var notify = new GroupStateChangeScNotify
        {
            GroupStateInfo = new GroupStateInfo
            {
                EntryId = (uint)entryId,
                GroupId = (uint)groupId,
                GroupState = (uint)propState
            }
        };

        SetData(notify);
    }
}
