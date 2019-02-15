using UnityEngine;
using Verse;

namespace Toggles
{
    // Holds and performs mod specific behaviours.
    internal class Mod_Toggles : Mod
    {
        public Mod_Toggles(ModContentPack content) : base(content)
        {
            Settings = GetSettings<ModSettings_Toggles>();
        }

        public override string SettingsCategory()
        {
            return Constants.ModName;
        }

        // Mod settings window.
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Dialog_Settings.DoWindowContents(inRect);
        }

        public static ModSettings_Toggles Settings;

        // Checks whether a specific mod is currently active.
        internal static bool ModIsActive(string mod)
        {
            return ModLister.HasActiveModWithName(mod);
        }
    }
}