using Harmony;
using RimWorld;

namespace Toggles.Patches
{
    // Toggles the temperature readout on the HUD.
    [HarmonyPatch(typeof(GlobalControls))]
    [HarmonyPatch("TemperatureString")]
    class GlobalControls_Patch
    {
        internal GlobalControls_Patch() =>
            ToggleManager.Add(
                label: Label,
                root: ButtonCat.PlayScreen,
                group: ButtonCat.Readouts
                );

        static string Label => $"{ButtonCat.Readouts}_Temperature";

        // Stops the temperature from being drawn if setting is inactive.
        static string Postfix(string __result) => ToggleManager.IsActive(Label) ? __result : string.Empty;
    }
}