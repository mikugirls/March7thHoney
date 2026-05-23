using March7thHoney.Database;
using March7thHoney.GameServer.Server.Packet.Send.Avatar;
using March7thHoney.GameServer.Server.Packet.Send.PlayerSync;
using March7thHoney.Kcp;
using March7thHoney.Proto;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.SetPlayerOutfitCsReq)]
public class HandlerSetPlayerOutfitCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetPlayerOutfitCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;

        player.Data.PlayerOutfitList.Clear();
        if (req.PlayerOutfitData != null)
            foreach (var id in req.PlayerOutfitData.PlayerOutfitList)
                player.Data.PlayerOutfitList.Add((int)id);

        DatabaseHelper.ToSaveUidList.Add(player.Uid);

        await connection.SendPacket(new PacketPlayerSyncScNotify(player.Data.ToPlayerOutfitProto()));
        await connection.SendPacket(new PacketSetPlayerOutfitScRsp());
        await player.TrainCakeCatchManager!.BroadcastPlayerStateAsync();
    }
}
