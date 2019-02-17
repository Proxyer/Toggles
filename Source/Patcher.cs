using Harmony;
using Verse;

namespace Toggles
{
    // Applies relevant patches to the game.
    [StaticConstructorOnStartup]
    internal static class Patcher
    {
        static HarmonyInstance Harmony { get; } = HarmonyInstance.Create(Constants.ModName);

        static Patcher() => DoPatches();

        internal static void DoPatches()
        {
            // DEBUG Patch All
            foreach (var patch in Constants.Patches)
                patch.Apply(Harmony);

            //if (DefDatabase<IncidentDef>.AllDefsListForReading.NullOrEmpty())
            //    Log.Message("Empty...");
            //else
            //    DefDatabase<IncidentDef>.AllDefsListForReading.ForEach(x => Log.Message(x.defName));

            // Apply patches required according to user settings.
            //foreach (var patch in Constants.Patches)
            //{
            //    if (ToggleHandler.Toggles
            //        .Where(x => x.Patch.Equals(patch.GetType().Name)).ToList()
            //        .Exists(x => x.active == false))
            //        patch.Apply(Harmony);
            //    else
            //        patch.Undo(Harmony);
            //}
        }
    }
}