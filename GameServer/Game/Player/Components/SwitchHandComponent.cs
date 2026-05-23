using March7thHoney.Data;
using March7thHoney.Database.Scene;
using March7thHoney.Proto;
using March7thHoney.Util;

namespace March7thHoney.GameServer.Game.Player.Components;

public class SwitchHandComponent(PlayerInstance player) : BasePlayerComponent(player)
{
    public int RunningHandConfigId { get; set; } = 0;

    public List<SwitchHandInfo> GetHandInfos()
    {
        List<SwitchHandInfo> infos = [];
        foreach (var configId in GameData.MazePuzzleSwitchHandData.Keys)
        {
            var info = GetHandInfo(configId);
            if (info.Item2 == null) continue;
            infos.Add(info.Item2);
        }

        return infos;
    }

    public (Retcode, SwitchHandInfo?) GetHandInfo(int configId)
    {
        var excel = GameData.MazePuzzleSwitchHandData.GetValueOrDefault(configId);
        if (excel == null) return (Retcode.RetInteractConfigNotExist, null);
        if (Player.SceneData!.SwitchHandData.TryGetValue(configId, out var info)) return (Retcode.RetSucc, info);

        
        info = new SwitchHandInfo
        {
            ConfigId = configId
        };
        
        var floorInfo = GameData.GetFloorInfo(excel.FloorID);
        if (floorInfo == null) return (Retcode.RetInteractConfigNotExist, null);
        if (!floorInfo.Groups.TryGetValue(excel.SwitchHandID[0], out var groupInfo))
            return (Retcode.RetReqParaInvalid, null);
        var prop = groupInfo.PropList.FirstOrDefault(x => x.ID == excel.SwitchHandID[1]);
        if (prop == null) return (Retcode.RetReqParaInvalid, null);

        info.Pos = prop.ToPositionProto();
        info.Rot = prop.ToRotationProto();

        Player.SceneData.SwitchHandData[configId] = info;
        return (Retcode.RetSucc, info);
    }

    public (Retcode, SwitchHandInfo?) UpdateHandInfo(GODHDEIPDJL info)
    {
        var dbInfo = GetHandInfo((int)info.ConfigId).Item2;
        if (dbInfo == null) return (Retcode.RetInteractConfigNotExist, null);

        dbInfo.Pos = info.PLFAOCPBBCP.Pos.ToPosition();
        dbInfo.Rot = info.PLFAOCPBBCP.Rot.ToPosition();
        dbInfo.State = info.JLMJFEDNBMF;
        dbInfo.ByteValue = info.MHINKADJCCG.ToByteArray();

        return (Retcode.RetSucc, dbInfo);
    }
}
