namespace March7thHoney.GameServer.Game.GridFight.Component;

public sealed class GridFightBattleComponent
{
    public uint StageId { get; private set; } = 90303;
    public List<uint> MonsterIds { get; } = new() { 300101007u, 800102021u, 300101007u };

    public void SetEncounter(uint stageId, IEnumerable<uint> monsterIds)
    {
        if (stageId > 0) StageId = stageId;
        MonsterIds.Clear();
        MonsterIds.AddRange(monsterIds.Where(x => x > 0));
        if (MonsterIds.Count == 0)
        {
            MonsterIds.Add(300101007u);
            MonsterIds.Add(800102021u);
            MonsterIds.Add(300101007u);
        }
    }
}
