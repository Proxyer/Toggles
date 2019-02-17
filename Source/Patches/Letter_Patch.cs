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

        internal override void InitToggles()
        {
            foreach (LetterDef letter in DefDatabase<LetterDef>.AllDefsListForReading)
                ToggleFactory.Add(
                    label: letter.defName,
                    root: "InGameUI",
                    group: "Letters",
                    patch: "Letter_Patch"
                    );
        }

        static void Postfix(ref Letter __instance, ref bool __result)
        {
            __result = ToggleHandler.IsActive(__instance.def.defName) ? __result : false;
        }
    }
}