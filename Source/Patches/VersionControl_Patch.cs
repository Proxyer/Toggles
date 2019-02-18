using Harmony;
using RimWorld;
using Toggles.Source;
using Verse;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(VersionControl))]
    [HarmonyPatch("DrawInfoInCorner")]
    class VersionControl_Patch
    {
        internal VersionControl_Patch() => InitToggles();

        void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(Label, ProgramState.Entry),
                    root: ButtonCat.Entry,
                    group: ButtonCat.StartScreenUI
                    );

            ToggleFactory.Add(
                    label: GetLabel(Label, ProgramState.Playing),
                    root: ButtonCat.Play,
                    group: ButtonCat.PauseMenu
                    );
        }

        static string GetLabel(string label, ProgramState state)
        {
            string preLabel = string.Empty;
            if (state == ProgramState.Entry)
                preLabel = ButtonCat.StartScreenUI;
            else if (state == ProgramState.Playing)
                preLabel = ButtonCat.PauseMenu;

            return $"{preLabel}_{label}";
        }

        static string Label { get; } = "VersionControl";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(GetLabel(Label, Current.ProgramState));
        }
    }
}