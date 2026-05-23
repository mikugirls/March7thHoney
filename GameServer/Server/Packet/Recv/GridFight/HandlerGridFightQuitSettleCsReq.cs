using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightQuitSettleCsReq)]
public class HandlerGridFightQuitSettleCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = GridFightQuitSettleCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        var keepWinCnt = player.GridFightManager?.GridFightInstance?.KeepWinCnt ?? 0;

        await connection.SendPacket(new PacketGridFightWeeklyExtraSeasonExpScRsp());
        await connection.SendPacket(new PacketGridFightSyncKeepWinCntNotify(keepWinCnt));
        await connection.SendPacket(new PacketGridFightSettleNotify(player));
        player.GridFightManager!.GridFightInstance = null;

        var rsp = new BasePacket((ushort)CmdIds.GCOBEPOGHPL);
        rsp.SetData(new GCOBEPOGHPL());
        await connection.SendPacket(rsp);
    }
}
