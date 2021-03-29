using System.Collections.Generic;
using TaleWorlds.Core;

namespace WarhammerOldWorld.Extensions
{
    public static class BasicCharacterObjectExtension
    {
        public static Dictionary<string, List<string>> TroopNameToAttributeList = new Dictionary<string, List<string>>();

        public static List<string> GetAttributes(this BasicCharacterObject character)
        {
            if (character != null)
            {
                string characterName = character.GetName().ToString();

                List<string> attributeList;
                if (TroopNameToAttributeList.TryGetValue(characterName, out attributeList))
                {
                    return attributeList;
                }
            }
            return new List<string>();
        }
    }
}
