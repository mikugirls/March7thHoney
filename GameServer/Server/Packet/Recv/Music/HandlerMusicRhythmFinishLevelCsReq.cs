using March7thHoney.GameServer.Server.Packet.Send.Music;
using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server.Packet.Recv.Music;

[Opcode(CmdIds.MusicRhythmFinishLevelCsReq)]
public class HandlerMusicRhythmFinishLevelCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var curLevel = connection.Player!.Data.CurMusicLevel;
        await connection.SendPacket(new PacketMusicRhythmFinishLevelScRsp((uint)curLevel));
    }
}
