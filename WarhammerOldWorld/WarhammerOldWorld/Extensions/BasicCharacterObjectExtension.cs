using System.Collections.Generic;
using TaleWorlds.Core;

namespace WarhammerOldWorld.Extensions
{
    public static class BasicCharacterObjectExtension
    {
        public static Dictionary<string, List<string>> troopNameToAttributeList = new Dictionary<string, List<string>>();

        public static List<string> getAttributes(this BasicCharacterObject character)
        {
            if (character != null)
            {
                string characterName = character.GetName().ToString();

                List<string> attributeList;
                if (troopNameToAttributeList.TryGetValue(characterName, out attributeList))
                {
                    return attributeList;
                }
            }
            return new List<string>();
        }
    }
}
