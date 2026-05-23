using March7thHoney.GameServer.Server.Packet.Send.Activity;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Activity;

[Opcode(CmdIds.TakeRewardCsReq)]
public class HandlerTakeRewardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = TakeRewardCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketTakeRewardScRsp());
    }
}
