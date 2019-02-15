using Harmony;
using Verse;

namespace Toggles.Patches
{
    // Toggles all letter on the HUD. NB! Does not apply to letters spawned by debug menu. (Can patch ReceiveLetter for that)
    internal class Letter_Patch : Patch
    {
        internal Letter_Patch() : base(
            patchType: typeof(Letter_Patch),
            originType: typeof(Letter),
            originMethod: "get_CanShowInLetterStack"
            )
        { }

        static string Label { get; } = "Letter";

        static bool Prefix(ref Letter __instance, ref bool __result)
        {
            foreach (LetterDef def in DefDatabase<LetterDef>.AllDefs)
                if (__instance.def == def && !ToggleHandler.IsActive(Label + def))
                    __result = false;

            return true;
        }
    }
}