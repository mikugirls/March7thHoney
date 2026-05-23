using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Activity;

[Opcode(CmdIds.GetAvatarRewardDataCsReq)]
public class HandlerGetAvatarRewardDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = GetAvatarRewardDataCsReq.Parser.ParseFrom(data);

        var proto = new GetAvatarRewardDataScRsp
        {
            AvatarId = 1415,
            IsTakenReward = false
        };

        var rsp = new BasePacket((ushort)CmdIds.GetAvatarRewardDataScRsp);
        rsp.SetData(proto);
        await connection.SendPacket(rsp);
    }
}
