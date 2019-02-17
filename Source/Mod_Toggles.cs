using UnityEngine;
using Verse;

namespace Toggles
{
    // Holds and performs mod specific behaviours.
    internal class Mod_Toggles : Mod
    {
        public Mod_Toggles(ModContentPack content) : base(content)
        {
            ModInstance = this;
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

        static ModSettings_Toggles Settings;
        static Mod_Toggles ModInstance;

        // Checks whether a specific mod is currently active.
        internal static bool ModIsActive(string mod)
        {
            return ModLister.HasActiveModWithName(mod);
        }

        // Delayed initialization of mod settings for all defs to have loaded first.
        internal static void CustomLoadSettings()
        {
            Settings = ModInstance.GetSettings<ModSettings_Toggles>();
        }
    }
}