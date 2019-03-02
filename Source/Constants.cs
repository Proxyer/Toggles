using UnityEngine;
using Verse;

namespace Toggles
{
    [StaticConstructorOnStartup]
    internal static class Constants
    {
        internal static string ModName { get; } = "Toggles";

        internal static Texture2D TexEmpty { get; private set; }

        internal static void InitTextures() =>
            TexEmpty = SolidColorMaterials.NewSolidColorTexture(new Color(0, 0, 0, 0));
    }
}