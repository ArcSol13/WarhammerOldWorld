using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace WarhammerOldWorld.Utility
{
    public static class TowCommon
    {
        /// <summary>
        /// Helpers.Say("message") is easier than whatever that is below.
        /// </summary>
        /// <param name="text">The text that you want to print to the console.</param>
        public static void Say(string text)
        {
            InformationManager.DisplayMessage(new InformationMessage(text, new TaleWorlds.Library.Color(134, 114, 250)));
        }

        /// <summary>
        /// Get the time elapsed in the current mission.
        /// </summary>
        /// <returns>Mission time as a float</returns>
        public static float GetMissionTime()
        {
            return MBCommon.GetTime(MBCommon.TimeType.Mission);
        }
    }
}
