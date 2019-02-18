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

        void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.InGameUI,
                    group: "HUD"
                    );
        }

        static string GetLabel()
        {
            return "HUD_ResourceReadout";
        }

        static bool Prefix()
        {
            return ToggleHandler.IsActive(GetLabel());
        }
    }
}