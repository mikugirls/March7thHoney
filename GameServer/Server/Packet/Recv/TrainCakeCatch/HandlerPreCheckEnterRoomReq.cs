using March7thHoney.Kcp;
using March7thHoney.Proto;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Server.Packet.Recv.TrainCakeCatch;

[Opcode(CmdIds.PreCheckEnterRoomReq)]
public class HandlerPreCheckEnterRoomReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = PreCheckEnterRoomReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        var roomOwnerUid = ResolveRoomOwnerUid(player.Uid, req.TeleportId);
        player.TrainCakeCatchManager!.PrepareSocialPlayRoom(roomOwnerUid);

        var preCheckPacket = new BasePacket(CmdIds.SocialPlayPreCheckEnterRoomScRsp);
        preCheckPacket.SetData(new SocialPlayPreCheckEnterRoomScRsp
        {
            DHLGCEGONIB = player.TrainCakeCatchManager.GetRoomRefreshTime(roomOwnerUid)
        });
        await connection.SendPacket(preCheckPacket);
    }

    private static uint ResolveRoomOwnerUid(int playerUid, uint teleportId)
    {
        if (teleportId == 0)
            return (uint)playerUid;

        var serverProfileUid = (uint)ConfigManager.Config.ServerOption.ServerProfile.Uid;
        if (teleportId == serverProfileUid)
            
            
            
            
            return (uint)playerUid;

        return teleportId;
    }
}
