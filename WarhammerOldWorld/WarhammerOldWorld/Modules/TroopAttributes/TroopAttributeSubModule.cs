using System.Collections.Generic;
using System.Xml;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers.Logic;
using WarhammerOldWorld.Extensions;
using WarhammerOldWorld.Modules.TroopAttributes.CustomMissionLogic;

namespace WarhammerOldWorld.Modules.TroopAttributes
{
    public class TroopAttributeSubModule : CustomSubModule
    {
        protected internal override void OnSubModuleLoad()
        {
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            mission.AddMissionBehaviour(new TroopAttributeMissionLogic());
            populateTroopAttributes();

            // Replace the default morale interaction logic with our new custom morale logic
            mission.RemoveMissionBehaviour(mission.GetMissionBehaviour<AgentMoraleInteractionLogic>());
            mission.AddMissionBehaviour(new TowAgentMoraleInteractionLogic());
        }

        private void populateTroopAttributes()
        {
            Dictionary<string, List<string>> troopNameToAttributeList = new Dictionary<string, List<string>>();

            XmlDocument attributeXml = new XmlDocument();
            attributeXml.Load("C:\\Steam\\steamapps\\common\\Mount & Blade II Bannerlord\\Modules\\WarhammerOldWorld\\ModuleData\\VampireCounts\\attributes.xml");
            XmlNodeList characters = attributeXml.GetElementsByTagName("NPCCharacter");
            foreach (XmlNode character in characters)
            {
                List<string> attributes = new List<string>();
                foreach (XmlNode attributeNode in character.ChildNodes)
                {
                    if (attributeNode.NodeType == XmlNodeType.Comment)
                    {
                        continue;
                    }
                    attributes.Add(attributeNode.Attributes["name"].Value);
                }
                troopNameToAttributeList.Add(character.Attributes["name"].Value, attributes);
            }

            AgentExtensions.TroopNameToAttributeList = troopNameToAttributeList;
        }
    }
}
