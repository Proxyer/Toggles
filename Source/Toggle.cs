namespace Toggles
{
    // Holds all data about a particular setting in the mod.
    internal class Toggle
    {
        internal bool active = true;

        string label;

        internal Toggle(string label)
        {
            Label = label;
        }

        internal Toggle(string label, string root, string group, string patch)
        {
            Label = label;
            Root = root;
            Group = group;
            Patch = patch;
        }

        internal string Group { get; set; }
        internal string Root { get; set; }

        internal string Patch { get; set; }

        internal string LabelGUI { get; set; }
        internal string LabelInternal { get; set; }

        internal string Label
        {
            get => label;
            //get => StringUtil.Pretty(label);

            set => label = value;
        }
    }
}