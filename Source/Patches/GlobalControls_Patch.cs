using RimWorld;

namespace Toggles.Patches
{
    // Toggles the temperature readout on the HUD.
    internal class GlobalControls_Patch : Patch
    {
        internal GlobalControls_Patch() : base(
            patchType: typeof(GlobalControls_Patch),
            originType: typeof(GlobalControls),
            originMethod: "TemperatureString"
            )
        { }

        internal override void InitToggles()
        {
            ToggleFactory.Add(
                    label: Label,
                    root: "InGameUI",
                    group: "HUD",
                    patch: "GlobalControls_Patch"
                    );
        }

        static string Label { get; } = "TemperatureReadout";

        static string Postfix(string __result)
        {
            return ToggleHandler.IsActive(Label) ? __result : string.Empty;
        }
    }
}