using March7thHoney.GameServer.Server.Packet.Send.Player;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Player;

[Opcode(CmdIds.UpdatePlayerSettingCsReq)]
public class HandlerUpdatePlayerSettingCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = UpdatePlayerSettingCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketUpdatePlayerSettingScRsp(req.PlayerSetting));
    }
}
