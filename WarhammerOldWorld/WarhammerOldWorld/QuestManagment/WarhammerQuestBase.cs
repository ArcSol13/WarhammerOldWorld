using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace WarhammerOldWorld.QuestManagment
{
    public abstract class WarhammerQuestBase : QuestBase,ILoggable
    {
        protected QuestActionStructure structure;
        protected WarhammerQuestBase(string questId, Hero questGiver, CampaignTime duration, int rewardGold) : base(questId, questGiver, duration, rewardGold)
        {
        }


        protected override void InitializeQuestOnGameLoad()
        {
            SetDialogs();
            structure.Init();
        }

        public override bool IsSpecialQuest => true;

        public JournalLog AddDiscreteLogWrapper(TextObject text, TextObject task, int currentProgress, int targetProgress) => AddDiscreteLog(text, task, currentProgress, targetProgress, hideInformation: false);

        public JournalLog AddLogWrapper(TextObject text) => AddLog(text);
    }
}
