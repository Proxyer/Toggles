using System.Collections.Generic;
using System.Linq;
using Toggles.Patches;
using Verse;

namespace Toggles
{
    // Populates mod with toggles.
    internal static class ToggleManager
    {
        internal static List<Toggle> Toggles { get; } = new List<Toggle>();

        static Dictionary<string, Toggle> ToggleActive { get; } = new Dictionary<string, Toggle>();

        // Checks if toggle is active. Returns true if toggle does not exist.
        internal static bool IsActive(string label) => Exists(label) ? ToggleActive.TryGetValue(label).active : true;

        // Checks if toggle exists.
        internal static bool Exists(string label) => ToggleActive.ContainsKey(label);

        // Creates and adds toggle to settings.
        internal static void Add(string label, string root, string group)
        {
            if (!Exists(label))
            {
                Toggle toggle = new Toggle(
                        label: label,
                        root: root,
                        group: group
                        );
                Toggles.Add(toggle);
                ToggleActive.Add(label, toggle);
            }
        }

        internal static void ToggleMany(string hotkeyLabel)
        {
            Toggles.Where(toggle => toggle.Hotkey.Equals(hotkeyLabel)).ToList().ForEach(x => x.active = !x.active);
        }

        // Removes toggle from settings.
        internal static void Remove(string label)
        {
            Toggles.RemoveAll(x => x.Label.Equals(label));
            ToggleActive.Remove(label);
        }

        internal static void Reset()
        {
            Letter_Patch.RemoveCustomLetters();
            Toggles.ForEach(x => { x.active = true; x.Hotkey = string.Empty; });
            AlertsReadout_Patch.hourMultiplier = 2;
        }
    }
}