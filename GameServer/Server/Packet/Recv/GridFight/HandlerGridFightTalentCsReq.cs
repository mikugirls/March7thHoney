using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightPermanentTalentEnableCsReq)]
public class HandlerGridFightPermanentTalentEnableCsReq : Handler { public override async Task OnHandle(Connection connection, byte[] header, byte[] data) { _ = GridFightPermanentTalentEnableCsReq.Parser.ParseFrom(data); await connection.SendPacket(new PacketGridFightPermanentTalentEnableScRsp()); } }

[Opcode(CmdIds.GridFightPermanentTalentResetCsReq)]
public class HandlerGridFightPermanentTalentResetCsReq : Handler { public override async Task OnHandle(Connection connection, byte[] header, byte[] data) { _ = GridFightPermanentTalentResetCsReq.Parser.ParseFrom(data); await connection.SendPacket(new PacketGridFightPermanentTalentResetScRsp()); } }

[Opcode(CmdIds.GridFightSeasonTalentEnableCsReq)]
public class HandlerGridFightSeasonTalentEnableCsReq : Handler { public override async Task OnHandle(Connection connection, byte[] header, byte[] data) { _ = GridFightSeasonTalentEnableCsReq.Parser.ParseFrom(data); await connection.SendPacket(new PacketGridFightSeasonTalentEnableScRsp()); } }

[Opcode(CmdIds.GridFightSeasonTalentResetCsReq)]
public class HandlerGridFightSeasonTalentResetCsReq : Handler { public override async Task OnHandle(Connection connection, byte[] header, byte[] data) { _ = GridFightSeasonTalentResetCsReq.Parser.ParseFrom(data); await connection.SendPacket(new PacketGridFightSeasonTalentResetScRsp()); } }
