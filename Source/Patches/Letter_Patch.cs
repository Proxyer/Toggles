using Harmony;
using Toggles.Source;
using Verse;

namespace Toggles.Patches
{
    // Toggles all letter on the HUD.
    [HarmonyPatch(typeof(Letter))]
    [HarmonyPatch("CanShowInLetterStack", MethodType.Getter)]
    class Letter_Patch
    {
        internal Letter_Patch() => InitToggles();

        static void InitToggles()
        {
            foreach (LetterDef letter in DefDatabase<LetterDef>.AllDefsListForReading)
                ToggleFactory.Add(
                    label: GetLabel(letter),
                    root: ButtonCat.Events,
                    group: ButtonCat.Letters
                    );
        }

        static string GetLabel(LetterDef letter) => ButtonCat.Letters + "_" + letter.defName;

        static void Postfix(ref Letter __instance, ref bool __result)
        {
            __result = ToggleHandler.IsActive(GetLabel(__instance.def)) ? __result : false;
        }
    }
}