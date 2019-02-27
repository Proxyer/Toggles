using Harmony;
using RimWorld;

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
                group: ButtonCat.Misc
                );

        static string Label => $"{ButtonCat.Misc}_ColonistBar";

        // Adds vanilla toggle for Colonist Bar to the mod.
        static void Postfix(ref bool __result) => __result = ToggleManager.IsActive(Label);
    }
}