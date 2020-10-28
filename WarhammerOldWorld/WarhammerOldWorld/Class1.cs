using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

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
        protected T InstantiateInternal() => Game.Current.ObjectManager.CreateObject<T>();
        protected T InstantiateInternal(string stringID) => Game.Current.ObjectManager.CreateObject<T>(stringID);
        public abstract void Deserialize(T obj);
        protected abstract string PathToXML();
        public abstract void Destroy(T obj);

        public T Instantiate(string entry)
        {
            var result = MBObjectManager.Instance.CreateObjectFromXmlNode(GetXmlByID(entry));
            try
            {
                if (result is BasicCharacterObject)
                    (result as BasicCharacterObject).Name = new TaleWorlds.Localization.TextObject("Spawned Character!");
                return result as T;
            }
            catch
            {
                throw new Exception("Trying to instantiate " + result.ToString() + " with type " + typeof(T) + ", wrong type!");
            }
        }
    }


    public class BasicCharacterObjectManager : ObjectManager<BasicCharacterObject>
    {
        private static BasicCharacterObjectManager _instance;
        public static BasicCharacterObjectManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BasicCharacterObjectManager();
                return _instance;
            }
        }
        List<XmlNode> heroNodes = new List<XmlNode>();
        public override void Destroy(BasicCharacterObject obj)
        {
        }
        public override Type GetObjectType() => typeof(BasicCharacterObject);

        protected override string PathToXML() => Path.Combine(BasePath.Name, "Modules", "WarhammerOldWorld", "ModuleData", "Data", "lords.xml");
        
        public override void Deserialize(BasicCharacterObject obj)
        {
            var character = obj;
            character.Deserialize(Game.Current.ObjectManager, xmlNodes.GetRandomElement());
        }
    }

    public class CharacterObjectManager : ObjectManager<CharacterObject>
    {
        private static CharacterObjectManager _instance;
        public static CharacterObjectManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CharacterObjectManager();
                return _instance;
            }
        }
        public override void Deserialize(CharacterObject obj)
        {
            obj.Deserialize(MBObjectManager.Instance,xmlNodes.GetRandomElement());
        }

        public override void Destroy(CharacterObject obj)
        {
        }
        public override Type GetObjectType() => typeof(CharacterObject);

        protected override string PathToXML() => Path.Combine(BasePath.Name, "Modules", "WarhammerOldWorld", "ModuleData", "Data", "lords.xml");

    }

    public class HeroObjectManager : ObjectManager<Hero>
    {
        private static HeroObjectManager _instance;
        public static HeroObjectManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HeroObjectManager();
                return _instance;
            }
        }
        public override void Deserialize(Hero obj)
        {
            obj.Deserialize(MBObjectManager.Instance, xmlNodes.GetRandomElement());
        }

        public override void Destroy(Hero obj)
        {
        }

        public override Type GetObjectType() => typeof(Hero);

        protected override string PathToXML() => Path.Combine(BasePath.Name, "Modules", "WarhammerOldWorld", "ModuleData", "Data", "heroes.xml");

    }


}
