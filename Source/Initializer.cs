using RimWorld;
using Toggles.Patches;
using Verse;

namespace Toggles
{
    internal static class Initializer
    {
        public static int Tmp { get; set; } = -1;

        internal static void OnDefsLoaded()
        {
            Tmp = DefDatabase<IncidentDef>.AllDefsListForReading.Count;
        }
    }
}