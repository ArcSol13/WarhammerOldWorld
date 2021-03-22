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
            if(agent.Character != null)
            {
                BasicCharacterObject character = agent.Character;

                List<string> attributeList = character.getAttributes();
                
                foreach (string attribute in attributeList)
                {
                    applyAgentComponentsForAttribute(attribute, agent);
                }

                if (agent.Character.Culture != null)
                {
                    if (agent.Character.Culture.StringId.Equals("vampire"))
                    {
                        giveVampireComponentsToAgent(agent);
                    }
                }
            }
            
        }

        private void giveVampireComponentsToAgent(Agent agent)
        {
            // Remove default retreat component
            AgentComponent defaultRetreatComponent = agent.GetComponent<RetreatAgentComponent>();
            agent.RemoveComponentIfNotNull(defaultRetreatComponent);

            // Replace default morale component with vamp component
            AgentComponent moraleComponent = agent.GetComponent<MoraleAgentComponent>();
            agent.RemoveComponentIfNotNull(moraleComponent);
            agent.AddComponent(new VampireMoraleAgentComponent(agent));
        }

        private void applyAgentComponentsForAttribute(string attribute, Agent agent)
        {
            switch (attribute)
            {
                case "Unbreakable":
                    // Unbreakable agents shouldn't be subject to default retreat/morale logic
                    agent.RemoveComponentIfNotNull(agent.GetComponent<MoraleAgentComponent>());

                    agent.AddComponent(new UnbreakableMoraleAgentComponent(agent));
                    break;
                case "Expendable":
                    //components.Add(new ExpendableMoraleAgentComponent(agent));
                    //components.Add(new ExpendableRetreatAgentComponent(agent));
                    break;
                case "HealingAura":
                    agent.AddComponent(new HealingAuraAgentComponent(agent, base.Mission));
                    break;
                case "Frenzy":
                    agent.AddComponent(new FrenzyAgentComponent(agent));
                    break;
            }
        }
    }
}
