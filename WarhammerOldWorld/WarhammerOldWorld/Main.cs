using System;
using TaleWorlds.MountAndBlade;
using System.Reactive.Subjects;
using TaleWorlds.Engine;
using System.Reactive.Linq;
using System.Collections.Generic;
using TaleWorlds.InputSystem;
using WarhammerOldWorld.ObjectManagment;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;
using TaleWorlds.CampaignSystem;
using Helpers;

namespace WarhammerOldWorld
{
    public class ModuleControllerSubModule : MBSubModuleBase
    {
        public static ModuleControllerSubModule Instance { get; private set; }

        List<CustomSubModule> activeModules = new List<CustomSubModule>();
        public void LoadCustomModule(CustomSubModule module)
        {
            module.OnLoad();
            activeModules.Add(module);
        }
        public void UnloadCustomModule(CustomSubModule module)
        {
            module.OnUnload();
            activeModules.Remove(module);
        }


        BehaviorSubject<Scene> sceneSubject = new BehaviorSubject<Scene>(null);

        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);
            foreach (var module in activeModules)
                module.OnUpdate();
            //Idk why agent.main isnt instantiated at scene start...
            if (Mission.Current != null && Agent.Main!=null && sceneSubject.Value != Mission.Current.Scene)
                sceneSubject.OnNext(Mission.Current.Scene);

            if (Input.IsKeyPressed(InputKey.G))
            {
            }
        }
        public override void OnGameLoaded(Game game, object initializerObject)
        {
            base.OnGameLoaded(game, initializerObject);
            var questGiver = MBObjectManager.Instance.GetObject<Hero>((x) => { return x != Hero.MainHero && x.IsAlive; });
            var target = MBObjectManager.Instance.GetObject<BasicCharacterObject>(BasicCharacterObjectManager.Instance.GetXmlByID("looter").Attributes[0].Value);
           
            //starting quest here becouse i didnt figure out how to assign quests to npcs
            Observable.Timer(TimeSpan.FromSeconds(10)).Subscribe((X) => new TestQuest(questGiver,target,20).StartQuest());
        }
        protected override void OnSubModuleLoad()
        {
            Instance = this;


        }

        /// <summary>
        /// Tracks scene changes, null at start
        /// </summary>
        /// <returns></returns>
        public IObservable<Scene> SceneChangeObservable() => sceneSubject.AsObservable();
        public Scene GetCurrentScene() => sceneSubject.Value;
        /// <summary>
        /// Returns player character
        /// </summary>
        /// <returns></returns>
        public Agent GetMainCharacter() => Agent.Main;
    }
}
