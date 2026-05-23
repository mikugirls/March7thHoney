using March7thHoney.GameServer.Server.Packet.Send.Phone;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Phone;

[Opcode(CmdIds.SelectPhoneThemeCsReq)]
public class HandlerSelectPhoneThemeCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SelectPhoneThemeCsReq.Parser.ParseFrom(data);

        connection.Player!.Data.PhoneTheme = (int)req.ThemeId;

        await connection.SendPacket(new PacketSelectPhoneThemeScRsp(req.ThemeId));
    }
}
