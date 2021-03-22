using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.MountAndBlade;

namespace WarhammerOldWorld.Extensions
{
    public static class AgentExtensions
    {
        public static List<MoraleAgentComponent> getMoraleComponents(this Agent agent)
        {
            List<MoraleAgentComponent> components = new List<MoraleAgentComponent>();

            Type[] types = Assembly.GetExecutingAssembly().GetTypes()
              .Where(t => String.Equals(t.Namespace, "TheOldWorldDev.CustomAgentComponents.MoraleAgentComponents", StringComparison.Ordinal))
              .ToArray();

            components.AddIfNotNull(agent.GetComponent<MoraleAgentComponent>());
            foreach (Type type in types)
            {
                MethodInfo agentGetComponent = typeof(Agent).GetMethod("GetComponent", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);
                agentGetComponent = agentGetComponent.MakeGenericMethod(type);
                MoraleAgentComponent component = agentGetComponent.Invoke(agent, null) as MoraleAgentComponent;
                components.AddIfNotNull(component);
            }

            return components;
        }

        public static void RemoveComponentIfNotNull(this Agent agent, AgentComponent component)
        {
            if(component != null)
            {
                agent.RemoveComponent(component);
            }
        }
    }
}
