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
        public IObservable<TextObject> logUpdated => logSubject.AsObservable();
        protected IObservable<bool> Active() => isActive.AsObservable();
        protected QuestAction()
        {
        }
        protected BehaviorSubject<bool> onComplete = new BehaviorSubject<bool>(false);
        protected BehaviorSubject<bool> onFail = new BehaviorSubject<bool>(false);

        BehaviorSubject<bool> isActive = new BehaviorSubject<bool>(false);
        protected Subject<TextObject> logSubject = new Subject<TextObject>();
        public void SetActive() => isActive.OnNext(true);
        public abstract IObservable<int> UpdateScore();
        #region logging
        public abstract TextObject TaskName();
        public abstract TextObject TaskDescription();
        public abstract int StartProgress();
        public abstract int Target();
        #endregion
        ~QuestAction()
        {
            logSubject.Dispose();
            onComplete.Dispose();
            onFail.Dispose();
        }
    }
    /// <summary>
    /// Takes care of quests with multiple quest paths/actions 
    /// </summary>
    class QuestActionStructure
    {
        public QuestActionStructure(QuestAction current, ILoggable updateLogs)
        {
            hasActionOnComplete = new Dictionary<QuestAction, bool>();
            hasActionOnFail = new Dictionary<QuestAction, bool>();
            currentAction = current;
            _actionChanged = new BehaviorSubject<QuestAction>(current);
            ActionChanged().Subscribe((action) =>
            {
                #region Structure Logs
                currentAction = action;
                action.SetActive();

                JournalLog currentLog = updateLogs.AddDiscreteLogWrapper(action.TaskName(), action.TaskDescription(), action.StartProgress(), action.Target());
                action.UpdateScore().Subscribe((score) =>
                {
                    if (!currentLog.HasBeenCompleted())
                        currentLog.UpdateCurrentProgress(score > currentLog.Range ? currentLog.Range : score);
                });
                action.logUpdated.Subscribe((log) =>
                {
                    updateLogs.AddLogWrapper(log);
                });
                #endregion
                #region Structure Completion status
                action.OnComplete().Subscribe((x) =>
                {
                    if (x)
                    {
                        if (!hasActionOnComplete.ContainsKey(action))
                            completeSubject.OnNext(true);
                    }
                });

                action.OnFail().Subscribe((x) =>
                {
                    if (x)
                    {
                        if (!hasActionOnFail.ContainsKey(action))
                            failSubject.OnNext(true);
                    }
                });
                #endregion
            });
        }

        QuestAction currentAction;
        //Checks whether an action has a follow up task
        Dictionary<QuestAction, bool> hasActionOnComplete;
        Dictionary<QuestAction, bool> hasActionOnFail;

        //Adds a task on completion of another task
        public void AddQuestActionOnComplete(QuestAction onAction, QuestAction addingAction)
        {
            hasActionOnComplete.Add(onAction, true);
            onAction.OnComplete().Subscribe((x) =>
            {
                if (x)
                {
                    currentAction = addingAction;
                    _actionChanged.OnNext(currentAction);
                }
            });
        }
        //Adds a task when a task is failed
        public void AddQuestActionOnFail(QuestAction onAction, QuestAction addingAction)
        {
            hasActionOnFail.Add(onAction, true);
            onAction.OnFail().Subscribe((x) =>
            {
                if (x)
                {
                    currentAction = addingAction;
                    _actionChanged.OnNext(currentAction);
                }
            });
        }

        BehaviorSubject<QuestAction> _actionChanged;
        public IObservable<QuestAction> ActionChanged() => _actionChanged;

        Subject<bool> completeSubject = new Subject<bool>();
        Subject<bool> failSubject = new Subject<bool>();
        //Returns true when the whole structure is done
        public IObservable<bool> Completed() => completeSubject.AsObservable();
        //Returns true when you failed the quest
        public IObservable<bool> Failed() => failSubject.AsObservable();

    }


    class KillQuestAction : QuestAction
    {
        ~KillQuestAction() => killCount.Dispose();

        Subject<int> killCount = new Subject<int>();
        int killsNeeded;
        int kills = 0;
        BasicCharacterObject targetType;

        IDisposable disposable = null;
        public KillQuestAction(BasicCharacterObject target, int targetNumber) : base()
        {
            targetType = target;
            killsNeeded = targetNumber;
            IDisposable activeDisposable = null;
            activeDisposable =
            Active().Where((x) => x).Subscribe((active) =>
            {
                  activeDisposable.Dispose();
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
                  OnFail().Subscribe((x) => { if (x) disposable.Dispose(); });
                  killCount.Subscribe((kills) =>
                  {
                      if (kills >= targetNumber)
                      {
                          onComplete.OnNext(true);
                          killCount.Dispose();
                      }
                  });

            });

        }

        public override IObservable<int> UpdateScore() => killCount.AsObservable();

        private void Main_OnAgentHealthChanged(Agent agent, float oldHealth, float newHealth)
        {
            if(killCount.IsDisposed)
            {
                agent.OnAgentHealthChanged -= Main_OnAgentHealthChanged;
                return;

            }

            if (newHealth < 1)
            {
                if (agent.Character == targetType)
                {
                    killCount.OnNext(++kills);
                }

                if (agent != null)
                    agent.OnAgentHealthChanged -= Main_OnAgentHealthChanged;
            }
        }

        public override TextObject TaskName() => new TextObject("Kill Units");

        public override TextObject TaskDescription() => new TextObject("Kill " + targetType.GetName().ToString());

        public override int StartProgress() => 0;
        public override int Target() => killsNeeded;
    }

    // Dumb solution to access AddDiscreteLog from QuestBase outside the class
    public interface ILoggable
    {
        JournalLog AddDiscreteLogWrapper(TextObject text, TextObject task, int currentProgress, int targetProgress);
        JournalLog AddLogWrapper(TextObject text);
    }

    internal class TestQuest : QuestBase, ILoggable
    {
        QuestActionStructure structure;
        public TestQuest(Hero questGiver, BasicCharacterObject target, int targetCount) : base("test_quest_1", questGiver, CampaignTime.YearsFromNow(2), 99999999)
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
