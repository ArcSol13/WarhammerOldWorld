using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.SaveSystem;

namespace WarhammerOldWorld.QuestManagment
{
    /// <summary>
    /// Action that is completed by killing certain types of specific enemies
    /// </summary>
    class KillQuestAction : QuestAction
    {
        public KillQuestAction(BasicCharacterObject target, int targetNumber) : base()
        {
            targetType = target;
            killsNeeded = targetNumber;
            Init();
        }
        ~KillQuestAction() => killCount?.Dispose();

        Subject<int> killCount;
        IDisposable sceneDisposable = null;

        [SaveableField(1)]
        int killsNeeded;
        [SaveableField(2)]
        int kills = 0;
        [SaveableField(3)]
        BasicCharacterObject targetType;

        public override IObservable<int> UpdateScore() => killCount.AsObservable();

        public override TextObject TaskName() => new TextObject("Kill Units");

        public override TextObject TaskDescription() => new TextObject("Kill " + targetType.GetName().ToString());

        public override int StartProgress() => kills;
        public override int Target() => killsNeeded;
        public override void Init()
        {
            base.Init();
            killCount = new Subject<int>();

            sceneDisposable = ModuleControllerSubModule.Instance.SceneChangeObservable().Subscribe((scene) =>
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
            OnComplete().Subscribe((x) => { if (x) sceneDisposable?.Dispose(); });
            OnFail().Subscribe((x) => { if (x) sceneDisposable?.Dispose(); });
            killCount.Subscribe((kills) =>
            {
                if (kills >= killsNeeded)
                {
                    onComplete.OnNext(true);
                    killCount.Dispose();
                }
            });
        }
        private void Main_OnAgentHealthChanged(Agent agent, float oldHealth, float newHealth)
        {
            if (killCount.IsDisposed)
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
    }
}
