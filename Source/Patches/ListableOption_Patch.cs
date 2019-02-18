using Harmony;
using System.Collections.Generic;
using Toggles.Source;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(ListableOption))]
    [HarmonyPatch("DrawOption")]
    [HarmonyPatch(new[] { typeof(Vector2), typeof(float) })]
    class ListableOption_Patch
    {
        internal ListableOption_Patch() => InitToggles();

        void InitToggles()
        {
            foreach (string button in EntryButtons)
                ToggleFactory.Add(
                        label: GetLabel(button, ProgramState.Entry),
                        root: ButtonCat.StartScreenUI,
                        group: ButtonCat.MainMenuButtons
                        );

            foreach (string button in PlayButtons)
                ToggleFactory.Add(
                        label: GetLabel(button, ProgramState.Playing),
                        root: ButtonCat.InGameUI,
                        group: ButtonCat.PauseMenu
                        );
        }

        List<string> EntryButtons { get; } = new List<string>
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

        List<string> PlayButtons { get; } = new List<string>
        {
            { "Options" },
            { "QuitToMainMenu" },
            { "QuitToOS" },
            { "ReviewScenario" },
            { "Save" },
            { "LoadGame" }
        };

        static string GetLabel(string option, ProgramState state)
        {
            string preLabel = string.Empty;
            if (state == ProgramState.Entry)
                preLabel = ButtonCat.StartScreenUI;
            else if (state == ProgramState.Playing)
                preLabel = ButtonCat.InGameUI;

            return $"{preLabel}_{StringUtil.Conform(option)}";
        }

        static bool Prefix(ListableOption __instance)
        {
            return ToggleHandler.IsActive(GetLabel(__instance.label, Current.ProgramState));
        }
    }
}