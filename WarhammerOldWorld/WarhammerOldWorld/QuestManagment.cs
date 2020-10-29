using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace WarhammerOldWorld
{
    public class MyCampainQuestBehaviorTypeDefiner : CampaignBehaviorBase.SaveableCampaignBehaviorTypeDefiner
    {
        public MyCampainQuestBehaviorTypeDefiner()
            // This number is the SaveID, supposed to be a unique identifier
            : base(332_001_000)
        {
        }

        protected override void DefineClassTypes()
        {
            // Your quest goes here, second argument is the SaveID
            AddClassDefinition(typeof(TestQuest), 1);
        }
    }
    public abstract class QuestAction
    {
        public IObservable<bool> OnComplete() => onComplete.AsObservable();
        public IObservable<bool> OnFail() => onFail.AsObservable();

        protected QuestAction()
        {
        }
        protected BehaviorSubject<bool> onComplete = new BehaviorSubject<bool>(false);
        protected BehaviorSubject<bool> onFail = new BehaviorSubject<bool>(false);

        public abstract IObservable<int> UpdateScore();

        ~QuestAction()
        {
            onComplete.Dispose();
            onFail.Dispose();
        }
    }
    class KillQuestAction: QuestAction
    {
        Subject<int> killCount = new Subject<int>();
        int kills = 0;
        BasicCharacterObject targetType;

        IDisposable disposable = null;
        public  KillQuestAction(BasicCharacterObject target, int targetNumber) : base()
        {
            targetType = target;
            disposable = ModuleControllerSubModule.Instance.SceneChangeObservable().Subscribe((scene) =>
            {
                Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe((sec) =>
                {
                    if (Agent.Main != null && Mission.Current != null && scene != null)
                    {
                        var enemyAgents = Mission.Current.Agents.Where((x) => x.IsEnemyOf(Agent.Main));
                        foreach (var enemy in enemyAgents)
                            enemy.OnAgentHealthChanged += Main_OnAgentHealthChanged;
                    }
                });
            });
            OnComplete().Subscribe((x) => { if (x) disposable.Dispose(); });
            OnFail().Subscribe((x) =>{ if (x)  disposable.Dispose(); });
            killCount.Subscribe((kills) =>
            {
                if (kills == targetNumber)
                    onComplete.OnNext(true);
            });
        }

        public override IObservable<int> UpdateScore() => killCount.AsObservable();

        private void Main_OnAgentHealthChanged(Agent agent, float oldHealth, float newHealth)
        {
            if (newHealth < 0)
            {
                if (agent.Character == targetType)
                {
                    killCount.OnNext(++kills);
                }

                if (agent != null)
                    agent.OnAgentHealthChanged -= Main_OnAgentHealthChanged;
            }
        }
    }



    internal class TestQuest : QuestBase
    {
        QuestAction action;
        public JournalLog _Task1;
        public TestQuest(Hero questGiver,BasicCharacterObject target,int targetCount) : base("test_quest_1", questGiver, CampaignTime.YearsFromNow(2),99999999)
        {
            action = new KillQuestAction(target, targetCount);

            _Task1 = AddDiscreteLog(new TextObject("Kill "+target.Name.ToString()), new TextObject("Kill " + targetCount +" " +target.Name.ToString()), 0, targetCount);
            action.UpdateScore().Subscribe((x) => {
                _Task1.UpdateCurrentProgress(x);
                if (_Task1.HasBeenCompleted())
                    CompleteQuestWithSuccess();
            });

            SetDialogs();

            AddTrackedObject(questGiver);
            AddLog(new TextObject("I have accepted to kill "+targetCount +" "+target.GetName().ToString()));
        }
        public override TextObject Title => new TextObject("Kill Units");

        public override bool IsRemainingTimeHidden => false;

        protected override void InitializeQuestOnGameLoad()
        {
            SetDialogs();
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
            AddLog(new TextObject("I have managed to kill all of the required units"));
        }
    }
}
