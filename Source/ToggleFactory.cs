namespace Toggles
{
    // Class for creating instances of Toggles and saving these to memory.
    internal static class ToggleFactory
    {
        internal static void Add(string label, string root, string group)
        {
            Toggle toggle = new Toggle(
                    label: label,
                    root: root,
                    group: group
                    );

            ToggleHandler.Toggles.Add(toggle);
            ToggleHandler.ToggleActive.Add(label, toggle);
        }

        internal static void Remove(string label)
        {
            ToggleHandler.Toggles.RemoveAll(x => x.Label.Equals(label));

            ToggleHandler.ToggleActive.Remove(label);
        }
    }
}