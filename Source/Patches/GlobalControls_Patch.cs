using Harmony;
using RimWorld;
using Toggles.Source;

namespace Toggles.Patches
{
    // Toggles the temperature readout on the HUD.
    [HarmonyPatch(typeof(GlobalControls))]
    [HarmonyPatch("TemperatureString")]
    class GlobalControls_Patch
    {
        internal GlobalControls_Patch() => InitToggles();

        static void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.InGameUI,
                    group: "HUD"
                    );
        }

        static string GetLabel() => "HUD_TemperatureReadout";

        static string Postfix(string __result)
        {
            return ToggleHandler.IsActive(GetLabel()) ? __result : string.Empty;
        }
    }
}