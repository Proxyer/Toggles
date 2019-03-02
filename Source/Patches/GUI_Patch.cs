using Harmony;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    // Toggles various graphical elements on the start screen.
    [HarmonyPatch(typeof(GUI))]
    [HarmonyPatch("DrawTexture")]
    [HarmonyPatch(new[] { typeof(Rect), typeof(Texture), typeof(ScaleMode), typeof(bool) })]
    class GUI_Patch
    {
        internal GUI_Patch() =>
            Elements
                .ForEach(element =>
                    ToggleManager.Add(
                        label: Format(element),
                        root: ButtonCat.StartScreen,
                        group: ButtonCat.MiscEntry
                        ));

        static List<string> Elements { get; } = new List<string>
        {
            "GameTitle",
            "LudeonLogoSmall",
            "LangIcon"
        };

        static string Format(string input) => $"{ButtonCat.MiscEntry}_{input}";

        // Replaces the texture of images with empty if the corresponding setting is inactive.
        static bool Prefix(Rect position, ref Texture image)
        {
            if (Current.ProgramState == ProgramState.Entry)
                if (Elements.Contains(image.name))
                    if (!ToggleManager.IsActive(Format(image.name)))
                        image = Constants.TexEmpty;

            return true;
        }
    }
}