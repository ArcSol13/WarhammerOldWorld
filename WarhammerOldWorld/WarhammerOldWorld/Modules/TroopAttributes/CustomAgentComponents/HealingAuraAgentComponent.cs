using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using WarhammerOldWorld.Combat;
using WarhammerOldWorld.Utility;

namespace WarhammerOldWorld.Modules.TroopAttributes.CustomAgentComponents
{
    public class HealingAuraAgentComponent : AgentComponent
    {
        private TowTimer healTimer;
        private readonly Mission currentMission = MissionHelper.GetCurrentMission();
        private float secondsBetweenHealProcs = 5;
        private bool healReady = false;
        private float healRadius = 5;
        private float healAmount = 5;


        public HealingAuraAgentComponent(Agent agent) : base(agent)
        {
            healTimer = new TowTimer(GetHealStartTime(), secondsBetweenHealProcs, true);
        }

        protected override void OnTickAsAI(float dt)
        {
            healReady = healTimer.Check(TowCommon.GetMissionTime());
            if (healReady)
            {
                IEnumerable<Agent> nearbyAgents = MissionHelper.GetAgentsAroundPoint(Agent.GetWorldPosition().AsVec2, healRadius);
                foreach (Agent agent in nearbyAgents)
                {
                    float maxHitPoints = agent.Character.MaxHitPoints();
                    if (!agent.Team.IsEnemyOf(Agent.Team) && agent.Health != maxHitPoints)
                    {
                        agent.Heal(healAmount);
                    }
                }
                healTimer.Reset(MBCommon.GetTime(MBCommon.TimeType.Mission));
            }
        }

        /// <summary>
        /// Get a random float between 0 and the healing frequency so agents that have healing auras won't all heal simultaneously.
        /// Otherwise, there's a periodic high CPU load.
        /// </summary>
        /// <returns>float between 0 and the healing frequency</returns>
        private float GetHealStartTime()
        {
            return TowCommon.GetMissionTime() - (float)TowMath.GetRandomDouble(0, secondsBetweenHealProcs);
        }
    }
}
