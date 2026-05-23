using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Player;

public class PacketPlayerSqueezedScNotify : BasePacket
{
    public PacketPlayerSqueezedScNotify() : base(CmdIds.PlayerSqueezedScNotify)
    {
        var proto = new PlayerSqueezedScNotify
        {
            KickType = KickType.KickSqueezed
        };
        SetData(proto);
    }

    public PacketPlayerSqueezedScNotify(KickType type, BlackInfo? info = null) : base(CmdIds.PlayerSqueezedScNotify)
    {
        var proto = new PlayerSqueezedScNotify
        {
            KickType = type
        };

        if (info != null) proto.BlackInfo = info;

        SetData(proto);
    }
}
