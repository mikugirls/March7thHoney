using March7thHoney.GameServer.Game.Player;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.GridFight;

public class PacketGridFightGetDataScRsp : BasePacket
{
    public PacketGridFightGetDataScRsp(PlayerInstance player) : base(CmdIds.GridFightGetDataScRsp)
    {
        var proto = new GridFightGetDataScRsp
        {
            RogueGetInfo = player.GridFightManager!.ToProto()
        };
        if (player.GridFightManager.GridFightInstance != null)
            proto.FightCurrentInfo = player.GridFightManager.BuildCurrentInfo();
        SetData(proto);
    }
}
