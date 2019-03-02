using Harmony;
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
                group: ButtonCat.MiscEntry
                );

        static string Label => $"{ButtonCat.MiscEntry}_MainPageCredit";

        // Stops the credits label from being drawn if setting is inactive.
        static bool Prefix(ref string label)
        {
            if (Current.ProgramState == ProgramState.Entry)
                if (label.Equals("MainPageCredit".Translate()))
                    if (!ToggleManager.IsActive(Label))
                        label = string.Empty;

            return true;
        }
    }
}