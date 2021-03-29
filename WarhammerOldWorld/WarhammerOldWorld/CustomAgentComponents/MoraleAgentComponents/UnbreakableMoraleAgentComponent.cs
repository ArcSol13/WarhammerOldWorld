using TaleWorlds.MountAndBlade;
using WarhammerOldWorld.Extensions;

namespace WarhammerOldWorld.CustomAgentComponents.MoraleAgentComponents
{
    public class UnbreakableMoraleAgentComponent : MoraleAgentComponent
    {
        public new float Morale
        {
            get
            {
                Helpers.Say(base.Morale.ToString());
                return base.Morale;
            }
            set
            {
                Helpers.Say(base.Morale.ToString());
                base.Morale = 100f;
            }
        }

        public UnbreakableMoraleAgentComponent(Agent agent) : base(agent)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.InitializeMorale();
        }

        private void InitializeMorale()
        {
            Helpers.Say("Unbreakable initialize morale");
            base.Morale = 100f;
        }
        protected override void OnTickAsAI(float dt)
        {
            Agent.GetCustomMoraleComponents().ForEach(component =>
            {
                component.Morale = 100;
            });
        }

        public new void Panic()
        {

        }
        public new void Retreat()
        {

        }
        public new void StopRetreating()
        {

        }
    }
}
