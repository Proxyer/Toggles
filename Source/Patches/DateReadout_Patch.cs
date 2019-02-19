using Harmony;
using RimWorld;
using Toggles.Source;
using UnityEngine;

namespace Toggles.Patches
{
    // Toggles the date on the HUD.
    [HarmonyPatch(typeof(DateReadout))]
    [HarmonyPatch("DateOnGUI")]
    [HarmonyPatch(new[] { typeof(Rect) })]
    class DateReadout_Patch
    {
        internal DateReadout_Patch() => InitToggles();

        static void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.PlayScreen,
                    group: ButtonCat.HUD
                    );
        }

        static string GetLabel() => ButtonCat.HUD + "_Date";

        // Stops the date from being drawn if setting is inactive.
        static bool Prefix()
        {
            return ToggleHandler.IsActive(GetLabel());
        }
    }
}