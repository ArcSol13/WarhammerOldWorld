using TaleWorlds.MountAndBlade;
using System.Collections.Generic;
using TaleWorlds.Core;
using WarhammerOldWorld.Modules;
using TaleWorlds.CampaignSystem;

namespace WarhammerOldWorld
{
    public class ModuleManager : MBSubModuleBase
    {
        private readonly List<CustomSubModule> activeModules = new List<CustomSubModule>()
        {
            new TroopAttributeSubModule(),
            new QuestSubModule()
        };

        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);

            foreach (var module in activeModules)
                module.OnApplicationTick(dt);
        }

        public override void OnGameLoaded(Game game, object initializerObject)
        {
            base.OnGameLoaded(game, initializerObject);

            foreach (var module in activeModules)
                module.OnGameLoaded(game, initializerObject);
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            foreach (var module in activeModules)
                module.OnSubModuleLoad();
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

            foreach (var module in activeModules)
                module.OnSubModuleUnloaded();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            foreach (var module in activeModules)
                module.OnBeforeInitialModuleScreenSetAsRoot();
        }

        public override void OnConfigChanged()
        {
            base.OnConfigChanged();

            foreach (var module in activeModules)
                module.OnConfigChanged();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            foreach (var module in activeModules)
                module.OnGameStart(game, gameStarterObject);
        }

        public override void OnNewGameCreated(Game game, object initializerObject)
        {
            base.OnNewGameCreated(game, initializerObject);

            foreach (var module in activeModules)
                module.OnNewGameCreated(game, initializerObject);
        }

        public override void BeginGameStart(Game game)
        {
            base.BeginGameStart(game);

            foreach (var module in activeModules)
                module.BeginGameStart(game);
        }

        public override void OnCampaignStart(Game game, object starterObject)
        {
            base.OnCampaignStart(game, starterObject);

            foreach (var module in activeModules)
                module.OnCampaignStart(game, starterObject);
        }

        public override void OnMultiplayerGameStart(Game game, object starterObject)
        {
            base.OnMultiplayerGameStart(game, starterObject);

            foreach (var module in activeModules)
                module.OnMultiplayerGameStart(game, starterObject);
        }

        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);

            foreach (var module in activeModules)
                module.OnGameInitializationFinished(game);
        }

        public override bool DoLoading(Game game)
        {
            return base.DoLoading(game);
        }

        public override void OnGameEnd(Game game)
        {
            base.OnGameEnd(game);

            foreach (var module in activeModules)
                module.OnGameEnd(game);
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            base.OnMissionBehaviourInitialize(mission);

            foreach (var module in activeModules)
                module.OnMissionBehaviourInitialize(mission);
        }
    }
}
