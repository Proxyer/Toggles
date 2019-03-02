using Harmony;
using RimWorld;
using UnityEngine;

namespace Toggles.Patches
{
    // Toggles the date on the HUD.
    [HarmonyPatch(typeof(DateReadout))]
    [HarmonyPatch("DateOnGUI")]
    [HarmonyPatch(new[] { typeof(Rect) })]
    class DateReadout_Patch
    {
        internal DateReadout_Patch() =>
            ToggleManager.Add(
                label: Label,
                root: ButtonCat.PlayScreen,
                group: ButtonCat.Readouts
                );

        static string Label => $"{ButtonCat.Readouts}_Date";

        // Stops the date from being drawn if setting is inactive.
        static bool Prefix() => ToggleManager.IsActive(Label);
    }
}