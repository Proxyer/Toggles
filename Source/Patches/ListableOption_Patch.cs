using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Verse;

namespace Toggles.Patches
{
    internal class ListableOption_Patch : Patch
    {
        internal ListableOption_Patch() : base(
            patchType: typeof(ListableOption_Patch),
            originType: typeof(ListableOption),
            originMethod: "DrawOption",
            paramTypes: new[] { typeof(Vector2), typeof(float) }
            )
        { }

        internal override void InitToggles()
        {
            foreach (string button in AllButtons)
                ToggleFactory.Add(
                        label: button,
                        root: button.EndsWith("Entry") ? "StartScreenUI" : "InGameUI",
                        group: button.EndsWith("Entry") ? "MainMenuButtons" : "PauseMenu",
                        patch: "ListableOption_Patch"
                        );
        }

        static List<string> AllButtons = new List<string>
        {
            { "TutorialEntry" },
            { "NewColonyEntry" },
            { "LoadGameEntry" },
            { "OptionsEntry" },
            { "SaveTranslationReportEntry" },
            { "ModsEntry" },
            { "CreditsEntry" },
            { "QuitToOSEntry" },

            { "OptionsPlay" },
            { "QuitToMainMenuPlay" },
            { "QuitToOSPlay" },
            { "ReviewScenarioPlay" },
            { "SavePlay" }
        };

        static bool Prefix(ListableOption __instance)
        {
            string label = StringUtil.Conform(__instance.label) + SceneManager.GetActiveScene().name;
            return ToggleHandler.IsActive(label);
        }
    }
}