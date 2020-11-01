using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace WarhammerOldWorld.QuestManagment
{
    public interface ILoggable
    {
        JournalLog AddDiscreteLogWrapper(TextObject text, TextObject task, int currentProgress, int targetProgress);
        JournalLog AddLogWrapper(TextObject text);
    }
    class TestQuest : QuestBase, ILoggable
        {
            public override bool IsSpecialQuest => true;

            [SaveableField(1)]
            QuestActionStructure structure;
            public TestQuest(Hero questGiver, BasicCharacterObject target, int targetCount) : base("test_quest_1", questGiver, CampaignTime.DaysFromNow(10), 99999999)
            {
                KillQuestAction task1 = new KillQuestAction(target, targetCount);
                structure = new QuestActionStructure(task1, this);
                structure.AddQuestActionOnComplete(task1, new KillQuestAction(target, targetCount));
                structure.Completed().Where((x) => x).Subscribe((x) => CompleteQuestWithSuccess());
                structure.Failed().Where((x) => x).Subscribe((x) => CompleteQuestWithFail());
                SetDialogs();
                AddTrackedObject(questGiver);
            }
            public override TextObject Title => new TextObject("Kill Units");

            public override bool IsRemainingTimeHidden => false;

            protected override void InitializeQuestOnGameLoad()
            {
                SetDialogs();
                structure.Init();
            }

            protected override void SetDialogs()
            {
                // Dialog to discuss the quest?

                OfferDialogFlow = DialogFlow.CreateDialogFlow(QuestManager.QuestOfferToken, 100)
                    .NpcLine(new TextObject("We have run into a problem with certain units"))
                    .NpcLine(new TextObject("Do you want to help us get rid of them"))
                    .BeginPlayerOptions()
                        .PlayerOption(new TextObject("Yes"))
                        .PlayerOption(new TextObject("No")/*, new ConversationSentence.OnMultipleConversationConsequenceDelegate((x) => { StartQuest();return true; })*/)
                    .EndPlayerOptions()
                    .CloseDialog();

                DiscussDialogFlow = DialogFlow.CreateDialogFlow(QuestManager.QuestDiscussToken, 100)
                        .NpcLine(new TextObject("You have more units to kill"))
                        .CloseDialog();



                QuestCharacterDialogFlow = DialogFlow.CreateDialogFlow(QuestManager.CharacterTalkToken)
                    .NpcLine(new TextObject("idk"))
                    .CloseDialog();

                Campaign.Current.ConversationManager.AddDialogFlow(OfferDialogFlow, this);
                Campaign.Current.ConversationManager.AddDialogFlow(DiscussDialogFlow, this);
                Campaign.Current.ConversationManager.AddDialogFlow(QuestCharacterDialogFlow, this);
            }

            protected override void OnCompleteWithSuccess()
            {
                base.OnCompleteWithSuccess();
            }
            public JournalLog AddDiscreteLogWrapper(TextObject text, TextObject task, int currentProgress, int targetProgress) => AddDiscreteLog(text, task, currentProgress, targetProgress, hideInformation: false);

            public JournalLog AddLogWrapper(TextObject text) => AddLog(text);
        }
    
}
