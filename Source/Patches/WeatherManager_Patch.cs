using Harmony;
using RimWorld;
using Toggles.Source;
using UnityEngine;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(WeatherManager))]
    [HarmonyPatch("DoWeatherGUI")]
    [HarmonyPatch(new[] { typeof(Rect) })]
    class WeatherManager_Patch
    {
        internal WeatherManager_Patch() => InitToggles();

        void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.InGameUI,
                    group: "HUD"
                    );
        }

        static string GetLabel() => "HUD_WeatherReadout";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(GetLabel());
        }
    }
}