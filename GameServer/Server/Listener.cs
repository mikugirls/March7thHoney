using March7thHoney.Kcp;

namespace March7thHoney.GameServer.Server;

public class Listener : March7thHoneyListener
{
    public static Connection? GetActiveConnection(int uid)
    {
        var con = Connections.Values.FirstOrDefault(c =>
            (c as Connection)?.Player?.Uid == uid && c.State == SessionStateEnum.ACTIVE) as Connection;
        return con;
    }
}
