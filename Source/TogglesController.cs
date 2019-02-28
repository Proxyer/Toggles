using Harmony;
using RimWorld;
using System.Reflection;
using Toggles.Patches;
using Verse;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Toggles
{
    // Controls the initialization flow of the mod.
    [StaticConstructorOnStartup]
    class TogglesController
    {
        static TogglesController()
        {
            //MethodInfo me;
            ////var method = AccessTools.Method(typeof(Dialog_KeyBindings), "DrawKeyEntry");
            //List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(Dialog_KeyBindings));
            //foreach (MethodInfo method in methods)
            //{
            //    if (method.Name.Equals("DrawKeyEntry"))
            //    {
            //        foreach (ParameterInfo para in method.GetParameters())
            //            DebugUtil.Log(para.Name);
            //    }
            //    me = method;
            //}

            //var v = me.GetParameters();

            //DebugUtil.Log("METHOD  " + method.Name);

            InitTextures();
            //InitPatches();
            InitToggles();
            InitSettings();
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

            //new Dialog_KeyBindings_Patch();
        }

        static void InitPatches() =>
            HarmonyInstance.Create(Constants.ModName)
                .PatchAll(Assembly.GetExecutingAssembly());
    }
}