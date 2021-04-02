using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade;

namespace WarhammerOldWorld.Combat
{
    /// <summary>
    /// Extension methods for modifying an agent during combat.
    /// </summary>
    public static class AgentCombatExtensions
    {
        /// <summary>
        /// Apply damage to an agent. 
        /// </summary>
        /// <param name="agent">The agent that will be damaged</param>
        /// <param name="damageAmount">How much damage the agent will receive.</param>
        public static void ApplyDamage(this Agent agent, float damageAmount)
        {
            //Prevent reduction below 0 health (possibly unnecessary)
            agent.Health = Math.Max(agent.Health - damageAmount, 0);
        }

        /// <summary>
        /// Apply healing to an agent.
        /// </summary>
        /// <param name="agent">The agent that will be healed</param>
        /// <param name="healingAmount">How much healing the agent will receive</param>
        public static void Heal(this Agent agent, float healingAmount)
        {
            //Cap healing at the agent's max hit points
            agent.Health = Math.Min(agent.Health + healingAmount, agent.HealthLimit);
        }
    }
}
