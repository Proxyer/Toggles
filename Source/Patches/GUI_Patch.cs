using Harmony;
using System.Collections.Generic;
using Toggles.Source;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(GUI))]
    [HarmonyPatch("DrawTexture")]
    [HarmonyPatch(new[] { typeof(Rect), typeof(Texture), typeof(ScaleMode), typeof(bool) })]
    class GUI_Patch
    {
        internal GUI_Patch() => InitToggles();

        static void InitToggles()
        {
            foreach (string element in Elements)
                ToggleFactory.Add(
                        label: GetLabel(element),
                        root: ButtonCat.StartScreen,
                        group: ButtonCat.MiscellaneousEntry
                        );
        }

        static List<string> Elements { get; } = new List<string>
        {
            "GameTitle",
            "LudeonLogoSmall",
            "LangIcon"
        };

        static string GetLabel(string input)
        {
            return ButtonCat.MiscellaneousEntry + "_" + input;
        }

        static bool Prefix(Rect position, ref Texture image)
        {
            if (Current.ProgramState == ProgramState.Entry)
                if (Elements.Contains(image.name))
                    if (!ToggleHandler.IsActive(GetLabel(image.name)))
                        image = Constants.TexEmpty;

            return true;
        }
    }
}