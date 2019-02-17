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

        internal override void InitToggles()
        {
            ToggleFactory.Add(
                    label: Label,
                    root: "InGameUI",
                    group: "HUD",
                    patch: "ResourceReadout_Patch"
                    );
        }

        static string Label { get; } = "ResourceReadout";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label);
        }
    }
}