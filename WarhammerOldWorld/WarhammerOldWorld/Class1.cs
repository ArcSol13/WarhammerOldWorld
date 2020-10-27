using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.Encyclopedia.Pages;
using System.Linq;

namespace WarhammerOldWorld.ObjectManagment
{
    /// <summary>
    /// Generic Instantiator
    /// </summary>
    public abstract class ObjectManager<T>  where T : MBObjectBase,new()
    {
        public ObjectManager() 
        {
            LoadXmls();
        }
        protected List<XmlNode> xmlNodes = new List<XmlNode>();
        protected virtual void LoadXmls()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToXML());
            try
            {
                foreach (XmlNode node in doc.DocumentElement)
                {
                    if (node.NodeType == XmlNodeType.Comment)
                        continue;

                    xmlNodes.Add(node);
                }
            }
            catch
            {

            }
        }
        public abstract Type GetObjectType();

        public abstract void Instantiate();

        public ObjectManager<T> Instance { get; private set; }
        protected T InstantiateInternal() => Game.Current.ObjectManager.CreateObject<T>();
        protected T InstantiateInternal(string stringID) => Game.Current.ObjectManager.CreateObject<T>(stringID);
        protected abstract void Deserialize(T obj);
        protected abstract string PathToXML();
        public abstract void Destroy(T obj);
    }


    public class CharacterObjectManager : ObjectManager<CharacterObject>
    {
        List<XmlNode> heroNodes = new List<XmlNode>();
        public override void Destroy(CharacterObject obj)
        {
        }
        public override Type GetObjectType() => typeof(CharacterObject);

        protected override string PathToXML() => Path.Combine(BasePath.Name, "Modules", "WarhammerOldWorld", "ModuleData", "Data", "lords.xml");
        
        string PathToHeroesXML() => Path.Combine(BasePath.Name, "Modules", "WarhammerOldWorld", "ModuleData", "Data", "heroes.xml");
        protected override void LoadXmls()
        {
            base.LoadXmls();
            XmlDocument doc = new XmlDocument();
            doc.Load(PathToHeroesXML());
            try
            {
                foreach (XmlNode node in doc.DocumentElement)
                {
                    if (node.NodeType == XmlNodeType.Comment)
                        continue;

                    heroNodes.Add(node);
                }
            }
            catch
            {

            }

        }

        public override void Instantiate()
        {
            var character = InstantiateInternal();
            character.Initialize();
            Deserialize(character);
            //Test purposes only :P
            character.Name = new TaleWorlds.Localization.TextObject("IMROBERTSSLAVEFOREVER");
            Game.Current.ObjectManager.RegisterObject(character);
        }

        protected override void Deserialize(CharacterObject obj)
        {
            var character = obj;
            var node = xmlNodes.GetRandomElement();
            character.Deserialize(Game.Current.ObjectManager, node);
            character.InitializeHeroBasicCharacterOnAfterLoad(character, character.Name);
            if (character.IsHero)
            {
                try
                {

                    var myHero = heroNodes.Where((x) => x.Attributes[0].Value == character.StringId).First();
                    character.HeroObject.Deserialize(Game.Current.ObjectManager, myHero);
                    character.HeroObject.Init();
                    character.HeroObject.Initialize();
                }
                catch
                {
                    throw new Exception("Character is a hero but is not contained in heroes.xml");
                }
            }
        }
    }


}
