using March7thHoney.GameServer.Server.Packet.Send.GridFight;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.GridFight;

[Opcode(CmdIds.GridFightFinishTutorialCsReq)]
public class HandlerGridFightFinishTutorialCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = GridFightFinishTutorialCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketEMIDPCIJAEN());
    }
}
