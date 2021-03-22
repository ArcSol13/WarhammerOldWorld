using TaleWorlds.MountAndBlade;

namespace WarhammerOldWorld.CustomAgentComponents
{
    public class FrenzyAgentComponent : AgentComponent
    {
        private float originalSwingSpeed;
        //private AgentStatCalculateModel model;

        public FrenzyAgentComponent(Agent agent) : base(agent)
        {
        }

        protected override void OnTickAsAI(float dt)
        {
            if(originalSwingSpeed == 0)
            {
                originalSwingSpeed = Agent.AgentDrivenProperties.SwingSpeedMultiplier;
            }
            if(Agent.Health > 50)
            {
                Agent.AgentDrivenProperties.SwingSpeedMultiplier = originalSwingSpeed * 100;
            }
            else
            {
                Agent.AgentDrivenProperties.SwingSpeedMultiplier = originalSwingSpeed;
            }
        }

        protected override void OnHit(Agent affectorAgent, int damage, in MissionWeapon affectorWeapon)
        {
            if (!Agent.IsAIControlled)
            {
                Agent.Health = 10000000;
                Agent.AgentDrivenProperties.SwingSpeedMultiplier = 500;
                Agent.AgentDrivenProperties.CombatMaxSpeedMultiplier = 500;
                Agent.AgentDrivenProperties.MaxSpeedMultiplier = 500;
                Agent.AgentDrivenProperties.TopSpeedReachDuration = 0.01f;
                MissionGameModels.Current.AgentStatCalculateModel.UpdateAgentStats(Agent, Agent.AgentDrivenProperties);
            }
        }
    }
}
