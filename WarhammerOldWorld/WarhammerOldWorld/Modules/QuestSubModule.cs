using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace WarhammerOldWorld.Modules
{
    class QuestSubModule : CustomSubModule
    {
        BehaviorSubject<Scene> sceneSubject = new BehaviorSubject<Scene>(null);
        public IObservable<Scene> SceneChangeObservable() => sceneSubject.AsObservable();
        public Scene GetCurrentScene() => sceneSubject.Value;
        public Agent GetMainCharacter() => Agent.Main;


        public override void BeginGameStart(Game game)
        {
            base.BeginGameStart(game);
        }

        public override bool DoLoading(Game game)
        {
            return base.DoLoading(game);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void OnCampaignStart(Game game, object starterObject)
        {
            base.OnCampaignStart(game, starterObject);
        }

        public override void OnConfigChanged()
        {
            base.OnConfigChanged();
        }

        public override void OnGameEnd(Game game)
        {
            base.OnGameEnd(game);
        }

        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
        }

        public override void OnGameLoaded(Game game, object initializerObject)
        {
            base.OnGameLoaded(game, initializerObject);
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            base.OnMissionBehaviourInitialize(mission);
        }

        public override void OnMultiplayerGameStart(Game game, object starterObject)
        {
            base.OnMultiplayerGameStart(game, starterObject);
        }

        public override void OnNewGameCreated(Game game, object initializerObject)
        {
            base.OnNewGameCreated(game, initializerObject);
        }

        protected internal override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);

            //Idk why agent.main isnt instantiated at scene start...
            if (Mission.Current != null && Agent.Main != null && sceneSubject.Value != Mission.Current.Scene)
                sceneSubject.OnNext(Mission.Current.Scene);

            if (Input.IsKeyPressed(InputKey.G))
            {
            }
        }

        protected internal override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
        }

        protected internal override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            //var questGiver = MBObjectManager.Instance.GetObject<Hero>((x) => { return x != Hero.MainHero && x.IsAlive; });
            //var target = MBObjectManager.Instance.GetObject<BasicCharacterObject>(BasicCharacterObjectManager.Instance.GetXmlByID("looter").Attributes[0].Value);

            //starting quest here becouse i didnt figure out how to assign quests to npcs
            //Observable.Timer(TimeSpan.FromSeconds(10)).Subscribe((X) => new TestQuest(questGiver,target,20).StartQuest());
        }

        protected internal override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
        }

        protected internal override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }
    }
}
