using Harmony;
using Toggles.Source;
using Verse;

namespace Toggles.Patches
{
    // Toggles the mouseover readout on the HUD.
    [HarmonyPatch(typeof(MouseoverReadout))]
    [HarmonyPatch("MouseoverReadoutOnGUI")]
    class MouseoverReadout_Patch
    {
        internal MouseoverReadout_Patch() =>
            ToggleManager.Add(
                label: Label,
                root: ButtonCat.PlayScreen,
                group: ButtonCat.HUD
                );

        static string Label { get; } = $"{ButtonCat.HUD}_MouseoverReadout";

        // Stops the readout from being drawn if setting is inactive.
        static bool Prefix() => ToggleManager.IsActive(Label);
    }
}