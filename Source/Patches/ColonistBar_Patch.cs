using Harmony;
using RimWorld;
using Toggles.Source;

namespace Toggles.Patches
{
    // Toggles the Colonist bar.
    [HarmonyPatch(typeof(ColonistBar))]
    [HarmonyPatch("Visible", MethodType.Getter)]
    class ColonistBar_Patch
    {
        internal ColonistBar_Patch() =>
            ToggleManager.Add(
                label: Label,
                root: ButtonCat.PlayScreen,
                group: ButtonCat.HUD
                );

        static string Label => $"{ButtonCat.HUD}_ColonistBar";

        // Adds vanilla toggle for Colonist Bar to the mod.
        static void Postfix(ref bool __result) => __result = ToggleManager.IsActive(Label);
    }
}