using System;

namespace Toggles
{
    // Class for creating instances of Toggles and saving these to memory.
    internal static class ToggleFactory
    {
        internal static void Add(string label, string root, string group)
        {
            ToggleHandler.Toggles.Add(new Toggle(
                    label: label,
                    root: root,
                    group: group
                    ));
        }

        internal static void Add(string label, object root, string group)
        {
            throw new NotImplementedException();
        }
    }
}