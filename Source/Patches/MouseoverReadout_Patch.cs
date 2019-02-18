using Harmony;
using Toggles.Source;
using Verse;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(MouseoverReadout))]
    [HarmonyPatch("MouseoverReadoutOnGUI")]
    class MouseoverReadout_Patch
    {
        internal MouseoverReadout_Patch() => InitToggles();

        void InitToggles()
        {
            ToggleFactory.Add(
                    label: Label,
                    root: ButtonCat.Play,
                    group: ButtonCat.InGameUI
                    );
        }

        static string Label { get; } = $"{ButtonCat.InGameUI}_MouseoverReadout";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label);
        }
    }
}