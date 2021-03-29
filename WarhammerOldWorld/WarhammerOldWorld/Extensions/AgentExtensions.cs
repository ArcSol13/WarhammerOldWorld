using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.MountAndBlade;

namespace WarhammerOldWorld.Extensions
{
    public static class AgentExtensions
    {
        public static Dictionary<string, List<string>> TroopNameToAttributeList = new Dictionary<string, List<string>>();
        public static List<string> GetAttributes(this Agent agent)
        {
            if (agent != null && agent.Character != null)
            {
                string characterName = agent.Character.GetName().ToString();

                List<string> attributeList;
                if (TroopNameToAttributeList.TryGetValue(characterName, out attributeList))
                {
                    return attributeList;
                }
            }
            return new List<string>();
        }

        public static List<MoraleAgentComponent> GetCustomMoraleComponents(this Agent agent)
        {
            List<MoraleAgentComponent> components = new List<MoraleAgentComponent>();
            List<AgentComponent> agentComponents = agent.Components
                .Where(component => component.GetType().IsSubclassOf(typeof(MoraleAgentComponent)) && component.GetType() != typeof(MoraleAgentComponent))
                .ToList();

            agentComponents.ForEach(component => components.AddIfNotNull(component as MoraleAgentComponent));

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
