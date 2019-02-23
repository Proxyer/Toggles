using Harmony;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Toggles.Source;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(Widgets))]
    [HarmonyPatch("Label")]
    [HarmonyPatch(new[] { typeof(Rect), typeof(string) })]
    class Widgets_Patch
    {
        internal Widgets_Patch() => InitToggles();

        static void InitToggles()
        {
            ToggleFactory.Add(
                    label: GetLabel(),
                    root: ButtonCat.StartScreen,
                    group: ButtonCat.MiscellaneousEntry
                    );
        }

        static string GetLabel() => ButtonCat.MiscellaneousEntry + "_MainPageCredit";

        static bool Prefix(string label)
        {
            return label.Equals("MainPageCredit".Translate()) ? ToggleHandler.IsActive(GetLabel()) : true;
        }

        //static MethodInfo _ToggleableIcon_Method { get; } = AccessTools.Method(typeof(WidgetRow), "ToggleableIcon", new Type[] { typeof(bool).MakeByRefType(), typeof(Texture2D), typeof(string), typeof(SoundDef), typeof(string) });
        //static MethodInfo _ToggleableIcon_Proxy { get; } = AccessTools.Method(typeof(PlaySettings_Patch), "ToggleableIcon_Proxy", new Type[] { typeof(WidgetRow), typeof(bool).MakeByRefType(), typeof(Texture2D), typeof(string), typeof(SoundDef), typeof(string) });
        static MethodInfo Translate { get; } = AccessTools.Method(typeof(Translator), "Translate", new Type[] { typeof(string) });

        //static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        //{
        //    var codes = new List<CodeInstruction>(instructions);
        //    codes.RemoveAll(x => x.opcode == OpCodes.Call && x.operand == Translate);
        //    return codes;
        //}
    }
}