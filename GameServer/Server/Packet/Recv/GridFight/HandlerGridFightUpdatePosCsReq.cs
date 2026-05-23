using March7thHoney.GameServer.Game.GridFight;
using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightUpdatePosCsReq)]
public class HandlerGridFightUpdatePosCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GridFightUpdatePosCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        var service = new GridFightService(player);
        if (service.Current == null)
        {
            await connection.SendPacket(new PacketGridFightUpdatePosScRsp());
            return;
        }

        var posList = service.UpdatePos(req.GridFightPosInfoList);

        await connection.SendPacket(new PacketGridFightUpdatePosScRsp(posList));
        await connection.SendPacket(new PacketGridFightSyncUpdateResultScNotify(player, posList));
    }
}
