using Harmony;
using System.Reflection;
using Toggles.Hotkeys;
using Toggles.Patches;
using Verse;

namespace Toggles
{
    // Controls the initialization flow of the mod.
    [StaticConstructorOnStartup]
    class TogglesController
    {
        static TogglesController()
        {
            InitTextures();
            //InitPatches();
            InitToggles();
            InitHotkeys();
            InitSettings();
        }

        static void InitHotkeys()
        {
            HotkeyHandler.InitHotkeys();
        }

        static void InitTextures() => Constants.InitTextures();

        static void InitSettings() =>
            ((Mod_Toggles)LoadedModManager
                .GetMod(typeof(Mod_Toggles)))
                .InitSettings();

        // Constructs all patch instances, which initializes all respective toggles.
        static void InitToggles()
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
            new SkyOverlay_Patch();
        }

        static void InitPatches() =>
            HarmonyInstance.Create(Constants.ModName)
                .PatchAll(Assembly.GetExecutingAssembly());
    }
}