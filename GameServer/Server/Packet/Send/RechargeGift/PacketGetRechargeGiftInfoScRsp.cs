using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.RechargeGift;

public class PacketGetRechargeGiftInfoScRsp : BasePacket
{
    public PacketGetRechargeGiftInfoScRsp() : base(CmdIds.GetRechargeGiftInfoScRsp)
    {
        var proto = new GetRechargeGiftInfoScRsp
        {
            FEKLJCAOGII =
            {
                new ABIPCNKGFNI
                {
                    DNNHCNCKJCM = 1777112805,
                    EndTime = 1777976805,
                    GiftType = 16,
                    FLHLJMEMIHL =
                    {
                        new PPPFOMBLOIF { Status = BNDHJNDFFED.Amjcmcjdpkd },
                        new PPPFOMBLOIF { Index = 2, Status = BNDHJNDFFED.Amjcmcjdpkd },
                        new PPPFOMBLOIF { Index = 3, Status = BNDHJNDFFED.Amjcmcjdpkd },
                        new PPPFOMBLOIF { Index = 4, Status = BNDHJNDFFED.Amjcmcjdpkd },
                        new PPPFOMBLOIF { Index = 5, Status = BNDHJNDFFED.Amjcmcjdpkd }
                    }
                }
            }
        };

        SetData(proto);
    }
}

