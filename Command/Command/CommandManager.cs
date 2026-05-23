using System.Reflection;
using March7thHoney.GameServer.Server;
using March7thHoney.Internationalization;
using March7thHoney.Kcp;
using March7thHoney.Util;
using Spectre.Console;
using static March7thHoney.GameServer.Plugin.Event.PluginEvent;

namespace March7thHoney.Command.Command;

public class CommandManager
{
    private const int MaxCommandHistory = 100;

    private readonly List<string> _commandHistory = [];
    private int _historyIndex = -1;
    public static CommandManager? Instance { get; private set; }
    public Dictionary<string, ICommand> Commands { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, CommandInfoAttribute> CommandInfo { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> CommandAlias { get; } = new(StringComparer.OrdinalIgnoreCase); 
    public Logger Logger { get; } = new("CommandManager");
    public Connection? Target { get; set; }

    public void RegisterCommands()
    {
        Instance = this;
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            if (typeof(ICommand).IsAssignableFrom(type) && !type.IsAbstract)
                RegisterCommand(type);

        Logger.Info(I18NManager.Translate("Server.ServerInfo.RegisterItem", Commands.Count.ToString(),
            I18NManager.Translate("Word.Command")));
    }

    public void RegisterCommand(Type type)
    {
        var attr = type.GetCustomAttribute<CommandInfoAttribute>();
        if (attr == null) return;
        var instance = Activator.CreateInstance(type);
        if (instance is not ICommand command) return;
        Commands.Add(attr.Name, command);
        CommandInfo.Add(attr.Name, attr);

        
        foreach (var alias in attr.Alias) 
            CommandAlias.Add(alias, attr.Name);
    }

    public void Start()
    {
        while (true)
            try
            {
                var input = ReadCommand();

                if (string.IsNullOrEmpty(input)) continue;

                if (input.StartsWith("/")) input = input[1..];

                if (_commandHistory.Count >= MaxCommandHistory) _commandHistory.RemoveAt(0);

                if (_commandHistory.Count == 0 || _commandHistory.Last() != input) _commandHistory.Add(input);
                _historyIndex = _commandHistory.Count;
                HandleCommand(input, new ConsoleCommandSender(Logger));
            }
            catch
            {
                Logger.Error(I18NManager.Translate("Game.Command.Notice.InternalError"));
            }
        
    }

    private string ReadCommand()
    {
        var input = new List<char>();

        AnsiConsole.Markup("[yellow]> [/]");
        while (true)
        {
            var keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }

            switch (keyInfo.Key)
            {
                case ConsoleKey.Backspace:
                {
                    if (input.Count > 0)
                    {
                        input.RemoveAt(input.Count - 1);
                        Console.Write("\b \b");
                    }

                    break;
                }
                case ConsoleKey.UpArrow:
                {
                    if (_historyIndex > 0)
                    {
                        _historyIndex--;
                        ReplaceInput(input, _commandHistory[_historyIndex]);
                    }

                    break;
                }
                case ConsoleKey.DownArrow when _historyIndex < _commandHistory.Count - 1:
                    _historyIndex++;
                    ReplaceInput(input, _commandHistory[_historyIndex]);
                    break;
                case ConsoleKey.DownArrow:
                {
                    if (_historyIndex == _commandHistory.Count - 1)
                    {
                        _historyIndex++;
                        ReplaceInput(input, string.Empty);
                    }

                    break;
                }
                
                default:
                    input.Add(keyInfo.KeyChar);
                    Console.Write(keyInfo.KeyChar);
                    break;
            }
        }

        return new string(input.ToArray());
    }

    private static void ReplaceInput(List<char> input, string newText)
    {
        while (input.Count > 0)
        {
            input.RemoveAt(input.Count - 1);
            Console.Write("\b \b");
        }

        input.AddRange(newText.ToCharArray());
        Console.Write(newText);
    }

    public void HandleCommand(string input, ICommandSender sender)
    {
        try
        {
            var split = CommandArg.Tokenize(input);
            if (split.Count == 0) return;

            var cmd = split[0].TrimStart('/');

            var tempTarget = Target;
            var isTargetExplicit = sender is ConsoleCommandSender && tempTarget != null;
            if (sender is ConsoleCommandSender)
            {
                if (IsTargetToken(cmd))
                {
                    var target = cmd[1..];
                    if (March7thHoneyListener.Connections.Values.ToList()
                            .Find(item => (item as Connection)?.Player?.Uid.ToString() == target) is Connection con)
                    {
                        if (split.Count == 1)
                        {
                            Target = con;
                            sender.SendMsg(I18NManager.Translate("Game.Command.Notice.TargetFound", target,
                                con.Player!.Data.Name!));
                            return;
                        }

                        tempTarget = con;
                        isTargetExplicit = true;
                        split.RemoveAt(0);
                        cmd = split[0].TrimStart('/');
                    }
                    else
                    {
                        
                        sender.SendMsg(I18NManager.Translate("Game.Command.Notice.TargetNotFound", target));
                        return;
                    }
                }
            }
            else
            {
                InvokeOnPlayerUseCommand(sender, input);
                
                tempTarget = Listener.GetActiveConnection(sender.GetSender());
                if (tempTarget == null)
                {
                    sender.SendMsg(I18NManager.Translate("Game.Command.Notice.TargetNotFound",
                        sender.GetSender().ToString()));
                    return;
                }
            }

            if (tempTarget is { IsOnline: false })
            {
                sender.SendMsg(I18NManager.Translate("Game.Command.Notice.TargetOffline",
                    tempTarget.Player!.Uid.ToString(), tempTarget.Player!.Data.Name!));
                tempTarget = null;
            }

            
            if (CommandAlias.TryGetValue(cmd, out var realCmd)) cmd = realCmd;

            if (Commands.TryGetValue(cmd, out var command))
            {
                split.RemoveAt(0);
                for (var i = split.Count - 1; i >= 0; i--)
                {
                    if (!IsTargetToken(split[i])) continue;
                    var target = split[i][1..];
                    if (March7thHoneyListener.Connections.Values.ToList()
                            .Find(item => (item as Connection)?.Player?.Uid.ToString() == target) is Connection con)
                    {
                        tempTarget = con;
                        isTargetExplicit = true;
                        split.RemoveAt(i);
                        continue;
                    }

                    sender.SendMsg(I18NManager.Translate("Game.Command.Notice.TargetNotFound", target));
                    return;
                }

                var arg = new CommandArg(split, split.JoinFormat(" ", ""), sender, tempTarget, isTargetExplicit);

                
                if (arg.Target?.Player?.Uid != sender.GetSender() && !sender.HasPermission(CommandPermissions.TargetOthers))
                {
                    sender.SendMsg(I18NManager.Translate("Game.Command.Notice.NoPermission"));
                    return;
                }

                
                var isFound = false;
                var info = CommandInfo[cmd];

                if (!sender.HasPermission(info.Permission))
                {
                    sender.SendMsg(I18NManager.Translate("Game.Command.Notice.NoPermission"));
                    return;
                }

                foreach (var method in command.GetType().GetMethods())
                {
                    var attr = method.GetCustomAttribute<CommandMethodAttribute>();
                    if (attr == null) continue;
                    var canRun = true;
                    foreach (var condition in attr.Conditions)
                    {
                        if (split.Count <= condition.Index)
                        {
                            canRun = false;
                            break;
                        }

                        if (split[condition.Index].Equals(condition.ShouldBe, StringComparison.OrdinalIgnoreCase))
                            continue;
                        canRun = false;
                        break;
                    }

                    if (!canRun) continue;
                    isFound = true;
                    InvokeCommandMethod(method, command, arg, sender, cmd);
                    break;
                }

                if (isFound) return;
                
                foreach (var method in command.GetType().GetMethods())
                {
                    var attr = method.GetCustomAttribute<CommandDefaultAttribute>();
                    if (attr == null) continue;
                    isFound = true;
                    InvokeCommandMethod(method, command, arg, sender, cmd);
                    break;
                }

                if (isFound) return;
                sender.SendMsg(I18NManager.Translate(info.Usage));
            }
            else
            {
                sender.SendMsg(I18NManager.Translate("Game.Command.Notice.CommandNotFound"));
            }
        }
        catch (Exception e)
        {
            Logger.Error(I18NManager.Translate("Game.Command.Notice.InternalError"), e);
            sender.SendMsg(I18NManager.Translate("Game.Command.Notice.InternalError"));
        }
    }

    private static bool IsTargetToken(string token)
    {
        return token.Length > 1 && token[0] == '@' && int.TryParse(token[1..], out _);
    }

    private void InvokeCommandMethod(MethodInfo method, ICommand command, CommandArg arg, ICommandSender sender,
        string commandName)
    {
        var result = method.Invoke(command, [arg]);
        ObserveCommandResult(result, sender, commandName, method.Name);
    }

    private void ObserveCommandResult(object? result, ICommandSender sender, string commandName, string methodName)
    {
        switch (result)
        {
            case Task task:
                _ = ObserveCommandTask(task, sender, commandName, methodName);
                break;
            case ValueTask valueTask:
                _ = ObserveCommandTask(valueTask.AsTask(), sender, commandName, methodName);
                break;
        }
    }

    private async Task ObserveCommandTask(Task task, ICommandSender sender, string commandName, string methodName)
    {
        try
        {
            await task;
        }
        catch (Exception e)
        {
            Logger.Error($"Command {commandName}.{methodName} failed", e);
            try
            {
                await sender.SendMsg(I18NManager.Translate("Game.Command.Notice.InternalError"));
            }
            catch (Exception sendException)
            {
                Logger.Warn($"Failed to send command error message for {commandName}.{methodName}", sendException);
            }
        }
    }
}
