using March7thHoney.Kcp;
using March7thHoney.Proto;
using Google.Protobuf.Collections;

namespace March7thHoney.GameServer.Server.Packet.Send.PlayerBoard;

public class PacketSetDisplayAvatarScRsp : BasePacket
{
    public PacketSetDisplayAvatarScRsp(RepeatedField<DisplayAvatarData> avatars) : base(CmdIds.SetDisplayAvatarScRsp)
    {
        var proto = new SetDisplayAvatarScRsp();
        proto.DisplayAvatarList.AddRange(avatars);

        SetData(proto);
    }
}
