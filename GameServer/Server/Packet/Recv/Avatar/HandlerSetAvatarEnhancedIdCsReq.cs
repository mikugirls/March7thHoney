using March7thHoney.Data;
using March7thHoney.GameServer.Server.Packet.Send.Avatar;
using March7thHoney.GameServer.Server.Packet.Send.PlayerSync;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.SetAvatarEnhancedIdCsReq)]
public class HandlerSetAvatarEnhancedIdCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetAvatarEnhancedIdCsReq.Parser.ParseFrom(data);
        var targetAvatarId = (int)req.AvatarId;
        var targetEnhancedId = (int)req.EnhancedId;

        var avatar = connection.Player!.AvatarManager!.GetFormalAvatar(targetAvatarId);
        if (avatar == null)
        {
            await connection.SendPacket(new PacketSetAvatarEnhancedIdScRsp(Retcode.RetAvatarNotExist));
            return;
        }

        
        var path = avatar.GetPathInfo(targetAvatarId) ?? avatar.GetCurPathInfo();
        if (path == null)
        {
            await connection.SendPacket(new PacketSetAvatarEnhancedIdScRsp(Retcode.RetAvatarNotExist));
            return;
        }

        
        if (GameData.AvatarConfigData.TryGetValue(path.PathId, out var pathExcel))
        {
            var exists = pathExcel.SkillTree.ContainsKey(targetEnhancedId) ||
                         pathExcel.DefaultSkillTree.ContainsKey(targetEnhancedId);
            path.EnhanceId = exists ? targetEnhancedId : 0;
        }
        else
        {
            path.EnhanceId = targetEnhancedId;
        }

        _ = path.GetSkillTree();

        await connection.Player.SendPacket(new PacketSetAvatarEnhancedIdScRsp((uint)path.PathId, path.EnhanceId));
        await connection.Player.SendPacket(new PacketPlayerSyncScNotify(avatar));
    }
}

