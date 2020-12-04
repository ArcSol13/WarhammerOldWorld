using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;

namespace WarhammerOldWorld.QuestManagement
{
    abstract class Quest
    {
        protected abstract void OnComplete();

        protected abstract void OnFail();

        public string StartDialogue { get; set; }
    }

    class KillQuest : Quest
    {
        protected override void OnComplete()
        {
            throw new NotImplementedException();
        }

        protected override void OnFail()
        {
            throw new NotImplementedException();
        }
    }
}
