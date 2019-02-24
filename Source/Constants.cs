using UnityEngine;
using Verse;

namespace Toggles
{
    [StaticConstructorOnStartup]
    internal static class Constants
    {
        internal static string ModName { get; } = "Toggles";

        //internal static string ModID { get; } = $"net.krafs.{ModName.ToLower()}";
        internal static string ModID { get; } = "net.krafs.toggles";

        internal static Texture2D TexEmpty { get; private set; }

        internal static void InitTextures() => TexEmpty = SolidColorMaterials.NewSolidColorTexture(new Color(0, 0, 0, 0));
    }
}