using March7thHoney.Database.Player;
using March7thHoney.GameServer.Server.Packet.Send.Friend;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Friend;

[Opcode(CmdIds.SearchPlayerCsReq)]
public class HandlerSearchPlayerCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SearchPlayerCsReq.Parser.ParseFrom(data);
        var playerList = new List<PlayerData>();

        foreach (var uid in req.UidList)
        {
            var player = connection.Player!.FriendManager!.GetFriendPlayerData([(int)uid])
                .FirstOrDefault(x => x.Uid == (int)uid);
            if (player != null) playerList.Add(player);
        }

        if (playerList.Count == 0)
            await connection.SendPacket(new PacketSearchPlayerScRsp());
        else
            await connection.SendPacket(new PacketSearchPlayerScRsp(playerList));
    }
}
