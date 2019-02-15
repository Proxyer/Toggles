using Harmony;
using RimWorld;
using UnityEngine;

namespace Toggles.Patches
{
    internal class WeatherManager_Patch : Patch
    {
        internal WeatherManager_Patch() : base(
            patchType: typeof(WeatherManager_Patch),
            originType: typeof(WeatherManager),
            originMethod: "DoWeatherGUI",
            paramTypes: new[] { typeof(Rect) }
            )
        { }

        static string Label { get; } = "WeatherReadout";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label);
        }
    }
}