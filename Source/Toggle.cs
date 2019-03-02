using Toggles.Hotkeys;
using Verse;

namespace Toggles
{
    // Holds all data about a particular setting in the mod.
    internal class Toggle
    {
        internal bool active = true;

        internal Toggle(string label, string root, string group)
        {
            Label = label;
            Root = root;
            Group = group;
        }

        internal string Group { get; private set; }
        internal string Root { get; private set; }

        internal string PrettyHotkey
        {
            get
            {
                if (HotkeyHandler.HotKeyDict.ContainsKey(hotkey))
                    return HotkeyHandler.HotKeyDict.TryGetValue(hotkey).CustomLabel;
                return hotkey;
            }
        }

        internal string Hotkey { get => hotkey; set => hotkey = value; }
        internal string hotkey = string.Empty;

        internal string Label { get; private set; }

        // String for GUI purposes. Returns translation if available, otherwise beautified attempt.
        internal string PrettyLabel { get => Label.CanTranslate() ? Label.Translate() : StringUtil.Pretty(Label); }

        internal void ExposeData()
        {
            Scribe_Values.Look(ref active, Label, true);
            Scribe_Values.Look(ref hotkey, Label + "Hotkey", string.Empty);
        }
    }
}