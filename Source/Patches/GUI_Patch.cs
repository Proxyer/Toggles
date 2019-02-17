using System.Collections.Generic;
using System.Linq;
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

        internal override void InitToggles()
        {
            foreach (string element in Dict.Values)
                ToggleFactory.Add(
                        label: element,
                        root: "StartScreenUI",
                        group: "ElementsEntry",
                        patch: "GUI_Patch"
                        );
        }

        static Dictionary<string, string> Dict = new List<string>
        {
            "GameTitle",
            "LudeonLogoSmall",
            "LangIcon"
        }
        .ToDictionary(x => x, x => StringUtil.Pretty(x));

        static bool Prefix(Rect position, ref Texture image)
        {
            if (Current.ProgramState == ProgramState.Entry)
                if (Dict.ContainsKey(image.name))
                    if (!ToggleHandler.IsActive(Dict.TryGetValue(image.name)))
                        image = Constants.TexEmpty;

            return true;
        }
    }
}