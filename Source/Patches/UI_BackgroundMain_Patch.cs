using Harmony;
using RimWorld;

namespace Toggles.Patches
{
    internal class UI_BackgroundMain_Patch : Patch
    {
        internal UI_BackgroundMain_Patch() : base(
            patchType: typeof(UI_BackgroundMain_Patch),
            originType: typeof(UI_BackgroundMain),
            originMethod: "BackgroundOnGUI"
            )
        { }

        static string Label { get; } = "Background";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label);
        }
    }
}