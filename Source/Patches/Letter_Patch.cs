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

        void InitToggles()
        {
            foreach (LetterDef letter in DefDatabase<LetterDef>.AllDefsListForReading)
                ToggleFactory.Add(
                    label: GetLabel(letter),
                    root: ButtonCat.Play,
                    group: "Letters"
                    );
        }

        static string GetLabel(LetterDef letter) => "Letter_" + letter.defName;

        static void Postfix(ref Letter __instance, ref bool __result)
        {
            __result = ToggleHandler.IsActive(GetLabel(__instance.def)) ? __result : false;
        }
    }
}