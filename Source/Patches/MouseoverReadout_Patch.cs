using Harmony;
using Toggles.Source;
using Verse;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(MouseoverReadout))]
    [HarmonyPatch("MouseoverReadoutOnGUI")]
    class MouseoverReadout_Patch
    {
        internal MouseoverReadout_Patch() => InitToggles();

        static void InitToggles()
        {
            ToggleFactory.Add(
                    label: Label,
                    root: ButtonCat.PlayScreen,
                    group: ButtonCat.HUD
                    );
        }

        static string Label { get; } = $"{ButtonCat.HUD}_MouseoverReadout";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label);
        }
    }
}