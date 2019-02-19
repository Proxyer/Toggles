using Harmony;
using RimWorld;
using Toggles.Source;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(UI_BackgroundMain))]
    [HarmonyPatch("BackgroundOnGUI")]
    class UI_BackgroundMain_Patch
    {
        internal UI_BackgroundMain_Patch() => InitToggles();

        static void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.StartScreen,
                    group: ButtonCat.MiscellaneousEntry
                    );
        }

        static string GetLabel()
        {
            return ButtonCat.MiscellaneousEntry + "_Background";
        }

        static bool Prefix()
        {
            return ToggleHandler.IsActive(GetLabel());
        }
    }
}