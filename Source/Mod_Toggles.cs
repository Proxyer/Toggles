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

        internal void InitSettings()
        {
            GetSettings<ModSettings_Toggles>();
        }

        internal static Mod_Toggles ModInstance;

        public override string SettingsCategory()
        {
            return Constants.ModName;
        }

        // Mod settings window.
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Dialog_Settings.DoWindowContents(inRect);
        }

        // Checks whether a specific mod is currently active.
        //internal static bool ModIsActive(string mod)
        //{
        //    return ModLister.HasActiveModWithName(mod);
        //}
    }
}