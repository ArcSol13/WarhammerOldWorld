using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.SandBox;
using TaleWorlds.SaveSystem;

namespace WarhammerOldWorld.QuestManagment
{
    class QuestActionStructure
    {
        public QuestActionStructure(QuestAction current, ILoggable updateLogs)
        {
            currentAction = current;
            this.updateLogs = updateLogs;
            Init();
        }
        [SaveableField(1)]
        QuestAction currentAction;
        //Checks whether an action has a follow up task
        [SaveableField(2)]
        Dictionary<QuestAction, QuestAction> hasActionOnComplete;
        [SaveableField(3)]
        Dictionary<QuestAction, QuestAction> hasActionOnFail;
        [SaveableField(4)]
        ILoggable updateLogs;
        //Adds a task on completion of another task
        public void AddQuestActionOnComplete(QuestAction onAction, QuestAction addingAction)
        {
            hasActionOnComplete.Add(onAction, addingAction);
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
            hasActionOnFail.Add(onAction, addingAction);
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
        [SaveableField(5)]
        JournalLog currentLog;
        public void Init()
        {
            hasActionOnComplete = new Dictionary<QuestAction, QuestAction>();
            hasActionOnFail = new Dictionary<QuestAction, QuestAction>();
            _actionChanged = new BehaviorSubject<QuestAction>(currentAction);
            ActionChanged().Subscribe((action) =>
            {
                #region Structure Logs
                currentAction = action;
                action.Init();

                //Shouldnt be called on save

                if (currentLog == null)
                    currentLog = updateLogs.AddDiscreteLogWrapper(action.TaskName(), action.TaskDescription(), action.StartProgress(), action.Target());


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
    }
}
