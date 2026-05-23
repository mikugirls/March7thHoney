using March7thHoney.GameServer.Server.Packet.Send.Challenge;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Challenge;

[Opcode(CmdIds.GetChallengeCsReq)]
public class HandlerGetChallengeCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetChallengeScRsp(connection.Player!));
    }
}
