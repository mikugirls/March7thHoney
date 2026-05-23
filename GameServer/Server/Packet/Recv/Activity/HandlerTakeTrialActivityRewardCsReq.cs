using March7thHoney.Data;
using March7thHoney.Database.Activity;
using March7thHoney.Database.Inventory;
using March7thHoney.GameServer.Server.Packet.Send.Activity;
using March7thHoney.GameServer.Server.Packet.Send.PlayerSync;
using March7thHoney.GameServer.Server.Packet.Send.Scene;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Recv.Activity;

[Opcode(CmdIds.TakeTrialActivityRewardCsReq)]
public class HandlerTakeTrialActivityRewardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TakeTrialActivityRewardCsReq.Parser.ParseFrom(data);

        GameData.AvatarDemoConfigData.TryGetValue((int)req.StageId, out var stage);
        if (stage != null)
        {
            GameData.RewardDataData.TryGetValue(stage.RewardID, out var reward);
            var itemList = new List<ItemData>();
            reward?.GetItems().ForEach(i =>
            {
                var res = connection.Player!.InventoryManager!.AddItem(i.Item1, i.Item2, false).Result;
                if (res != null) itemList.Add(res);
            });
            var activities = connection.Player!.ActivityManager!.Data.TrialActivityData.Activities;
            var activity = activities.Find(x => x.StageId == req.StageId);
            if (activity != null)
                activities[activities.FindIndex(x => x.StageId == req.StageId)] = new TrialActivityResultData
                {
                    StageId = (int)req.StageId,
                    TakenReward = true
                };
            connection.Player!.Data.Hcoin += reward!.Hcoin;

            await connection.Player!.SendPacket(new PacketPlayerSyncScNotify(connection.Player!.ToProto(), itemList));
            await connection.Player!.SendPacket(new PacketScenePlaneEventScNotify(itemList));
            await connection.SendPacket(new PacketTakeTrialActivityRewardScRsp(req.StageId, itemList));
        }
    }
}
