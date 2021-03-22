using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace WarhammerOldWorld.CustomAgentComponents
{
    public class HealingAuraAgentComponent : AgentComponent
    {
        private Timer healTimer;
        private float secondsBetweenHealProcs = 1;
        private bool canHeal = false;
        private float healRadius = 5;
        private float healAmount = 1;

        private Mission currentMission;
        public HealingAuraAgentComponent(Agent agent, Mission mission) : base(agent)
        {
            currentMission = mission;
            float time = MBCommon.GetTime(MBCommon.TimeType.Mission);
            this.healTimer = new Timer(time, secondsBetweenHealProcs, true);
        }

        protected override void OnTickAsAI(float dt)
        {
            canHeal = healTimer.Check(MBCommon.GetTime(MBCommon.TimeType.Mission));
            if (canHeal)
            {
                IEnumerable<Agent> nearbyAgents = currentMission.GetNearbyAgents(Agent.GetWorldPosition().AsVec2, healRadius);
                foreach (Agent agent in nearbyAgents)
                {
                    if(!agent.Team.IsEnemyOf(Agent.Team))
                    {
                        float maxHitPoints = agent.Character.MaxHitPoints();
                        agent.Health = Math.Min(agent.Health + healAmount, maxHitPoints);
                        Helpers.Say(Agent.Name + " healed " + agent.Character.Name + " from " + (agent.Health - 1) + " to " + agent.Health);
                    }
                }
                healTimer.Reset(MBCommon.GetTime(MBCommon.TimeType.Mission));
            }
            
        }
    }
}
