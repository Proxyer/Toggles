using RimWorld;

namespace Toggles.Patches
{
    internal class ResourceReadout_Patch : Patch
    {
        internal ResourceReadout_Patch() : base(
            patchType: typeof(ResourceReadout_Patch),
            originType: typeof(ResourceReadout),
            originMethod: "ResourceReadoutOnGUI"
            )
        { }

        static string Label { get; } = "ResourceReadout";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label);
        }
    }
}