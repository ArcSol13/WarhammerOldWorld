using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace WarhammerOldWorld.Combat
{
    public static class MissionHelper
    {
        public static Mission GetCurrentMission()
        {
            return Mission.Current;
        }

        public static IEnumerable<Agent> GetAgentsAroundPoint(Vec2 center, float radius)
        {
            return GetCurrentMission().GetNearbyAgents(center, radius);
        }
    }
}
