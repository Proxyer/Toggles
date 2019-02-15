using Harmony;
using UnityEngine;
using UnityEngine.SceneManagement;
using Verse;

namespace Toggles.Patches
{
    internal class ListableOption_WebLink_Patch : Patch
    {
        internal ListableOption_WebLink_Patch() : base(
            patchType: typeof(ListableOption_WebLink_Patch),
            originType: typeof(ListableOption_WebLink),
            originMethod: "DrawOption",
            paramTypes: new[] { typeof(Vector2), typeof(float) }
            )
        { }

        static bool Prefix(ListableOption_WebLink __instance)
        {
            string label = StringUtil.Conform(__instance.label) + SceneManager.GetActiveScene().name;
            return ToggleHandler.IsActive(label);
        }
    }
}