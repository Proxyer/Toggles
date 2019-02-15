using Toggles.Patches;

namespace Toggles
{
    internal static class Constants
    {
        internal static string InitFilePath { get; } = "..\\Properties\\InitDB.xml";
        internal static string DBPath { get; } = "Toggles.Properties.InitDB.xml";

        internal static string ModName { get; } = "Toggles";

        internal static Patch[] Patches { get; } = {
            new ListableOption_Patch(),
            new ListableOption_WebLink_Patch(),
            new VersionControl_Patch(),
            new Widgets_Patch(),
            new UI_BackgroundMain_Patch(),
            new DateReadout_Patch(),
            new ResourceReadout_Patch(),
            new MouseoverReadout_Patch(),
            new AlertsReadout_Patch(),
            new Letter_Patch(),
            new WeatherManager_Patch(),
            new GlobalControls_Patch(),
            new GUI_Patch(),
            new ColonistBar_Patch(),
            new IncidentWorker_Patch(),
            new PlaySettings_Patch(),

            //new DevTools_Patch(),
            //new ReceiveLetter_Patch(),
            //new GizmoGrid_Patch(),
            //new Pawn_Patch(),
            //new Command_Patch(),
        };
    }
}