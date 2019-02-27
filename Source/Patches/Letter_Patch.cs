using Harmony;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Toggles.Patches
{
    // Toggles all letter on the HUD.
    [HarmonyPatch(typeof(Letter))]
    [HarmonyPatch("CanShowInLetterStack", MethodType.Getter)]
    class Letter_Patch
    {
        internal Letter_Patch() =>
            DefDatabase<LetterDef>.AllDefsListForReading
                .ForEach(letterDef =>
                    LetterToToggle(Format(letterDef.defName)));

        // Holds all letters added by player
        internal static List<string> customLetters;

        internal static List<string> CustomLetters
        {
            get => customLetters ?? (CustomLetters = new List<string>());
            set => customLetters = value;
        }

        // Remove all toggles based on custom letters.
        internal static void RemoveCustomLetters() =>
            CustomLetters
                .ForEach(x => ToggleManager.Remove(FormatLabel(x)));

        // Adjusts strings to mod formats.
        static string Format(string letter) => $"{ButtonCat.Letters}_{letter}";
        static string FormatLabel(string letter) => Format(StringUtil.Sanitize(letter));

        // Creates toggle from unmodified letter label.
        internal static void AddRawLetter(string letter)
        {
            CustomLetters.Add(letter);
            LetterToToggle(FormatLabel(letter));
        }

        // Makes sure all player added letters exist as toggles.
        internal static void UpdateCustomLetters() =>
            CustomLetters
            .Where(letter => !ToggleManager.Exists(FormatLabel(letter))).ToList()
            .ForEach(letter => LetterToToggle(FormatLabel(letter)));

        // Creates a toggle from a sanitized letter label.
        static void LetterToToggle(string letter) =>
            ToggleManager.Add(
                label: letter,
                root: ButtonCat.Events,
                group: ButtonCat.Letters
                );

        // Replaces result if letter exists in settings. Prioritizes individual letters above letterDefs.
        static void Postfix(ref Letter __instance, ref bool __result)
        {
            string label = FormatLabel(__instance.label);
            string defLabel = Format(__instance.def.defName);

            if (ToggleManager.Exists(label))
                __result = ToggleManager.IsActive(label) ? __result : false;
            else if (ToggleManager.Exists(defLabel))
                __result = ToggleManager.IsActive(defLabel) ? __result : false;
        }

        // Returns list of raw letter labels from games' world history.
        internal static List<string> LoggedLetters =>
            Find.Archive.ArchivablesListForReading
                        .Where(x => x is Letter)
                        .Select(z => z.ArchivedLabel)
                        .Where(x => !ToggleManager.Exists(FormatLabel(x))).ToList();
    }
}