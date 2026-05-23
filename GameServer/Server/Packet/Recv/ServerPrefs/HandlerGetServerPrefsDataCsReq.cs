using March7thHoney.GameServer.Server.Packet.Send.ServerPrefs;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.ServerPrefs;

[Opcode(CmdIds.GetServerPrefsDataCsReq)]
public class HandlerGetServerPrefsDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetServerPrefsDataCsReq.Parser.ParseFrom(data);

        var info = connection.Player!.ServerPrefsData?.ServerPrefsDict.GetValueOrDefault((int)req.ServerPrefsId);

        await connection.SendPacket(new PacketGetServerPrefsDataScRsp(info, req.ServerPrefsId));
    }
}
