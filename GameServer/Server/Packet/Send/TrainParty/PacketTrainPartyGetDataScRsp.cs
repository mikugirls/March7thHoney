using March7thHoney.GameServer.Game.Player;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.TrainParty;

public class PacketTrainPartyGetDataScRsp : BasePacket
{
    public PacketTrainPartyGetDataScRsp(PlayerInstance player) : base(CmdIds.TrainPartyGetDataScRsp)
    {
        var proto = new TrainPartyGetDataScRsp
        {
            TrainPartyData = player.TrainPartyManager!.ToProto()
        };

        SetData(proto);
    }
}
