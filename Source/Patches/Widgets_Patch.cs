using Harmony;
using Toggles.Source;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    // Toggles the credits label on the start screen.
    [HarmonyPatch(typeof(Widgets))]
    [HarmonyPatch("Label")]
    [HarmonyPatch(new[] { typeof(Rect), typeof(string) })]
    class Widgets_Patch
    {
        internal Widgets_Patch() =>
            ToggleManager.Add(
                label: Label,
                root: ButtonCat.StartScreen,
                group: ButtonCat.MiscellaneousEntry
                );

        static string Label => $"{ButtonCat.MiscellaneousEntry}_MainPageCredit";

        // Stops the credits label from being drawn if setting is inactive.
        static bool Prefix(string label) => label.Equals("MainPageCredit".Translate()) ? ToggleManager.IsActive(Label) : true;
    }
}