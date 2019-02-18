using Harmony;
using RimWorld;
using Toggles.Source;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(UI_BackgroundMain))]
    [HarmonyPatch("BackgroundOnGUI")]
    class UI_BackgroundMain_Patch
    {
        internal UI_BackgroundMain_Patch() => InitToggles();

        void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.StartScreenUI,
                    group: "ElementsEntry"
                    );
        }

        static string GetLabel()
        {
            return "ElementsEntry_Background";
        }

        static bool Prefix()
        {
            return ToggleHandler.IsActive(GetLabel());
        }
    }
}