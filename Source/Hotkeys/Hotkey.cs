using Verse;

namespace Toggles
{
    internal class Hotkey
    {
        internal Hotkey(KeyBindingDef def) => Def = def;

        internal KeyBindingDef Def { get; private set; }

        internal string CustomLabel
        {
            get
            {
                if (customLabel == null)
                    return Def.defName;
                return customLabel;
            }

            set => customLabel = value;
        }

        string customLabel = "Hotkey";

        internal void ExposeData() => Scribe_Values.Look(ref customLabel, Def.defName, Def.defName);
    }
}