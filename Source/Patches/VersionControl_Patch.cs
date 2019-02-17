using RimWorld;
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

        static string Label { get; } = "VersionControl";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label + SceneManager.GetActiveScene().name);
        }
    }
}