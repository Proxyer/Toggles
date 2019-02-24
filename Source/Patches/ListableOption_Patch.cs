using Harmony;
using System.Collections.Generic;
using Toggles.Source;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    // Toggles buttons from being shown on the start screen and pause menu.
    [HarmonyPatch(typeof(ListableOption))]
    [HarmonyPatch("DrawOption")]
    [HarmonyPatch(new[] { typeof(Vector2), typeof(float) })]
    class ListableOption_Patch
    {
        internal ListableOption_Patch()
        {
            EntryButtons
                .ForEach(button => ToggleManager.Add(
                    label: Format(button, ProgramState.Entry),
                    root: ButtonCat.StartScreen,
                    group: ButtonCat.ButtonsEntry
                    ));

            PlayButtons
                .ForEach(button => ToggleManager.Add(
                    label: Format(button, ProgramState.Playing),
                    root: ButtonCat.PauseScreen,
                    group: ButtonCat.ButtonsPlay
                    ));
        }

        static List<string> EntryButtons { get; } = new List<string>
        {
            { "Tutorial" },
            { "NewColony" },
            { "LoadGame" },
            { "Options" },
            { "SaveTranslationReport" },
            { "Mods" },
            { "Credits" },
            { "QuitToOS" }
        };

        static List<string> PlayButtons { get; } = new List<string>
        {
            { "Options" },
            { "QuitToMainMenu" },
            { "QuitToOS" },
            { "ReviewScenario" },
            { "Save" },
            { "LoadGame" }
        };

        static string Format(string option, ProgramState state)
        {
            string preLabel = string.Empty;
            if (state == ProgramState.Entry)
                preLabel = ButtonCat.ButtonsEntry;
            else if (state == ProgramState.Playing)
                preLabel = ButtonCat.ButtonsPlay;

            return $"{preLabel}_{StringUtil.Sanitize(option)}";
        }

        // Stops the button from being drawn if setting is inactive.
        static bool Prefix(ListableOption __instance) => ToggleManager.IsActive(Format(__instance.label, Current.ProgramState));
    }
}