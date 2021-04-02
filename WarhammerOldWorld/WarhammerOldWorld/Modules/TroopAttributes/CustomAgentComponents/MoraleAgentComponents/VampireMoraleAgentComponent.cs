using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace WarhammerOldWorld.CustomAgentComponents.MoraleAgentComponents
{
    public class VampireMoraleAgentComponent : MoraleAgentComponent
    {
        private Timer crumbleTimer;

        private bool canCrumble = false;

        private float crumbleFrequencyInSeconds = 1f;

        public VampireMoraleAgentComponent(Agent agent) : base(agent)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();
            float time = MBCommon.GetTime(MBCommon.TimeType.Mission);
            this.crumbleTimer = new Timer(time, crumbleFrequencyInSeconds, true);
            this.InitializeMorale();
        }

        protected override void OnTickAsAI(float dt)
        {
            if(base.Morale < 20f)
            {
                canCrumble = crumbleTimer.Check(MBCommon.GetTime(MBCommon.TimeType.Mission));
                if(canCrumble)
                {
                    float damageTaken = 0.01f / base.Morale;
                    damageTaken = MBMath.ClampFloat(damageTaken, 0, 1);
                    this.Agent.Health -= damageTaken;
                    crumbleTimer.Reset(MBCommon.GetTime(MBCommon.TimeType.Mission));
                    Helpers.Say(this.Agent.Name + " took " + damageTaken + " damage from low morale.");
                }
            }
        }

        private void InitializeMorale()
        {
            Helpers.Say("Vampire initialize morale");
            float num = 35f;
            int num2 = MBRandom.RandomInt(30);
            float num3 = num + (float)num2;
            num3 = MissionGameModels.Current.BattleMoraleModel.GetEffectiveInitialMorale(this.Agent, num3);
            num3 = MBMath.ClampFloat(num3, 15f, 100f);
            base.Morale = num3;
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

        protected override void OnHit(Agent affectorAgent, int damage, in MissionWeapon affectorWeapon)
        {
        }
    }
}
