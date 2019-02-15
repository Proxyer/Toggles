using Harmony;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    [StaticConstructorOnStartup]
    internal class GUI_Patch : Patch
    {
        internal GUI_Patch() : base(
            patchType: typeof(GUI_Patch),
            originType: typeof(GUI),
            originMethod: "DrawTexture",
            paramTypes: new[] { typeof(Rect), typeof(Texture), typeof(ScaleMode), typeof(bool) }
            )
        { }

        static string Label { get; } = "TimeControls";
        static string StringPrefix { get; } = "TimeSpeedButton_";

        static Texture2D TexEmpty;

        static List<string> texList = new List<string>
        {
            "GameTitle",
            "LudeonLogoSmall",
            "LangIcon"
        };

        static bool Prefix(Rect position, ref Texture image)
        {
            if (TexEmpty.NullOrBad())
                TexEmpty = SolidColorMaterials.NewSolidColorTexture(new Color(0, 0, 0, 0));

            // Start screen elements. Blocks original method if toggle is inactive.
            if (Current.ProgramState == ProgramState.Entry)
            {
                if (texList.Contains(image.name) && !ToggleHandler.IsActive(image.name + GenScene.EntrySceneName))
                    image = TexEmpty;
            }
            else
            {
                // Time Controls
                if (!ToggleHandler.IsActive(Label))
                    if ((image == TexUI.HighlightTex) && position.width == TimeControls.TimeButSize.x || image.name.StartsWith(StringPrefix))
                        image = TexEmpty;
            }

            return true;
        }
    }
}