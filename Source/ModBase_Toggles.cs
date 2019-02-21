using HugsLib;
using System.Linq;
using System.Xml.Linq;
using Verse;

namespace Toggles.Source
{
    // Hook for data initialization.
    internal class ModBase_Toggles : ModBase
    {
        public override string ModIdentifier => Constants.ModName;

        //protected override bool HarmonyAutoPatch => false;

        public override void DefsLoaded()
        {
            base.DefsLoaded();
            ToggleHandler.MakeLookUp();
            Mod_Toggles.CustomLoadSettings();

            LoadCustomLetters();
        }

        void LoadCustomLetters()
        {
            XDocument doc = XDocument.Load("../CustomLetters.xml");

            var items = doc.Descendants("Letter")
                           //.Where(node => (string)node.Attribute("name") == "Name")
                           .Select(node => node.Value.ToString())
                           .ToList();

            items.ForEach(x => Log.Message(x));

            //var allElements = doc.Descendants();
            //foreach (var v in allElements)
            //{
            //    Log.Message(v.Name + " " + v.Value);
            //}

            //ScribeLoader scribe = new ScribeLoader();
            //scribe.InitLoading("../CustomLetters.xml");
            //Log.Message("curXmlParent " + scribe.curXmlParent.Name);
            //Log.Message("curParent " + scribe.curParent.ToString());
            //Log.Message("XML " + scribe.EnterNode);
            //Log.Message("XML " + scribe.curXmlParent.Name);
            //Log.Message("XML " + scribe.curXmlParent.Name);

            //XDocument xml = XDocument.Load("../CustomLetters.xml");

            //foreach (var node in xml)
            //{
            //}

            //foreach (var node in xml)
            //{
            //    string nodeName = xml.NodeType.ToString();
            //    string nodeValue = xml.NextNode.
            //    string time = coordinate.Attribute("time").Value;

            //    string initial = coordinate.Element("initial").Value;
            //    string final = coordinate.Element("final").Value;

            //    // do whatever you want to do with those items of information now
            //}
        }
    }
}