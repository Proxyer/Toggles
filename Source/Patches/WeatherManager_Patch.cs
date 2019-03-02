using Harmony;
using RimWorld;
using UnityEngine;

namespace Toggles.Patches
{
    // Toggles the weather readout on the HUD.
    [HarmonyPatch(typeof(WeatherManager))]
    [HarmonyPatch("DoWeatherGUI")]
    [HarmonyPatch(new[] { typeof(Rect) })]
    class WeatherManager_Patch
    {
        internal WeatherManager_Patch() =>
            ToggleManager.Add(
                label: Label,
                root: ButtonCat.PlayScreen,
                group: ButtonCat.Readouts
                );

        static string Label => $"{ButtonCat.Readouts}_Weather";

        // Stops the weather readout from being drawn if setting is inactive.
        static bool Prefix() => ToggleManager.IsActive(Label);
    }
}