using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using StoryMode.CharacterCreationContent;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;
using System;

namespace OldWorld
{
    class CharacterCreationStages
    {
        private static StoryModeCharacterCreationContent cc;

        public static void AddParentsMenu(CharacterCreation characterCreation, StoryModeCharacterCreationContent storyModeCharacterCreation) {
            cc = storyModeCharacterCreation;

            CharacterCreationMenu ParentsMenu = new CharacterCreationMenu(new TextObject("{=b4lDDcli}Family"), new TextObject("Your parent was a"), new CharacterCreationOnInit(ParentsOnInit), (CharacterCreationMenu.MenuTypes) 0);

            CharacterCreationCategory ParentsMenuCreationCategory = ParentsMenu.AddMenuCategory();

            List<SkillObject> Skills0 = new List<SkillObject>();
            Skills0.Add(DefaultSkills.Steward);
            Skills0.Add(DefaultSkills.Leadership);
            TextObject text0 = new TextObject("Captain of the Empire");
            CharacterCreationOnCondition optionCondition0 = new CharacterCreationOnCondition(EmpireCondition);
            CharacterCreationOnSelect onSelect0 = new CharacterCreationOnSelect(ParentCaptainOnConsequence);
            CharacterCreationApplyFinalEffects onApply0 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText0 = new TextObject("Your father served his Elector diligently and lived long enough to become a veteran, a rare sight within the Empire. Proudly wearing the scars earned from a life of fighting beastmen, he commanded respect and was known for his martial skill");
            ParentsMenuCreationCategory.AddCategoryOption(text0, Skills0, CharacterAttributesEnum.Vigor, 1, 15, 1, optionCondition0, onSelect0, onApply0, descriptionText0);

            List<SkillObject> Skills1 = new List<SkillObject>();
            Skills1.Add(DefaultSkills.Trade);
            Skills1.Add(DefaultSkills.Charm);
            TextObject text1 = new TextObject("Merchant");
            CharacterCreationOnCondition optionCondition1 = new CharacterCreationOnCondition(EmpireCondition);
            CharacterCreationOnSelect onSelect1 = new CharacterCreationOnSelect(ParentMerchantOnConsequence);
            CharacterCreationApplyFinalEffects onApply1 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText1 = new TextObject("Hailing from a long line of merchants, your family made its wealth peddling the various crafts along the River Stir. Ranald did not look upon your parents with kindness however and they could only just make ends meet.");
            ParentsMenuCreationCategory.AddCategoryOption(text1, Skills1, CharacterAttributesEnum.Social, 1, 15, 1, optionCondition1, onSelect1, onApply1, descriptionText1);

            List<SkillObject> Skills2 = new List<SkillObject>();
            Skills2.Add(DefaultSkills.Engineering);
            Skills2.Add(DefaultSkills.OneHanded);
            TextObject text2 = new TextObject("Imperial Engineer");
            CharacterCreationOnCondition optionCondition2 = new CharacterCreationOnCondition(EmpireCondition);
            CharacterCreationOnSelect onSelect2 = new CharacterCreationOnSelect(ParentEngineerOnConsequence);
            CharacterCreationApplyFinalEffects onApply2 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText2 = new TextObject("Your father was a tinkerer and spoke of little else but his schemes and ideas, the Colleges of Nuln had given him a stable life and your upbringing involved playing with things that perhaps, no child should be touching.");
            ParentsMenuCreationCategory.AddCategoryOption(text2, Skills2, CharacterAttributesEnum.Intelligence, 1, 15, 1, optionCondition2, onSelect2, onApply2, descriptionText2);

            List<SkillObject> Skills3 = new List<SkillObject>();
            Skills3.Add(DefaultSkills.Crafting);
            Skills3.Add(DefaultSkills.TwoHanded);
            TextObject text3 = new TextObject("Blacksmith");
            CharacterCreationOnCondition optionCondition3 = new CharacterCreationOnCondition(EmpireCondition);
            CharacterCreationOnSelect onSelect3 = new CharacterCreationOnSelect(ParentBlacksmithOnConsequence);
            CharacterCreationApplyFinalEffects onApply3 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText3 = new TextObject("The Empire is never short of enemies and as such, could never be short of weapons. Your father crafted blades and armour for State Troops and militia alike, proudly claiming that his blades would \"cut down any beast!\"");
            ParentsMenuCreationCategory.AddCategoryOption(text3, Skills3, CharacterAttributesEnum.Endurance, 1, 15, 1, optionCondition3, onSelect3, onApply3, descriptionText3);

            List<SkillObject> Skills4 = new List<SkillObject>();
            Skills4.Add(DefaultSkills.Scouting);
            Skills4.Add(DefaultSkills.Bow);
            TextObject text4 = new TextObject("Hunter");
            CharacterCreationOnCondition optionCondition4 = new CharacterCreationOnCondition(EmpireCondition);
            CharacterCreationOnSelect onSelect4 = new CharacterCreationOnSelect(ParentHunterOnConsequence);
            CharacterCreationApplyFinalEffects onApply4 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText4 = new TextObject("A simple man, your family was sustained by your fathers trips deep into the forest and his skill with a bow. It was a meagre living but you had a roof over your head and at times food in your belly.");
            ParentsMenuCreationCategory.AddCategoryOption(text4, Skills4, CharacterAttributesEnum.Control, 1, 15, 1, optionCondition4, onSelect4, onApply4, descriptionText4);

            List<SkillObject> Skills5 = new List<SkillObject>();
            Skills5.Add(DefaultSkills.Roguery);
            Skills5.Add(DefaultSkills.Throwing);
            TextObject text5 = new TextObject("Thief");
            CharacterCreationOnCondition optionCondition5 = new CharacterCreationOnCondition(EmpireCondition);
            CharacterCreationOnSelect onSelect5 = new CharacterCreationOnSelect(ParentThiefOnConsequence);
            CharacterCreationApplyFinalEffects onApply5 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText5 = new TextObject("There is no shortage of thieves and brigands in the Empire, your father amongst them. While he would claim that none were ever hurt as he pilfered the houses of wealthy merchants, only he knew the truth to that claim");
            ParentsMenuCreationCategory.AddCategoryOption(text5, Skills5, CharacterAttributesEnum.Cunning, 1, 15, 1, optionCondition5, onSelect5, onApply5, descriptionText5);

            List<SkillObject> Skills6 = new List<SkillObject>();
            Skills6.Add(DefaultSkills.Riding);
            Skills6.Add(DefaultSkills.Polearm);
            TextObject text6 = new TextObject("Vampiric Noble");
            CharacterCreationOnCondition optionCondition6 = new CharacterCreationOnCondition(VCCondition);
            CharacterCreationOnSelect onSelect6 = new CharacterCreationOnSelect(ParentVampiricNobleOnConsequence);
            CharacterCreationApplyFinalEffects onApply6 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText6 = new TextObject("-");
            ParentsMenuCreationCategory.AddCategoryOption(text6, Skills6, CharacterAttributesEnum.Social, 1, 20, 1, optionCondition6, onSelect6, onApply6, descriptionText6);

            List<SkillObject> Skills7 = new List<SkillObject>();
            Skills7.Add(DefaultSkills.OneHanded);
            //necormancy
            TextObject text7 = new TextObject("Weak Necromancer");
            CharacterCreationOnCondition optionCondition7 = new CharacterCreationOnCondition(VCCondition);
            CharacterCreationOnSelect onSelect7 = new CharacterCreationOnSelect(ParentNecromancerOnConsequence);
            CharacterCreationApplyFinalEffects onApply7 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText7 = new TextObject("Little could be said for your fathers sanity, his days were dedicated to dissecting scrolls and performing dark rituals. Obsessed with becoming powerful yet unable to master even the basics, his failures drove him to madness and he wandered deep into the swamps. Vanishing.");
            ParentsMenuCreationCategory.AddCategoryOption(text7, Skills7, CharacterAttributesEnum.Cunning, 1, 15, 1, optionCondition7, onSelect7, onApply7, descriptionText7);

            List<SkillObject> Skills8 = new List<SkillObject>();
            Skills8.Add(DefaultSkills.Trade);
            Skills8.Add(DefaultSkills.Charm);
            TextObject text8 = new TextObject("Stirland Merchant");
            CharacterCreationOnCondition optionCondition8 = new CharacterCreationOnCondition(VCCondition);
            CharacterCreationOnSelect onSelect8 = new CharacterCreationOnSelect(ParentStirlandMerchantOnConsequence);
            CharacterCreationApplyFinalEffects onApply8 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText8 = new TextObject("Very few risk travel into Sylvania, let alone with trade goods in tow. Your family were either incredibly brave, or incredibly desperate, to travel the dark roads plagued with bandits, the undead and worse..");
            ParentsMenuCreationCategory.AddCategoryOption(text8, Skills8, CharacterAttributesEnum.Intelligence, 1, 15, 1, optionCondition8, onSelect8, onApply8, descriptionText8);

            List<SkillObject> Skills9 = new List<SkillObject>();
            Skills9.Add(DefaultSkills.Charm);
            Skills9.Add(DefaultSkills.Steward);
            TextObject text9 = new TextObject("Priest of Morr");
            CharacterCreationOnCondition optionCondition9 = new CharacterCreationOnCondition(VCCondition);
            CharacterCreationOnSelect onSelect9 = new CharacterCreationOnSelect(ParentPriestofMorrOnConsequence);
            CharacterCreationApplyFinalEffects onApply9 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText9 = new TextObject("The dead do not rest easy in Sylvania and the work of Morr is never done, your father was a quiet man whose life was a dedication to death. Maintaining a garden of Morr and performing the necessary rights to ensure the dead would rest, sometimes having to do so more than once.");
            ParentsMenuCreationCategory.AddCategoryOption(text9, Skills9, CharacterAttributesEnum.Social, 1, 15, 1, optionCondition9, onSelect9, onApply9, descriptionText9);

            List<SkillObject> Skills10 = new List<SkillObject>();
            Skills10.Add(DefaultSkills.Scouting);
            Skills10.Add(DefaultSkills.TwoHanded);
            TextObject text10 = new TextObject("Woodsman");
            CharacterCreationOnCondition optionCondition10 = new CharacterCreationOnCondition(VCCondition);
            CharacterCreationOnSelect onSelect10 = new CharacterCreationOnSelect(ParentWoodsmanOnConsequence);
            CharacterCreationApplyFinalEffects onApply10 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText10 = new TextObject("-");
            ParentsMenuCreationCategory.AddCategoryOption(text10, Skills10, CharacterAttributesEnum.Endurance, 1, 15, 1, optionCondition10, onSelect10, onApply10, descriptionText10);

            List<SkillObject> Skills11 = new List<SkillObject>();
            Skills10.Add(DefaultSkills.Throwing);
            Skills10.Add(DefaultSkills.Trade);
            TextObject text11 = new TextObject("Fisherman");
            CharacterCreationOnCondition optionCondition11 = new CharacterCreationOnCondition(VCCondition);
            CharacterCreationOnSelect onSelect11 = new CharacterCreationOnSelect(ParentFishermanOnConsequence);
            CharacterCreationApplyFinalEffects onApply11 = new CharacterCreationApplyFinalEffects(NoEffect);
            TextObject descriptionText11 = new TextObject("-");
            ParentsMenuCreationCategory.AddCategoryOption(text11, Skills11, CharacterAttributesEnum.Endurance, 1, 15, 1, optionCondition11, onSelect11, onApply11, descriptionText11);

            characterCreation.AddNewMenu(ParentsMenu);
        }

