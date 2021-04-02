using HarmonyLib;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using StoryMode.CharacterCreationContent;

namespace OldWorld
{
    [HarmonyPatch(typeof(StoryModeCharacterCreationContent), "OnInitialized")]
    internal class CreationMenuPatch
    {
        private static bool Prefix(CharacterCreation characterCreation, StoryModeCharacterCreationContent __instance)
        {
            CharacterCreationStages.AddParentsMenu(characterCreation, __instance);
            return false;
        }
    }
}
