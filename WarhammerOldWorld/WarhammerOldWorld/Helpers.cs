using TaleWorlds.Core;

namespace WarhammerOldWorld
{
    public static class Helpers
    {
        public static void Say(string text)
        {
            InformationManager.DisplayMessage(new InformationMessage(text, new TaleWorlds.Library.Color(134, 114, 250)));
        }
    }
}
