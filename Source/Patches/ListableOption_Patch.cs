using Harmony;
using UnityEngine;
using UnityEngine.SceneManagement;
using Verse;

namespace Toggles.Patches
{
    internal class ListableOption_Patch : Patch
    {
        internal ListableOption_Patch() : base(
            patchType: typeof(ListableOption_Patch),
            originType: typeof(ListableOption),
            originMethod: "DrawOption",
            paramTypes: new[] { typeof(Vector2), typeof(float) }
            )
        { }

        static bool Prefix(ListableOption __instance)
        {
            string label = StringUtil.Conform(__instance.label) + SceneManager.GetActiveScene().name;
            return ToggleHandler.IsActive(label);
        }
    }
}