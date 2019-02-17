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

        internal override void InitToggles()
        {
            ToggleFactory.Add(
                    label: Label,
                    root: "InGameUI",
                    group: "HUD",
                    patch: "MouseoverReadout_Patch"
                    );
        }

        static string Label { get; } = "Date";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label);
        }
    }
}