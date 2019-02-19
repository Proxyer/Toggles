using Harmony;
using System.Collections.Generic;
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

        internal static List<string> LoggedLetters { get; } = new List<string>();

        static void Postfix(ref Letter __instance, ref bool __result)
        {
            if (!LoggedLetters.Contains(__instance.label))
            {
                Log.Message(__instance.label);
                LoggedLetters.Add(__instance.label);
            }
            //__result = ToggleHandler.IsActive(GetLabel(__instance.def)) ? __result : false;
        }
    }
}