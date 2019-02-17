using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    internal class Widgets_Patch : Patch
    {
        internal Widgets_Patch() : base(
            patchType: typeof(Widgets_Patch),
            originType: typeof(Widgets),
            originMethod: "Label",
            paramTypes: new[] { typeof(Rect), typeof(string) }
            )
        { }

        static string Label { get; } = "MainPageCredit";

        static bool Prefix(string label)
        {
            return label.Equals(Label.Translate()) ? ToggleHandler.IsActive(Label + GenScene.EntrySceneName) : true;
        }
    }
}