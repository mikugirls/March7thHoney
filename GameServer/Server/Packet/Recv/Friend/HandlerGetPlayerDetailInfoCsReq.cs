using March7thHoney.GameServer.Server.Packet.Send.Friend;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Friend;

[Opcode(CmdIds.GetPlayerDetailInfoCsReq)]
public class HandlerGetPlayerDetailInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetPlayerDetailInfoCsReq.Parser.ParseFrom(data);

        var playerData = connection.Player!.FriendManager!.GetFriendPlayerData([(int)req.Uid]).FirstOrDefault();
        if (playerData == null)
        {
            await connection.SendPacket(new PacketGetPlayerDetailInfoScRsp());
            return;
        }

        await connection.SendPacket(new PacketGetPlayerDetailInfoScRsp(playerData.ToDetailProto()));
    }
}
