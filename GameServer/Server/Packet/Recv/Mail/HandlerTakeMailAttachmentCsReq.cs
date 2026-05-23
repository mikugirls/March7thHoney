using March7thHoney.GameServer.Server.Packet.Send.Mail;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Mail;

[Opcode(CmdIds.TakeMailAttachmentCsReq)]
public class HandlerTakeMailAttachmentCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TakeMailAttachmentCsReq.Parser.ParseFrom(data);
        var mailManager = connection.Player!.MailManager!;
        IEnumerable<uint> mailIds = req.MailIdList.Count > 0 ? req.MailIdList : mailManager.GetMailIdsWithAttachments();
        var result = await mailManager.TakeAttachments(mailIds);

        await connection.SendPacket(new PacketTakeMailAttachmentScRsp(result));
    }
}
