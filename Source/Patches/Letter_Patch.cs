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

        internal static List<string> LoggedLetters { get; } = new List<string>();

        static string GetLabel(string letter) => ButtonCat.Letters + "_" + letter;

        internal static void LogToToggle(string letter)
        {
            LoggedLetters.RemoveAll(x => x.Equals(letter));
            LetterToToggle(GetLabel(letter), letter);
            ToggleHandler.MakeLookUp();
        }

        static void InitToggles()
        {
            foreach (LetterDef letter in DefDatabase<LetterDef>.AllDefsListForReading)
                LetterToToggle(GetLabel(letter.defName));
        }

        static void LetterToToggle(string letter, string rawLabel = "")
        {
            ToggleFactory.Add(
                    label: letter,
                    rawLabel: rawLabel,
                    root: ButtonCat.Events,
                    group: ButtonCat.Letters
                    );
        }

        static void Postfix(ref Letter __instance, ref bool __result)
        {
            string label = __instance.label;
            if (!LoggedLetters.Contains(label) && !ToggleHandler.Toggles.Exists(x => x.Label.Equals(GetLabel(label))))
                LoggedLetters.Add(label);

            //__result = ToggleHandler.IsActive(GetLabel(__instance.def.defName)) ? __result : false;
        }
    }
}