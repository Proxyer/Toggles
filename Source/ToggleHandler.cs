using RimWorld;
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
        internal static List<Toggle> Toggles { get; } = new List<Toggle>();

        internal static Dictionary<string, Toggle> ToggleActive { get; } = new Dictionary<string, Toggle>();

        // Generate dynamic toggles.
        internal static void InitGenerated()
        {
            foreach (IncidentDef incident in DefDatabase<IncidentDef>.AllDefsListForReading)
                ToggleFactory.Add(
                    label: "Incident" + incident.defName,
                    translatableLabel: incident.defName,
                    group: "Incidents",
                    root: "InGameUI",
                    patch: "IncidentWorker_Patch",
                    description: incident.description
                    );

            foreach (Type alert in typeof(Alert).AllLeafSubclasses())
                ToggleFactory.Add(
                    label: alert.Name,
                    translatableLabel: alert.Name,
                    group: "Alerts",
                    root: "InGameUI",
                    patch: "AlertsReadout_Patch"
                    );

            foreach (LetterDef letter in DefDatabase<LetterDef>.AllDefsListForReading)
                ToggleFactory.Add(
                    label: "Letter" + letter.defName,
                    translatableLabel: letter.defName,
                    group: "Letters",
                    root: "InGameUI",
                    patch: "Letter_Patch"
                    );
        }

        // Fast check for whether a toggle is active.
        internal static bool IsActive(string label)
        {
            Toggle tog = ToggleActive.TryGetValue(label);
            return tog != null ? tog.active : true;
        }

        // Read DB into memory from XML.
        internal static void InitHardcoded()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Constants.DBPath);
            XmlReader reader = XmlReader.Create(stream);
            XElement node = XElement.Load(reader);
            node.DescendantNodesAndSelf().Where(x => x.NodeType == XmlNodeType.Comment).Remove();
            LoadXML(node, Toggles);
        }

        // Create fast lookup for checking whether a certain toggle is active.
        internal static void MakeLookUp()
        {
            Toggles.ForEach(x => ToggleActive.Add(x.Label, x));
        }

        // Removes toggles from DB whose dependent mod is not active.
        internal static void TrimToggles()
        {
            Toggles.Where(x => !x.Mod.NullOrEmpty()).ToList().ForEach(x =>
            {
                if (!Mod_Toggles.ModIsActive(x.Mod))
                    Toggles.Remove(x);
            });
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