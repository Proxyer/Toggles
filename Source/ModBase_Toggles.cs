using HugsLib;
using Toggles.Patches;
using Verse;

namespace Toggles.Source
{
    // Hook for data initialization.
    internal class ModBase_Toggles : ModBase
    {
        public override string ModIdentifier => Constants.ModName;

        //protected override bool HarmonyAutoPatch => false;

        public override void DefsLoaded()
        {
            base.DefsLoaded();
            Scribe_Collections.Look<string>(ref Letter_Patch.customLetters, "CustomLetters");
            ToggleHandler.MakeLookUp();
            Mod_Toggles.CustomLoadSettings();
        }
    }
}