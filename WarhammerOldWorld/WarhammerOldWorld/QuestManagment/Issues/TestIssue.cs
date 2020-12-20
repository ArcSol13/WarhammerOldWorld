using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace WarhammerOldWorld.QuestManagment.Issues
{
    /// <summary>
    /// Test Class. Example of how to add issues to issue rotation (Spawn them randomly)
    /// If you want to force a issue to a npc just use the constructor of the Issue.
    /// </summary>
    public class IssueTestBehavior : CampaignBehaviorBase
    {
        //condition if the notable can have the issue
        private bool ConditionsHold(Hero hero) => true;

        public void OnCheckForIssue(IssueArgs issueArgs)
        {
            if (this.ConditionsHold(issueArgs.IssueOwner))
            {
                issueArgs.SetPotentialIssueData(new PotentialIssueData(new Func<PotentialIssueData, Hero, IssueBase>(this.OnStartIssue), typeof(TestIssue), IssueBase.IssueFrequency.VeryCommon, null));
                return;
            }
            issueArgs.SetPotentialIssueData(new PotentialIssueData(typeof(TestIssue), IssueBase.IssueFrequency.VeryCommon));
        }
        public override void RegisterEvents()
        {
            //Callback  when M&B recalculates if a npc needs a issue
            CampaignEvents.OnCheckForIssueEvent.AddNonSerializedListener(this, new Action<IssueArgs>(this.OnCheckForIssue));
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
        private IssueBase OnStartIssue(PotentialIssueData pid, Hero issueOwner)
        {
            // This is what actually makes the issue
            return new TestIssue(issueOwner);
        }
    }

    public class IssueDefiner : CampaignBehaviorBase.SaveableCampaignBehaviorTypeDefiner
    {
        public IssueDefiner() : base(383_004_001)
        {
        }

        protected override void DefineClassTypes()
        {
            AddClassDefinition(typeof(TestIssue), 1);
        }
    }

    /// <summary>
    /// This is a test class, dont use it.
    /// </summary>
    public class TestIssue : IssueBase
    {

        Hero issueOwner;
        public TestIssue(Hero issueOwner) : base(issueOwner, new Dictionary<IssueEffect, float>() { }, CampaignTime.WeeksFromNow(2))
        {
            this.issueOwner = issueOwner;
        }
        //Title on that is displayed on the dialog after talking to npc
        public override TextObject Title => new TextObject("Problem with Troops.");
        //Not sure where this is 
        public override TextObject Description => new TextObject("This lord has a problem with a certain troop.");
        //Thi is the what the npc says after players says I heard you have a problem...
        protected override TextObject IssueBriefByIssueGiver => new TextObject("There is a matter you would be perfect for; you look like a capable sort. There is a grudge that needs settling! Our warriors are stretched thin guarding the deep and I lack the warriors to spare, a brewery was raided by filthy grobbies and this cannot go unpunished. Seek vengeance on my behalf and you shall be rewarded, slay their kin and return once you have reaped a deadly toll. ");
        protected override TextObject IssueAcceptByPlayer => new TextObject(" Okay! :smile: ");
        protected override TextObject IssueQuestSolutionExplanationByIssueGiver => new TextObject("Kill kill kill kill");
        protected override TextObject IssueQuestSolutionAcceptByPlayer => new TextObject(" Okay! :smile:");
        // can troops/companion solve quest?
        protected override bool IsThereAlternativeSolution => false;
        // Can player finish it?
        protected override bool IsThereLordSolution => true;

        //Frequency of spawning, think this isnt used anywhere
        public override IssueFrequency GetFrequency() => IssueFrequency.Common;

        // Called on entering the settlement where quest giver is. If this check fails quest will be removed
        public override bool IssueStayAliveConditions() => true;

        // Think this is the actual check whether the quest can spawn
        protected override bool CanPlayerTakeQuestConditions(Hero issueGiver, out PreconditionFlags flag, out Hero relationHero, out SkillObject skill)
        {
            relationHero = null;
            skill = null;
            flag = IssueBase.PreconditionFlags.None;
            return true;
        }

        protected override void CompleteIssueWithTimedOutConsequences()
        {
        }
        // if accepted this quest will apear in journal
        protected override QuestBase GenerateIssueQuest(string questId)
            => new KillUnitsQuest(issueOwner, new List<BasicCharacterObject> { MBObjectManager.Instance.GetObject<BasicCharacterObject>("looter") }, 10);

        //This is for saving if something has to be done on load. Probably
        protected override void OnGameLoad()
        {
        }
    }

}
