using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Player;

public class PacketRetcodeNotify : BasePacket
{
    public PacketRetcodeNotify(Retcode retcode) : base(CmdIds.PlayerSyncScNotify)
    {
        
        SetData(new PlayerSyncScNotify());
    }
}

