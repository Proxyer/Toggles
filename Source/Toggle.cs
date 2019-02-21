using Verse;

namespace Toggles
{
    // Holds all data about a particular setting in the mod.
    internal class Toggle
    {
        internal bool active = true;

        internal Toggle(string label, string root, string group, string rawLabel = "")
        {
            Label = label;
            this.rawLabel = rawLabel;
            Root = root;
            Group = group;
        }

        internal string rawLabel;
        internal string Group { get; set; }
        internal string Root { get; set; }

        internal string Label { get; set; }

        internal string PrettyLabel
        {
            get
            {
                if (!rawLabel.NullOrEmpty())
                    return rawLabel;
                if (Label.CanTranslate())
                    return Label.Translate();
                return StringUtil.Pretty(Label);
            }
        }

        internal void ExposeData()
        {
            Scribe_Values.Look(ref active, Label, true);
            //Scribe_Values.Look(ref rawLabel, Label);
        }
    }
}