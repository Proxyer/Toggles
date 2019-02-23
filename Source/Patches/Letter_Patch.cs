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

        internal static List<string> customLetters = new List<string>();

        internal static List<string> CustomLetters { get => customLetters; set => customLetters = value; }

        static string GetLabel(string letter) => ButtonCat.Letters + "_" + letter;

        static string GetRawLabel(string letter) => ButtonCat.Letters + "_" + letter.Replace(" ", "");

        internal static void LogToToggle(string letter)
        {
            if (!ToggleHandler.Toggles.Exists(x => x.rawLabel.Equals(letter)))
            {
                LetterToToggle(GetLabel(letter), letter);
                CustomLetters.Add(letter);
                ToggleHandler.MakeLookUp();
            }
        }

        static void InitToggles()
        {
            foreach (LetterDef letter in DefDatabase<LetterDef>.AllDefsListForReading)
                LetterToToggle(GetLabel(letter.defName));
            foreach (string letter in CustomLetters)
                LetterToToggle(GetLabel(letter), letter);
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
            string defLabel = __instance.def.defName;
            DebugUtil.Log("QWEQWE " + GetLabel(__instance.def.defName));

            if (ToggleHandler.Toggles.Exists(x => x.Label.Equals(GetLabel(label))))
            {
                __result = ToggleHandler.IsActive(GetLabel(label)) ? __result : false;
            }
            else if (ToggleHandler.Toggles.Exists(x => x.Label.Equals(GetLabel(defLabel))))
            {
                __result = ToggleHandler.IsActive(GetLabel(__instance.def.defName)) ? __result : false;
            }
        }
    }
}