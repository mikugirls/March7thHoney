using March7thHoney.GameServer.Game.Battle;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.Kcp;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Server.Packet.Send.Battle;

public class PacketPVEBattleResultScRsp : BasePacket
{
    public PacketPVEBattleResultScRsp() : base(CmdIds.PVEBattleResultScRsp)
    {
        var proto = new PVEBattleResultScRsp
        {
            Retcode = 1
        };

        SetData(proto);
    }

    public PacketPVEBattleResultScRsp(PVEBattleResultCsReq req, PlayerInstance player, BattleInstance battle) : base(
        CmdIds.PVEBattleResultScRsp)
    {
        var proto = new PVEBattleResultScRsp
        {
            DropData = battle.GetDropItemList(),
            StageId = req.StageId,
            BattleId = req.BattleId,
            EndStatus = req.EndStatus,
            CheckIdentical = true,
            ItemListUnk1 = new ItemList(),
            ItemListUnk2 = new ItemList(),
            MultipleDropData = new ItemList(),
            EventId = (uint)battle.EventId
        };

        SetData(proto);
    }
}
