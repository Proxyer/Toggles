using Harmony;
using Toggles.Source;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(Widgets))]
    [HarmonyPatch("Label")]
    [HarmonyPatch(new[] { typeof(Rect), typeof(string) })]
    class Widgets_Patch
    {
        internal Widgets_Patch() => InitToggles();

        static void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.StartScreen,
                    group: ButtonCat.MiscellaneousEntry
                    );
        }

        static string GetLabel() => ButtonCat.MiscellaneousEntry + "_MainPageCredit";

        static bool Prefix(string label)
        {
            return label.Equals("MainPageCredit".Translate()) ? ToggleHandler.IsActive(GetLabel()) : true;
        }
    }
}