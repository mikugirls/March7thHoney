using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Gacha;

public class PacketSetGachaDecideItemScRsp : BasePacket
{
    public PacketSetGachaDecideItemScRsp(uint gachaId, List<uint> order) : base(CmdIds.SetGachaDecideItemScRsp)
    {
        var proto = new SetGachaDecideItemScRsp
        {
            LECPJJAMNPF = new OEIEJHBCOOM
            {
                PAPOKACIPPJ = gachaId,
                DGOMHDMJHEK = { order },
                MBOEFLAHLEM = 1
            }
        };

        SetData(proto);
    }
}

