using March7thHoney.GameServer.Server.Packet.Send.ServerPrefs;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.ServerPrefs;

[Opcode(CmdIds.GetAllServerPrefsDataCsReq)]
public class HandlerGetAllServerPrefsDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var infos = connection.Player?.ServerPrefsData?.ServerPrefsDict.Values.ToList() ?? [];
        await connection.SendPacket(new PacketGetAllServerPrefsDataScRsp(infos));
    }
}
