using System.Collections.Generic;
using Verse;

namespace Toggles
{
    // Populates mod with toggles.
    internal static class ToggleHandler
    {
        internal static List<Toggle> Toggles { get; } = new List<Toggle>();

        internal static Dictionary<string, Toggle> ToggleActive { get; } = new Dictionary<string, Toggle>();

        // Fast check for whether a toggle is active.
        internal static bool IsActive(string label)
        {
            Toggle tog = ToggleActive.TryGetValue(label);
            return tog != null ? tog.active : true;
        }

        // Create fast lookup for checking whether a certain toggle is active.
        internal static void MakeLookUp()
        {
            Toggles.ForEach(x => ToggleActive.Add(x.Label, x));
        }
    }
}