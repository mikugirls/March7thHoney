using System.Reflection;
using March7thHoney.GameServer.Game.Mission;
using March7thHoney.GameServer.Game.Mission.FinishAction;
using March7thHoney.GameServer.Game.Mission.FinishType;

namespace March7thHoney.GameServer.Server;

public static class ServerUtils
{
    public static void InitializeHandlers()
    {
        
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<MissionFinishActionAttribute>();
                if (attr != null)
                {
                    var handler = (MissionFinishActionHandler)Activator.CreateInstance(type, null)!;
                    MissionManager.ActionHandlers.Add(attr.FinishAction, handler);
                }

                var attr2 = type.GetCustomAttribute<MissionFinishTypeAttribute>();
                if (attr2 != null)
                {
                    var handler = (MissionFinishTypeHandler)Activator.CreateInstance(type, null)!;
                    MissionManager.FinishTypeHandlers.Add(attr2.FinishType, handler);
                }
            }
        }

    }
}
