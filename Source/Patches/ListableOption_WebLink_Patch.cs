using Harmony;
using System.Collections.Generic;
using Toggles.Source;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(ListableOption_WebLink))]
    [HarmonyPatch("DrawOption")]
    [HarmonyPatch(new[] { typeof(Vector2), typeof(float) })]
    class ListableOption_WebLink_Patch
    {
        internal ListableOption_WebLink_Patch() => InitToggles();

        void InitToggles()
        {
            foreach (string link in EntryLinks)
                ToggleFactory.Add(
                        label: GetLabel(link, ProgramState.Entry),
                        root: ButtonCat.StartScreenUI,
                        group: ButtonCat.MainMenuButtons
                        );

            foreach (string link in PlayLinks)
                ToggleFactory.Add(
                        label: GetLabel(link, ProgramState.Playing),
                        root: ButtonCat.InGameUI,
                        group: ButtonCat.PauseMenu
                        );
        }

        List<string> EntryLinks { get; } = new List<string>
        {
            { "BuySoundtrack" },
            { "TynansDesignBook" },
            { "FictionPrimer" },
            { "Forums" },
            { "LudeonBlog" },
            { "OfficialWiki" },
            { "HelpTranslateRimWorld" },
            { "TynansTwitter" }
        };

        List<string> PlayLinks { get; } = new List<string>
        {
            { "BuySoundtrack" },
            { "TynansDesignBook" },
            { "FictionPrimer" },
            { "Forums" },
            { "LudeonBlog" },
            { "OfficialWiki" },
            { "HelpTranslateRimWorld" },
            { "TynansTwitter" }
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

        static bool Prefix(ListableOption_WebLink __instance)
        {
            return ToggleHandler.IsActive(GetLabel(__instance.label, Current.ProgramState));
        }
    }
}