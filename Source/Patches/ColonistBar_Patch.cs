using Harmony;
using RimWorld;

namespace Toggles.Patches
{
    // Toggles the Colonist bar.
    internal class ColonistBar_Patch : Patch
    {
        internal ColonistBar_Patch() : base(
            patchType: typeof(ColonistBar_Patch),
            originType: typeof(ColonistBar),
            originMethod: "get_Visible"
            )
        { }

        static string Label { get; } = "ColonistBar";

        // Adds vanilla toggle for Colonist Bar to the mod.
        static void Postfix(ref bool __result)
        {
            __result = ToggleHandler.IsActive(Label);
        }
    }
}