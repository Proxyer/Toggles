using RimWorld;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Toggles.Patches
{
    internal class VersionControl_Patch : Patch
    {
        internal VersionControl_Patch() : base(
            patchType: typeof(VersionControl_Patch),
            originType: typeof(VersionControl),
            originMethod: "DrawInfoInCorner"
            )
        { }

        internal override void InitToggles()
        {
            foreach (string label in AllLabels)
                ToggleFactory.Add(
                        label: Label,
                        root: label.EndsWith("Entry") ? "StartScreenUI" : "InGameUI",
                        group: label.EndsWith("Entry") ? "ElementsEntry" : "PauseMenu",
                        patch: "VersionControl_Patch"
                        );
        }

        static List<string> AllLabels = new List<string>
        {
            { "VersionControlEntry" },
            { "VersionControlPlay" },
        };

        static string Label { get; } = "VersionControl";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label + SceneManager.GetActiveScene().name);
        }
    }
}