namespace Toggles
{
    // Holds all data about a particular setting in the mod.
    internal class Toggle
    {
        internal bool active = true;

        internal Toggle(string label) => Label = label;

        internal Toggle(string label, string group, string root, string patch, string mod)
        {
            Label = label;
            Group = group;
            Root = root;
            Patch = patch;
            Mod = mod;
            //DebugUtil.Log("New Toggle:\nlabel: " + Label + "\ngroup: " + Group + "\nroot: " + Root + "\npatch: " + Patch);
        }

        internal string Description => Label + "Desc";
        internal string Group { get; set; }
        internal string Root { get; set; }
        internal string Label { get; private set; }
        internal string Patch { get; set; }
        internal string Mod { get; set; }
    }
}