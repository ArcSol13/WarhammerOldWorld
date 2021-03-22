using TaleWorlds.MountAndBlade;

namespace WarhammerOldWorld.CustomAgentComponents.MoraleAgentComponents
{
    class UnbreakableMoraleAgentComponent : MoraleAgentComponent
    {
        private float _morale = 100f;

        public new bool IsRetreating
        {
            get
            {
                return false;
            }
        }

        public new bool IsPanicked
        {
            get
            {
                return false;
            }
        }

        public new float Morale
        {
            get
            {
                return this._morale;
            }
            set
            {
                this._morale = 100f;
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
            this._morale = 100f;
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
