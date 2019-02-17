namespace Toggles.Source
{
    // Hook for data initialization.
    internal class ModBase_Toggles : HugsLib.ModBase
    {
        public override string ModIdentifier => Constants.ModName;

        protected override bool HarmonyAutoPatch => false;

        public override void DefsLoaded()
        {
            base.DefsLoaded();
            ToggleHandler.InitHardcoded();
            ToggleHandler.InitGenerated();
            ToggleHandler.MakeLookUp();
            Mod_Toggles.CustomLoadSettings();
        }
    }
}