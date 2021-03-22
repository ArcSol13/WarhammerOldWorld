
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace WarhammerOldWorld.Modules
{
        public abstract class CustomSubModule
        {
		    protected internal virtual void OnSubModuleLoad()
		    {
		    }

			protected internal virtual void OnSubModuleUnloaded()
			{
			}

			protected internal virtual void OnBeforeInitialModuleScreenSetAsRoot()
			{
			}

			public virtual void OnConfigChanged()
			{
			}

			protected internal virtual void OnGameStart(Game game, IGameStarter gameStarterObject)
			{
			}

			protected internal virtual void OnApplicationTick(float dt)
			{
			}

			public virtual void OnGameLoaded(Game game, object initializerObject)
			{
			}

			public virtual void OnNewGameCreated(Game game, object initializerObject)
			{
			}

			public virtual void BeginGameStart(Game game)
			{
			}

			public virtual void OnCampaignStart(Game game, object starterObject)
			{
			}

			public virtual void OnMultiplayerGameStart(Game game, object starterObject)
			{
			}

			public virtual void OnGameInitializationFinished(Game game)
			{
			}

			public virtual bool DoLoading(Game game)
			{
				return true;
			}

			public virtual void OnGameEnd(Game game)
			{
			}

			public virtual void OnMissionBehaviourInitialize(Mission mission)
			{
			}
	}

}
