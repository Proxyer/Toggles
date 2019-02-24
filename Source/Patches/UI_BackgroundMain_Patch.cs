﻿using Harmony;
using RimWorld;
using Toggles.Source;

namespace Toggles.Patches
{
    // Toggles the background image on the start screen.
    [HarmonyPatch(typeof(UI_BackgroundMain))]
    [HarmonyPatch("BackgroundOnGUI")]
    class UI_BackgroundMain_Patch
    {
        internal UI_BackgroundMain_Patch() =>
            ToggleManager.Add(
                label: Label,
                root: ButtonCat.StartScreen,
                group: ButtonCat.MiscellaneousEntry
                );

        static string Label => $"{ButtonCat.MiscellaneousEntry}_Background";

        // Stops the background image from being drawn if setting is inactive.
        static bool Prefix() => ToggleManager.IsActive(Label);
    }
}