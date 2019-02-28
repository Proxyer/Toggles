//using Harmony;
//using RimWorld;
//using UnityEngine;
//using Verse;

//namespace Toggles.Patches
//{
//    // Toggles the date on the HUD.
//    [HarmonyPatch(typeof(Dialog_KeyBindings))]
//    [HarmonyPatch("DrawKeyEntry")]
//    [HarmonyPatch(new[] { typeof(KeyBindingDef), typeof(Rect), typeof(float).MakeByRefType(), typeof(bool) })]
//    class Dialog_KeyBindings_Patch
//    {
//        HarmonyTargetMethod targetMethod = new HarmonyTargetMethod();

//        //KeyBindingDef keyDef, Rect parentRect, ref float curY, bool skipDrawing
//        //internal DateReadout_Patch() =>
//        //    ToggleManager.Add(
//        //        label: Label,
//        //        root: ButtonCat.PlayScreen,
//        //        group: ButtonCat.Readouts
//        //        );

//        static string Label => $"{ButtonCat.Readouts}_Date";

//        // Stops the date from being drawn if setting is inactive.
//        static bool Prefix() => ToggleManager.IsActive(Label);
//    }
//}

using Harmony;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;

namespace Toggles.Patches
{
    [HarmonyPatch]
    public class Dialog_KeyBindings_Patch
    {
        //[HarmonyTargetMethod]
        public static MethodInfo HarmonyTargetMethod()
        {
            MethodInfo me = null;
            List<MethodInfo> methods = AccessTools.GetDeclaredMethods(typeof(Dialog_KeyBindings));
            foreach (MethodInfo method in methods)
            {
                if (method.Name.Equals("DrawKeyEntry"))
                    me = method;
            }
            return me;
        }

        [HarmonyPrefix]
        static bool Prefix()
        {
            DebugUtil.Log("Hey, now!");
            return true;
        }
    }
}