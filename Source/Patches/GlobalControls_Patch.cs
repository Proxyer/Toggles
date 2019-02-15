using Harmony;
using RimWorld;

namespace Toggles.Patches
{
    internal class GlobalControls_Patch : Patch
    {
        internal GlobalControls_Patch() : base(
            patchType: typeof(GlobalControls_Patch),
            originType: typeof(GlobalControls),
            originMethod: "TemperatureString"
            )
        { }

        static string Label { get; } = "TemperatureReadout";

        static string Postfix(string __result)
        {
            return ToggleHandler.IsActive(Label) ? __result : string.Empty;
        }
    }
}