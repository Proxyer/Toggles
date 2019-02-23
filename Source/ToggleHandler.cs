using System.Collections.Generic;
using Verse;

namespace Toggles
{
    // Populates mod with toggles.
    internal static class ToggleHandler
    {
        internal static List<Toggle> Toggles { get; } = new List<Toggle>();

        internal static Dictionary<string, Toggle> ToggleActive { get; private set; } = new Dictionary<string, Toggle>();

        // Fast check for whether a toggle is active.
        internal static bool IsActive(string label)
        {
            Toggle tog = ToggleActive.TryGetValue(label);
            return tog != null ? tog.active : true;
        }

        internal static bool Exists(string label)
        {
            return ToggleActive.ContainsKey(label);
        }
    }
}