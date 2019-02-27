using Harmony;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    // Toggles links from being shown on the start screen and pause menu.
    [HarmonyPatch(typeof(ListableOption_WebLink))]
    [HarmonyPatch("DrawOption")]
    [HarmonyPatch(new[] { typeof(Vector2), typeof(float) })]
    class ListableOption_WebLink_Patch
    {
        internal ListableOption_WebLink_Patch()
        {
            EntryLinks
                .ForEach(link => ToggleManager.Add(
                    label: Format(link, ProgramState.Entry),
                    root: ButtonCat.StartScreen,
                    group: ButtonCat.LinksEntry
                    ));

            PlayLinks
                .ForEach(link => ToggleManager.Add(
                    label: Format(link, ProgramState.Playing),
                    root: ButtonCat.PauseScreen,
                    group: ButtonCat.LinksPlay
                    ));
        }

        static List<string> EntryLinks { get; } = new List<string>
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

        static List<string> PlayLinks { get; } = new List<string>
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

        static string Format(string option, ProgramState state)
        {
            string preLabel = string.Empty;
            if (state == ProgramState.Entry)
                preLabel = ButtonCat.LinksEntry;
            else if (state == ProgramState.Playing)
                preLabel = ButtonCat.LinksPlay;

            return $"{preLabel}_{StringUtil.Sanitize(option)}";
        }

        // Stops the link from being drawn if setting is inactive.
        static bool Prefix(ListableOption_WebLink __instance) => ToggleManager.IsActive(Format(__instance.label, Current.ProgramState));
    }
}