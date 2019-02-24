﻿using Harmony;
using RimWorld;
using Toggles.Source;
using Verse;

namespace Toggles.Patches
{
    // Toggles the version control readout on the start screen and pause menu.
    [HarmonyPatch(typeof(VersionControl))]
    [HarmonyPatch("DrawInfoInCorner")]
    class VersionControl_Patch
    {
        internal VersionControl_Patch()
        {
            ToggleManager.Add(
                    label: Format(Label, ProgramState.Entry),
                    root: ButtonCat.StartScreen,
                    group: ButtonCat.MiscellaneousEntry
                    );

            ToggleManager.Add(
                    label: Format(Label, ProgramState.Playing),
                    root: ButtonCat.PauseScreen,
                    group: ButtonCat.MiscellaneousPlay
                    );
        }

        static string Format(string label, ProgramState state)
        {
            string preLabel = string.Empty;
            if (state == ProgramState.Entry)
                preLabel = ButtonCat.MiscellaneousEntry;
            else if (state == ProgramState.Playing)
                preLabel = ButtonCat.MiscellaneousPlay;

            return $"{preLabel}_{label}";
        }

        static string Label { get; } = "VersionControl";

        // Stops the version control readout from being drawn if setting is inactive.
        static bool Prefix() => ToggleManager.IsActive(Format(Label, Current.ProgramState));
    }
}