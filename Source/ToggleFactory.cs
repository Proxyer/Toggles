namespace Toggles
{
    // Class for creating instances of Toggles and saving these to memory.
    internal static class ToggleFactory
    {
        internal static void Add(string label, string group, string root, string patch, string translatableLabel = null, string description = null, string mod = null)
        {
            ToggleHandler.Toggles.Add(new Toggle(
                    label: label,
                    translatableLabel: translatableLabel,
                    group: group,
                    root: root,
                    patch: patch,
                    description: description,
                    mod: mod
                    ));
        }

        // Creates a new toggle with given label, if it does not already exist.
        // Also adds values to fields of toggles matching the cases.
        internal static void Add(string label, string key, string value)
        {
            if (!ToggleHandler.Toggles.Exists(x => x.Label.Equals(label)))
                ToggleHandler.Toggles.Add(new Toggle(label));
            Toggle toggle = ToggleHandler.Toggles.Find(x => x.Label.Equals(label));

            switch (key)
            {
                case "group":
                    toggle.Group = value;
                    break;

                case "root":
                    toggle.Root = value;
                    break;

                case "patch":
                    toggle.Patch = value;
                    break;

                case "mod":
                    toggle.Mod = value;
                    break;

                default:
                    break;
            }
        }
    }
}