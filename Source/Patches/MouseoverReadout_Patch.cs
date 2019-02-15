using Harmony;
using Verse;

namespace Toggles.Patches
{
    internal class MouseoverReadout_Patch : Patch
    {
        internal MouseoverReadout_Patch() : base(
            patchType: typeof(MouseoverReadout_Patch),
            originType: typeof(MouseoverReadout),
            originMethod: "MouseoverReadoutOnGUI"
            )
        { }

        static string Label { get; } = "MouseoverReadout";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label);
        }
    }
}