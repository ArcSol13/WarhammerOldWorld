using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using WarhammerOldWorld.Combat;
using WarhammerOldWorld.Extensions;

namespace WarhammerOldWorld.Modules.TroopAttributes.CustomAgentComponents.MoraleAgentComponents
{
    public class UndeadMoraleAgentComponent : MoraleAgentComponent
    {
        private Timer crumbleTimer;

        private bool canCrumble = false;

        private float crumbleFrequencyInSeconds = 1f;

        public UndeadMoraleAgentComponent(Agent agent) : base(agent)
        {
            agent.RemoveComponentIfNotNull(agent.GetComponent<MoraleAgentComponent>());
        }

        protected override void Initialize()
        {
            base.Initialize();
            float time = MBCommon.GetTime(MBCommon.TimeType.Mission);
            crumbleTimer = new Timer(time, crumbleFrequencyInSeconds, true);
            InitializeMorale();
        }

        protected override void OnTickAsAI(float dt)
        {
            if (Morale < 20f)
            {
                canCrumble = crumbleTimer.Check(MBCommon.GetTime(MBCommon.TimeType.Mission));
                if (canCrumble)
                {
                    float damageTaken = 0.01f / Morale;
                    damageTaken = MBMath.ClampFloat(damageTaken, 0, 1);
                    Agent.ApplyDamage(damageTaken);
                    crumbleTimer.Reset(MBCommon.GetTime(MBCommon.TimeType.Mission));
                    Helpers.Say(Agent.Name + " took " + damageTaken + " crumbling damage from low binding");
                }
            }
        }

        private void InitializeMorale()
        {
            Helpers.Say("Initialized UndeadMoraleAgentComponent");
            float num = 35f;
            int num2 = MBRandom.RandomInt(30);
            float num3 = num + num2;
            num3 = MissionGameModels.Current.BattleMoraleModel.GetEffectiveInitialMorale(Agent, num3);
            num3 = MBMath.ClampFloat(num3, 15f, 100f);
            Morale = num3;
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
