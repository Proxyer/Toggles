using Harmony;
using Toggles.Patches;
using Verse;

namespace Toggles
{
    [StaticConstructorOnStartup]
    internal static class Patcher
    {
        static HarmonyInstance Harmony { get; } = HarmonyInstance.Create(Constants.ModName);

        static Patcher() => DoPatches();

        internal static void DoPatches()
        {
            new ListableOption_Patch();
            new ListableOption_WebLink_Patch();
            new VersionControl_Patch();
            new Widgets_Patch();
            new UI_BackgroundMain_Patch();
            new DateReadout_Patch();
            new ResourceReadout_Patch();
            new MouseoverReadout_Patch();
            new WeatherManager_Patch();
            new GlobalControls_Patch();
            new GUI_Patch();
            new ColonistBar_Patch();
            new PlaySettings_Patch();
            new TimeControls_Patch();
            new Letter_Patch();
            new AlertsReadout_Patch();
            new IncidentWorker_Patch();

            // Work in progress
            //new LetterStack_Patch();
        }
    }
}