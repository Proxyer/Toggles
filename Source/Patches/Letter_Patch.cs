using Verse;

namespace Toggles.Patches
{
    // Toggles all letter on the HUD.
    internal class Letter_Patch : Patch
    {
        internal Letter_Patch() : base(
            patchType: typeof(Letter_Patch),
            originType: typeof(Letter),
            originMethod: "get_CanShowInLetterStack"
            )
        { }

        static string Label { get; } = "Letter";

        static void Postfix(ref Letter __instance, ref bool __result)
        {
            if (!ToggleHandler.IsActive(Label + __instance.def.defName))
                __result = false;
        }
    }
}