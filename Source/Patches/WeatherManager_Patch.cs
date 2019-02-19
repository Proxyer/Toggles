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

        static void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.PlayScreen,
                    group: ButtonCat.HUD
                    );
        }

        static string GetLabel() => ButtonCat.HUD + "_Weather";

        static bool Prefix()
        {
            return ToggleHandler.IsActive(GetLabel());
        }
    }
}