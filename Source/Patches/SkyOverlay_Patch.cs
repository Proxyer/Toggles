using Harmony;
using System;
using System.Collections.Generic;
using Verse;

namespace Toggles.Patches
{
    // Toggles overlays.
    [HarmonyPatch(typeof(SkyOverlay))]
    [HarmonyPatch("DrawOverlay")]
    [HarmonyPatch(new[] { typeof(Map) })]
    class SkyOverlay_Patch
    {
        internal SkyOverlay_Patch() =>
            new List<Type>(typeof(SkyOverlay).AllSubclasses())
                .ForEach(overlay => ToggleManager.Add(
                    label: Format(overlay),
                    root: ButtonCat.PlayScreen,
                    group: ButtonCat.Overlay
                    ));

        static string Format(Type overlay) =>
            overlay.Name;

        // Stops the different overlays, like fallout and fog, from being drawn if setting is inactive.
        static bool Prefix(ref SkyOverlay __instance) =>
            ToggleManager.IsActive(Format(__instance.GetType()));
    }
}