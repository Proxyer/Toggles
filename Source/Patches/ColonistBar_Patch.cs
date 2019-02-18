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
        internal ColonistBar_Patch() => InitToggles();

        static void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.InGameUI,
                    group: "HUD"
                    );
        }

        static string GetLabel() => "HUD_ColonistBar";

        // Adds vanilla toggle for Colonist Bar to the mod.
        public static void Postfix(ref bool __result)
        {
            __result = ToggleHandler.IsActive(GetLabel());
        }
    }
}