using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Verse;

namespace Toggles.Patches
{
    internal class ListableOption_WebLink_Patch : Patch
    {
        internal ListableOption_WebLink_Patch() : base(
            patchType: typeof(ListableOption_WebLink_Patch),
            originType: typeof(ListableOption_WebLink),
            originMethod: "DrawOption",
            paramTypes: new[] { typeof(Vector2), typeof(float) }
            )
        { }

        internal override void InitToggles()
        {
            foreach (string link in AllLinks)
                ToggleFactory.Add(
                        label: link,
                        root: link.EndsWith("Entry") ? "StartScreenUI" : "InGameUI",
                        group: link.EndsWith("Entry") ? "MainMenuLinks" : "PauseMenu",
                        patch: "DateReadout_Patch"
                        );
        }

        static List<string> AllLinks = new List<string>
        {
            { "BuySoundtrackEntry" },
            { "TynansDesignBookEntry" },
            { "FictionPrimerEntry" },
            { "ForumsEntry" },
            { "LudeonBlogEntry" },
            { "OfficialWikiEntry" },
            { "HelpTranslateRimWorldEntry" },
            { "TynansTwitterEntry" },

            { "BuySoundtrackPlay" },
            { "TynansDesignBookPlay" },
            { "FictionPrimerPlay" },
            { "ForumsPlay" },
            { "LudeonBlogPlay" },
            { "OfficialWikiPlay" },
            { "HelpTranslateRimWorldPlay" },
            { "TynansTwitterPlay" }
        };

        static bool Prefix(ListableOption_WebLink __instance)
        {
            string label = StringUtil.Conform(__instance.label) + SceneManager.GetActiveScene().name;
            return ToggleHandler.IsActive(label);
        }
    }
}