        private static void ParentFishermanOnConsequence(CharacterCreation charInfo)
        {
        }

        private static void ParentWoodsmanOnConsequence(CharacterCreation charInfo)
        {
        }

        private static void ParentPriestofMorrOnConsequence(CharacterCreation charInfo)
        {
        }

        private static void ParentStirlandMerchantOnConsequence(CharacterCreation charInfo)
        {
        }

        private static void ParentNecromancerOnConsequence(CharacterCreation charInfo)
        {
        }

        private static void ParentVampiricNobleOnConsequence(CharacterCreation charInfo)
        {
        }

        private static bool VCCondition()
        {
            return true;
        }

        private static void ParentThiefOnConsequence(CharacterCreation charInfo)
        {
        }

        private static void ParentHunterOnConsequence(CharacterCreation charInfo)
        {
        }

        private static void ParentBlacksmithOnConsequence(CharacterCreation charInfo)
        {
        }

        private static void ParentEngineerOnConsequence(CharacterCreation charInfo)
        {
        }

        private static void ParentMerchantOnConsequence(CharacterCreation charInfo)
        {
        }

        private static void ParentCaptainOnConsequence(CharacterCreation charInfo)
        {
        }

        private static bool EmpireCondition()
        {
            return true;
        }

        private static void NoEffect(CharacterCreation charInfo)
        {
        }

