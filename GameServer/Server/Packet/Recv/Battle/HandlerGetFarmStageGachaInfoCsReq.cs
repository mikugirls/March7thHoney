using March7thHoney.GameServer.Server.Packet.Send.Battle;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Battle;

[Opcode(CmdIds.GetFarmStageGachaInfoCsReq)]
public class HandlerGetFarmStageGachaInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetFarmStageGachaInfoCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketGetFarmStageGachaInfoScRsp(req));
    }
}
