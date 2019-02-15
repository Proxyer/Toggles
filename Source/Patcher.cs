using Harmony;
using Verse;

namespace Toggles
{
    [StaticConstructorOnStartup]
    internal static class Patcher
    {
        static HarmonyInstance Harmony { get; set; } = HarmonyInstance.Create(Constants.ModName);

        static Patcher() => DoPatches();

        internal static void DoPatches()
        {
            // DEBUG Patch All
            foreach (var patch in Constants.Patches)
                patch.Apply(Harmony);

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