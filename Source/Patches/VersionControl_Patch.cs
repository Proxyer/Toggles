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

        static void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(Label, ProgramState.Entry),
                    root: ButtonCat.StartScreen,
                    group: ButtonCat.MiscellaneousEntry
                    );

            ToggleFactory.Add(
                    label: GetLabel(Label, ProgramState.Playing),
                    root: ButtonCat.PauseScreen,
                    group: ButtonCat.MiscellaneousPlay
                    );
        }

        static string GetLabel(string label, ProgramState state)
        {
            string preLabel = string.Empty;
            if (state == ProgramState.Entry)
                preLabel = ButtonCat.MiscellaneousEntry;
            else if (state == ProgramState.Playing)
                preLabel = ButtonCat.MiscellaneousPlay;

            return $"{preLabel}_{label}";
        }

        static string Label { get; } = "VersionControl";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(GetLabel(Label, Current.ProgramState));
        }
    }
}