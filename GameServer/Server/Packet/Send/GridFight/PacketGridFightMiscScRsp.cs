using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.GridFight;

public class PacketGridFightEquipDressScRsp : BasePacket { public PacketGridFightEquipDressScRsp() : base(CmdIds.JNNNJBAOCOB) => SetData(new JNNNJBAOCOB()); }
public class PacketEPJJBNPIFLC : BasePacket { public PacketEPJJBNPIFLC() : base(CmdIds.EPJJBNPIFLC) => SetData(new EPJJBNPIFLC()); }
public class PacketOLOGIALOJDP : BasePacket { public PacketOLOGIALOJDP() : base(CmdIds.OLOGIALOJDP) => SetData(new OLOGIALOJDP()); }
public class PacketEEBMIAFNJMC : BasePacket { public PacketEEBMIAFNJMC() : base(CmdIds.EEBMIAFNJMC) => SetData(new EEBMIAFNJMC()); }
public class PacketDJCHCHCAJPB : BasePacket { public PacketDJCHCHCAJPB() : base(CmdIds.DJCHCHCAJPB) => SetData(new DJCHCHCAJPB()); }
public class PacketHGOIBDBMDBG : BasePacket { public PacketHGOIBDBMDBG() : base(CmdIds.HGOIBDBMDBG) => SetData(new HGOIBDBMDBG()); }
public class PacketGridFightRecycleRoleScRsp : BasePacket { public PacketGridFightRecycleRoleScRsp() : base(CmdIds.GridFightRecycleRoleScRsp) => SetData(new GridFightRecycleRoleScRsp()); }
public class PacketGridFightUpdateEquipTrackScRsp : BasePacket { public PacketGridFightUpdateEquipTrackScRsp() : base(CmdIds.GridFightUpdateEquipTrackScRsp) => SetData(new GridFightUpdateEquipTrackScRsp()); }
public class PacketCEFIMADBIBH : BasePacket { public PacketCEFIMADBIBH() : base(CmdIds.CEFIMADBIBH) => SetData(new CEFIMADBIBH()); }

public class PacketKMDHLENLIMF : BasePacket { public PacketKMDHLENLIMF() : base(CmdIds.KMDHLENLIMF) => SetData(new KMDHLENLIMF()); }
public class PacketEMIDPCIJAEN : BasePacket { public PacketEMIDPCIJAEN() : base(CmdIds.EMIDPCIJAEN) => SetData(new EMIDPCIJAEN()); }
public class PacketMNBAAGCGICN : BasePacket { public PacketMNBAAGCGICN() : base(CmdIds.MNBAAGCGICN) => SetData(new MNBAAGCGICN()); }
public class PacketDJCCBIOKNOD : BasePacket { public PacketDJCCBIOKNOD() : base(CmdIds.DJCCBIOKNOD) => SetData(new DJCCBIOKNOD()); }
public class PacketGDMIIBNJJEJ : BasePacket { public PacketGDMIIBNJJEJ() : base(CmdIds.GridFightBackToPrepareScRsp) => SetData(new GDMIIBNJJEJ()); }
public class PacketGridFightGetArchiveScRsp : BasePacket { public PacketGridFightGetArchiveScRsp() : base(CmdIds.GridFightGetArchiveScRsp) => SetData(new GridFightGetArchiveScRsp()); }
public class PacketGridFightPermanentTalentEnableScRsp : BasePacket { public PacketGridFightPermanentTalentEnableScRsp() : base(CmdIds.GridFightPermanentTalentEnableScRsp) => SetData(new GridFightPermanentTalentEnableScRsp()); }
public class PacketGridFightPermanentTalentResetScRsp : BasePacket { public PacketGridFightPermanentTalentResetScRsp() : base(CmdIds.GridFightPermanentTalentResetScRsp) => SetData(new GridFightPermanentTalentResetScRsp()); }
public class PacketGridFightSeasonTalentEnableScRsp : BasePacket { public PacketGridFightSeasonTalentEnableScRsp() : base(CmdIds.GridFightSeasonTalentEnableScRsp) => SetData(new GridFightSeasonTalentEnableScRsp()); }
public class PacketGridFightSeasonTalentResetScRsp : BasePacket { public PacketGridFightSeasonTalentResetScRsp() : base(CmdIds.GridFightSeasonTalentResetScRsp) => SetData(new GridFightSeasonTalentResetScRsp()); }
public class PacketGridFightSyncKeepWinCntNotify : BasePacket { public PacketGridFightSyncKeepWinCntNotify(uint cnt) : base(CmdIds.GridFightSyncKeepWinCntNotify) => SetData(new GridFightSyncKeepWinCntNotify { LBJPDJFHIBF = cnt }); }
public class PacketGridFightWeeklyExtraSeasonExpScRsp : BasePacket { public PacketGridFightWeeklyExtraSeasonExpScRsp(uint extraExp = 337) : base(CmdIds.GridFightWeeklyExtraSeasonExpScRsp) => SetData(new GridFightWeeklyExtraSeasonExpScRsp { CurTakenExtraExp = extraExp }); }
public class PacketGridFightSeasonHandBookNotify : BasePacket { public PacketGridFightSeasonHandBookNotify(GridFightSeasonHandBookNotify n) : base(CmdIds.GridFightSeasonHandBookNotify) => SetData(n); }
