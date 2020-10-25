using System;
using TaleWorlds.MountAndBlade;
using System.Reactive.Subjects;
using TaleWorlds.Engine;
using System.Reactive.Linq;
using System.Collections.Generic;

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

    }
}
