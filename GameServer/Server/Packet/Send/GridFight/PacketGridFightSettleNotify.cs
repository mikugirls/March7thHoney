using March7thHoney.Kcp;
using March7thHoney.Proto;
using March7thHoney.GameServer.Game.Player;

namespace March7thHoney.GameServer.Server.Packet.Send.GridFight;

public class PacketGridFightSettleNotify : BasePacket
{
    public PacketGridFightSettleNotify(PlayerInstance player) : base(CmdIds.GridFightSettleNotify)
    {
        SetData(player.GridFightManager!.BuildSettleNotify());
    }
}
