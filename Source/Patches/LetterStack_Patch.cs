using Harmony;
using System.Collections.Generic;
using Verse;

namespace Toggles.Patches
{
    // Work in progress
    [HarmonyPatch(typeof(LetterStack))]
    [HarmonyPatch("ReceiveLetter")]
    [HarmonyPatch(new[] { typeof(Letter), typeof(string) })]
    class LetterStack_Patch
    {
        public static List<Letter> LoggedLetters { get; set; } = new List<Letter>();

        static void Postfix(Letter let, string debugInfo = null)
        {
            DebugUtil.Log("Label " + let.label);
            LoggedLetters.Add(let);
        }
    }
}