using March7thHoney.Util;

namespace March7thHoney.Command;

public interface ICommandSender
{
    public ValueTask SendMsg(string msg);

    public bool HasPermission(string permission);

    public int GetSender();
}

public class ConsoleCommandSender(Logger logger) : ICommandSender
{
    public async ValueTask SendMsg(string msg)
    {
        logger.Info(msg);
        await Task.CompletedTask;
    }

    public bool HasPermission(string permission)
    {
        return true;
    }

    public int GetSender()
    {
        return 0;
    }
}