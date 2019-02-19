using Harmony;
using RimWorld;
using Toggles.Source;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(ResourceReadout))]
    [HarmonyPatch("ResourceReadoutOnGUI")]
    class ResourceReadout_Patch
    {
        internal ResourceReadout_Patch() => InitToggles();

        static void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.PlayScreen,
                    group: ButtonCat.HUD
                    );
        }

        static string GetLabel()
        {
            return ButtonCat.HUD + "_ResourceReadout";
        }

        static bool Prefix()
        {
            return ToggleHandler.IsActive(GetLabel());
        }
    }
}