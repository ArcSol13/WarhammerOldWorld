using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using WarhammerOldWorld.Combat;

namespace WarhammerOldWorld.Modules.TroopAttributes.CustomAgentComponents
{
    public class HealingAuraAgentComponent : AgentComponent
    {
        private Timer healTimer;
        private readonly Mission currentMission = MissionHelper.GetCurrentMission();
        private float secondsBetweenHealProcs = 5;
        private bool healReady = false;
        private float healRadius = 5;
        private float healAmount = 5;


        public HealingAuraAgentComponent(Agent agent) : base(agent)
        {
            healTimer = new Timer(GetHealStartTime(), secondsBetweenHealProcs, true);
        }

        protected override void OnTickAsAI(float dt)
        {
            healReady = healTimer.Check(MBCommon.GetTime(MBCommon.TimeType.Mission));
            if (healReady)
            {
                IEnumerable<Agent> nearbyAgents = currentMission.GetNearbyAgents(Agent.GetWorldPosition().AsVec2, healRadius);
                foreach (Agent agent in nearbyAgents)
                {
                    float maxHitPoints = agent.Character.MaxHitPoints();
                    if (!agent.Team.IsEnemyOf(Agent.Team) && agent.Health != maxHitPoints)
                    {
                        string origHealth = agent.Health.ToString();

                        //Prevent overhealing with Math.Min
                        agent.SyncHealthToClient();
                        agent.Heal(healAmount);
                        //Helpers.Say(Agent.Name + " healed " + agent.Character.Name + " from " + origHealth + " to " + agent.Health);
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
            return MBCommon.GetTime(MBCommon.TimeType.Mission) - (float)Helpers.rng.NextDouble() * secondsBetweenHealProcs;
        }
    }
}
