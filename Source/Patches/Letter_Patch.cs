using Harmony;
using System.Collections.Generic;
using System.Linq;
using Toggles.Source;
using Verse;

namespace Toggles.Patches
{
    // Toggles all letter on the HUD.
    [HarmonyPatch(typeof(Letter))]
    [HarmonyPatch("CanShowInLetterStack", MethodType.Getter)]
    class Letter_Patch
    {
        internal Letter_Patch()
        {
            InitToggles();
            //customLetter = new LetterManager();
        }

        static LetterManager customLetter;

        internal static List<string> customLetters = new List<string>();

        internal static List<string> CustomLetters { get => customLetters; set => customLetters = value; }

        static string GetDefLabel(string letter) => ButtonCat.Letters + "_" + letter;

        static string GetCustomLabel(string letter)
        {
            return ButtonCat.Letters + "_" + StringUtil.Conform(letter);
        }

        internal static void LogToToggle(string letter)
        {
            CustomLetters.Add(letter);
            LetterToToggle(GetCustomLabel(letter));
        }

        internal static void UpdateCustomLetters()
        {
            CustomLetters
                .Where(letter => !ToggleHandler.Exists(GetCustomLabel(letter))).ToList()
                .ForEach(letter => LetterToToggle(GetCustomLabel(letter)));
        }

        static void InitToggles()
        {
            foreach (LetterDef letter in DefDatabase<LetterDef>.AllDefsListForReading)
                LetterToToggle(GetDefLabel(letter.defName));
        }

        static void LetterToToggle(string letter)
        {
            ToggleFactory.Add(
                    label: letter,
                    root: ButtonCat.Events,
                    group: ButtonCat.Letters
                    );
        }

        static void Postfix(ref Letter __instance, ref bool __result)
        {
            string label = __instance.label;
            string defLabel = __instance.def.defName;

            if (ToggleHandler.Toggles.Exists(x => x.Label.Equals(GetCustomLabel(label))))
            {
                __result = ToggleHandler.IsActive(GetCustomLabel(label)) ? __result : false;
            }
            else if (ToggleHandler.Toggles.Exists(x => x.Label.Equals(GetDefLabel(defLabel))))
            {
                __result = ToggleHandler.IsActive(GetDefLabel(__instance.def.defName)) ? __result : false;
            }
        }

        internal static List<string> LoggedLetters =>
            Find.Archive.ArchivablesListForReading
                    .Where(x => x is Letter)
                    .Select(z => z.ArchivedLabel)
                    .Where(x => !ToggleHandler.Toggles.Exists(z => z.Label.Equals(GetCustomLabel(x))))
                    .ToList().ListFullCopy();
    }
}