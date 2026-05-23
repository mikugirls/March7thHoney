using March7thHoney.Kcp;
using March7thHoney.Proto;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Server.Packet.Send.Player;

public class PacketPlayerHeartBeatScRsp : BasePacket
{
    public PacketPlayerHeartBeatScRsp(long clientTime) : base(CmdIds.PlayerHeartBeatScRsp)
    {
        var data = new PlayerHeartBeatScRsp
        {
            ClientTimeMs = (ulong)clientTime,
            ServerTimeMs = (ulong)ServerTimeProvider.GetServerUnixMs()
        };

        SetData(data);
    }
}
