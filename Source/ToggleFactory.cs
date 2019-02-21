using System;

namespace Toggles
{
    // Class for creating instances of Toggles and saving these to memory.
    internal static class ToggleFactory
    {
        internal static void Add(string label, string root, string group, string rawLabel = "")
        {
            ToggleHandler.Toggles.Add(new Toggle(
                    label: label,
                    rawLabel: rawLabel,
                    root: root,
                    group: group
                    ));
        }
    }
}