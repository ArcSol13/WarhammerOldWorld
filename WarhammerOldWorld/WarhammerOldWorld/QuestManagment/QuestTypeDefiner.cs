using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace WarhammerOldWorld.QuestManagment
{
    public class QuestTypeDefiner : CampaignBehaviorBase.SaveableCampaignBehaviorTypeDefiner
    {
        public QuestTypeDefiner()
            // This number is the SaveID, supposed to be a unique identifier
            : base(332_001_000)
        {
        }

        protected override void DefineClassTypes()
        {
            // Your quest goes here, second argument is the SaveID
            AddClassDefinition(typeof(KillUnitsQuest), 1);
            AddClassDefinition(typeof(QuestAction), 2);
            AddClassDefinition(typeof(QuestActionStructure), 4);
            AddClassDefinition(typeof(KillQuestAction), 8);
            AddInterfaceDefinition(typeof(ILoggable), 16);
        }
        protected override void DefineContainerDefinitions()
        {
            ConstructContainerDefinition(typeof(Dictionary<QuestAction, QuestAction>));
            ConstructContainerDefinition(typeof(List<BasicCharacterObject>));
        }
    }
}
