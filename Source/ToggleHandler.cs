using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using Verse;

namespace Toggles
{
    // Populates mod with toggles.
    [StaticConstructorOnStartup]
    internal static class ToggleHandler
    {
        static ToggleHandler()
        {
            Init();
            TrimToggles();
            MakeLookUp();
        }

        internal static List<Toggle> Toggles { get; private set; } = new List<Toggle>();

        internal static Dictionary<string, Toggle> ToggleActive { get; private set; } = new Dictionary<string, Toggle>();

        // Fast check for whether a toggle is active.
        internal static bool IsActive(string label)
        {
            Toggle tog = ToggleActive.TryGetValue(label);
            return tog != null ? tog.active : true;
        }

        // Read DB into memory.
        static void Init()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Constants.DBPath);
            XmlReader reader = XmlReader.Create(stream);
            XElement node = XElement.Load(reader);
            node.DescendantNodesAndSelf().Where(x => x.NodeType == XmlNodeType.Comment).Remove();
            LoadXML(node, Toggles);
        }

        // Create fast lookup for checking whether a certain toggle is active.
        static void MakeLookUp()
        {
            Toggles.ForEach(x => ToggleActive.Add(x.Label, x));
        }

        // Removes toggles from DB whose dependent mod is not active.
        static void TrimToggles()
        {
            Toggles.Where(x => !x.Mod.NullOrEmpty()).ToList().ForEach(x =>
            {
                if (!Mod_Toggles.ModIsActive(x.Mod))
                    Toggles.Remove(x);
            });
        }

        [Obsolete]
        static void Init_Old()
        {
            XElement mainNode = XElement.Load(Constants.InitFilePath);
            mainNode.DescendantNodesAndSelf().Where(x => x.NodeType == XmlNodeType.Comment).Remove();
            LoadXML(mainNode, Toggles);
        }

        // Walks through DB and creates Toggles out of nodes.
        static void LoadXML(XElement node, List<Toggle> toggles)
        {
            if (node.HasElements)
                foreach (XElement subNode in node.Nodes())
                    if (!subNode.HasElements && !subNode.Value.NullOrEmpty())
                        ToggleFactory.Add(node.Name.LocalName, subNode.Name.LocalName, subNode.Value);
                    else
                        LoadXML(subNode, toggles);
        }
    }
}