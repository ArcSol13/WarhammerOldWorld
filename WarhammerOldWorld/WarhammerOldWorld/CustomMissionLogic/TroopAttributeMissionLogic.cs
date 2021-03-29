using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using WarhammerOldWorld.CustomAgentComponents;
using WarhammerOldWorld.CustomAgentComponents.MoraleAgentComponents;
using WarhammerOldWorld.Extensions;

namespace WarhammerOldWorld.CustomMissionLogic
{
    class TroopAttributeMissionLogic : MissionLogic
    {
        public TroopAttributeMissionLogic()
        {
        }

        public override void OnAgentCreated(Agent agent)
        {
            base.OnAgentCreated(agent);

            List<string> attributeList = agent.GetAttributes();
            
            foreach (string attribute in attributeList)
            {
                ApplyAgentComponentsForAttribute(attribute, agent);
            }
        }

        private void ApplyAgentComponentsForAttribute(string attribute, Agent agent)
        {
            switch (attribute)
            {
                case "Unbreakable":
                    agent.AddComponent(new UnbreakableMoraleAgentComponent(agent));
                    break;
                case "Expendable":
                    //Expendable units are handled in the mission's morale interaction logic
                    break;
                case "HealingAura":
                    agent.AddComponent(new HealingAuraAgentComponent(agent));
                    break;
                case "Undead":
                    agent.AddComponent(new UndeadMoraleAgentComponent(agent));
                    break;
            }
        }
    }
}
