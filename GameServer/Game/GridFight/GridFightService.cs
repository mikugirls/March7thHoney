using March7thHoney.GameServer.Game.Player;
using March7thHoney.Proto;

namespace March7thHoney.GameServer.Game.GridFight;

public sealed class GridFightService(PlayerInstance player)
{
    private GridFightManager Manager => player.GridFightManager!;

    public GridFightInstance? Current => Manager.GridFightInstance;

    public GridFightInstance EnsureOrStart(uint season = 1, uint divisionId = 10940, bool isOverlock = false)
    {
        var (_, inst) = Manager.StartGamePlay(season, divisionId, isOverlock);
        return inst!;
    }

    public GridFightInstance StartAndPrepare(uint season, uint divisionId, bool isOverlock)
    {
        var inst = EnsureOrStart(season, divisionId, isOverlock);
        if (inst.PendingAction == null)
        {
            inst.PendingAction = new GridFightPendingAction
            {
                QueuePosition = inst.QueuePosition,
                PortalBuffAction = new GridFightPortalBuffActionInfo
                {
                    FCHPJKAIBHB = 1,
                    GridFightPortalBuffList = { inst.EnsurePortalBuffOffer() }
                }
            };
        }
        return inst;
    }

    public List<GridFightPosInfo> UpdatePos(IEnumerable<GridFightPosInfo> posInfoList)
    {
        var inst = Current;
        var list = posInfoList.ToList();
        inst?.ApplyPositionList(list);
        return list;
    }

    public (uint roleId, uint roleUniqueId, uint pos, int goldDelta, List<uint> mergedRemoved, uint mergedKeepUid, uint mergedNewStar) BuyGoods(IList<uint> buyGoodsIndexList)
    {
        var inst = Current;
        if (inst == null || buyGoodsIndexList.Count == 0)
            return (0, 0, 14, 0, [], 0, 0);

        var idx = (int)buyGoodsIndexList[0];
        var (ok, uid, rid, addedPos) = inst.TryBuyGoods(idx);
        if (!ok) return (0, 0, 14, 0, [], 0, 0);

        var merge = inst.TryAutoMergeRole(rid, uid);
        return (rid, uid, addedPos == 0 ? 14u : addedPos, -1, merge.RemovedUniqueIds, merge.KeptUniqueId, merge.NewStar);
    }

    public bool RefreshShop()
    {
        return Current?.TryRefreshShop() ?? false;
    }

    public uint RecycleRole(uint uniqueId)
    {
        var inst = Current;
        if (inst == null) return 0;
        var (_, refund) = inst.TryRecycleRole(uniqueId);
        return refund;
    }

    public bool BuyExp()
    {
        return Current?.TryBuyExp() ?? false;
    }
}
