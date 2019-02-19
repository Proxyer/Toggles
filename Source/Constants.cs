using UnityEngine;
using Verse;

namespace Toggles
{
    [StaticConstructorOnStartup]
    internal static class Constants
    {
        internal static string ModName { get; } = "Toggles";

        internal static Texture2D TexEmpty
        {
            get
            {
                if (texEmpty.NullOrBad())
                    texEmpty = SolidColorMaterials.NewSolidColorTexture(new Color(0, 0, 0, 0));

                return texEmpty;
            }
        }

        static Texture2D texEmpty;
    }
}