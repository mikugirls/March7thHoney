using March7thHoney.GameServer.Game.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Scene;

public class PacketEnterSceneByServerScNotify : BasePacket
{
    public PacketEnterSceneByServerScNotify(SceneInstance scene) : base(CmdIds.EnterSceneByServerScNotify)
    {
        var sceneInfo = scene.ToProto();
        var notify = new EnterSceneByServerScNotify
        {
            Scene = sceneInfo,
            Lineup = scene.Player.LineupManager!.GetCurLineup()!.ToProto()
        };

        SetData(notify);
    }
}
