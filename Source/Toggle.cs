namespace Toggles
{
    // Holds all data about a particular setting in the mod.
    internal class Toggle
    {
        internal bool active = true;

        internal Toggle(string label) => Label = label;

        internal string Description => Label + "Desc";
        internal string Group { get; set; }
        internal string Root { get; set; }
        internal string Label { get; private set; }
        internal string Patch { get; set; }
        internal string Mod { get; set; }
    }
}