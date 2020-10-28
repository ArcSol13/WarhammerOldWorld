using System;
using TaleWorlds.MountAndBlade;
using System.Reactive.Subjects;
using TaleWorlds.Engine;
using System.Reactive.Linq;
using System.Collections.Generic;
using TaleWorlds.InputSystem;
using WarhammerOldWorld.ObjectManagment;
using TaleWorlds.Core;

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

            if (Input.IsKeyPressed(InputKey.G))
            {
                CharacterObjectManager.Instance.Instantiate();
                HeroObjectManager.Instance.Instantiate();
                InformationManager.DisplayMessage(new InformationMessage("Character instantiated!"));
            }
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
