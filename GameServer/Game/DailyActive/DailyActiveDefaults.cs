using March7thHoney.Proto;

namespace March7thHoney.GameServer.Game.DailyActive;

public static class DailyActiveDefaults
{
    public const uint FixedPoint = 500;

    public static readonly uint[] QuestIds =
    [
        2100132,
        2100133,
        2100139,
        2100152,
        2100153,
        2100155,
        2100156
    ];

    public static List<DailyActivityInfo> CreateLevels(bool isHasTaken)
    {
        return
        [
            new DailyActivityInfo { Level = 1, WorldLevel = 6, DailyActivePoint = 100, IsHasTaken = isHasTaken },
            new DailyActivityInfo { Level = 2, WorldLevel = 6, DailyActivePoint = 200, IsHasTaken = isHasTaken },
            new DailyActivityInfo { Level = 3, WorldLevel = 6, DailyActivePoint = 300, IsHasTaken = isHasTaken },
            new DailyActivityInfo { Level = 4, WorldLevel = 6, DailyActivePoint = 400, IsHasTaken = isHasTaken },
            new DailyActivityInfo { Level = 5, WorldLevel = 6, DailyActivePoint = 500, IsHasTaken = isHasTaken }
        ];
    }
}
