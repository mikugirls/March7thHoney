using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.SetGroupCustomSaveDataCsReq)]
public class HandlerSetGroupCustomSaveDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetGroupCustomSaveDataCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        player.SetCustomSaveData((int)req.EntryId, (int)req.GroupId, req.SaveData);
        await connection.SendPacket(new PacketSetGroupCustomSaveDataScRsp(req.EntryId, req.GroupId));
    }
}
