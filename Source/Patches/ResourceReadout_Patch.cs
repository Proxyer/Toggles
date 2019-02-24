using Harmony;
using RimWorld;
using Toggles.Source;

namespace Toggles.Patches
{
    // Toggles the resource readout on the HUD.
    [HarmonyPatch(typeof(ResourceReadout))]
    [HarmonyPatch("ResourceReadoutOnGUI")]
    class ResourceReadout_Patch
    {
        internal ResourceReadout_Patch() =>
            ToggleManager.Add(
                label: Format(),
                root: ButtonCat.PlayScreen,
                group: ButtonCat.HUD
                );

        static string Format() => $"{ButtonCat.HUD}_ResourceReadout";

        // Stops the resource readout from being drawn if setting is inactive.
        static bool Prefix() => ToggleManager.IsActive(Format());
    }
}