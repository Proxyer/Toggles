using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    internal class GUI_Patch : Patch
    {
        internal GUI_Patch() : base(
            patchType: typeof(GUI_Patch),
            originType: typeof(GUI),
            originMethod: "DrawTexture",
            paramTypes: new[] { typeof(Rect), typeof(Texture), typeof(ScaleMode), typeof(bool) }
            )
        { }

        static List<string> texList = new List<string>
        {
            "GameTitle",
            "LudeonLogoSmall",
            "LangIcon"
        };

        static bool Prefix(Rect position, ref Texture image)
        {
            if (Current.ProgramState == ProgramState.Entry)
                if (texList.Contains(image.name) && !ToggleHandler.IsActive(image.name + GenScene.EntrySceneName))
                    image = Constants.TexEmpty;

            return true;
        }
    }
}