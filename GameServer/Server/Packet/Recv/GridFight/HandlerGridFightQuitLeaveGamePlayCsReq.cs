using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightQuitLeaveGamePlayCsReq)]
public class HandlerGridFightQuitLeaveGamePlayCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = GridFightQuitLeaveGamePlayCsReq.Parser.ParseFrom(data);
        connection.Player!.GridFightManager!.GridFightInstance = null;
        var rsp = new BasePacket((ushort)CmdIds.GridFightQuitLeaveGamePlayScRsp);
        rsp.SetData(new CENLNIAOMAK());
        await connection.SendPacket(rsp);
    }
}