        private static void ParentsOnInit(CharacterCreation characterCreation)
        {

            characterCreation.ClearFaceGenMounts();
            characterCreation.ClearFaceGenPrefab();
            FaceGenChar mother;
            FaceGenChar father;

            BodyProperties player = CharacterObject.PlayerCharacter.GetBodyProperties(CharacterObject.PlayerCharacter.Equipment, -1);
            BodyProperties motherBodyProperties = player;
            BodyProperties fatherBodyProperties = player;
            FaceGen.GenerateParentKey(player, ref motherBodyProperties, ref fatherBodyProperties);
            motherBodyProperties = new BodyProperties(new DynamicBodyProperties(33f, 0.3f, 0.2f), motherBodyProperties.StaticProperties);
            fatherBodyProperties = new BodyProperties(new DynamicBodyProperties(33f, 0.5f, 0.5f), fatherBodyProperties.StaticProperties);
            CharacterObject characterObject1 = Game.Current.ObjectManager.GetObject<CharacterObject>("village_woman_empire");
            CharacterObject characterObject2 = Game.Current.ObjectManager.GetObject<CharacterObject>("townsman_empire");
            mother = new FaceGenChar(motherBodyProperties, characterObject1.Equipment, true, "anim_mother_1");
            father = new FaceGenChar(fatherBodyProperties, characterObject2.Equipment, false, "anim_father_1");

            CharacterCreation characterCreation1 = characterCreation;
            List<FaceGenChar> faceGenCharList = new List<FaceGenChar>();
            faceGenCharList.Add(mother);
            faceGenCharList.Add(father);
            characterCreation1.ChangeFaceGenChars(faceGenCharList);
            ChangeParentsOutfit(characterCreation);
            ChangeParentsAnimation(characterCreation);
        }

        private static void ChangeParentsAnimation(CharacterCreation characterCreation)
        {
        }

        private static void ChangeParentsOutfit(CharacterCreation characterCreation)
        {
        }
    }
}
