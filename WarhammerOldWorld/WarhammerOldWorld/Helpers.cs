using System;
using TaleWorlds.Core;

namespace WarhammerOldWorld
{
    public static class Helpers
    {
        public static Random rng = new Random();
        /// <summary>
        /// Helpers.Say("message") is easier than whatever that is below.
        /// </summary>
        /// <param name="text">The text that you want to print to the console.</param>
        public static void Say(string text)
        {
            InformationManager.DisplayMessage(new InformationMessage(text, new TaleWorlds.Library.Color(134, 114, 250)));
        }
    }
}
