//using Harmony;
//using RimWorld;
//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using Toggles.Hotkeys;
//using UnityEngine;
//using Verse;

//namespace Toggles.Patches
//{
//    [HarmonyPatch]
//    class Dialog_KeyBindings_Patch
//    {
//        [HarmonyTargetMethod]
//        static MethodInfo HarmonyTargetMethod(HarmonyInstance harmonyInstance) =>
//            AccessTools.Method(typeof(Dialog_KeyBindings), "DrawKeyEntry");

//        // Proxy method for filtering out and replacing the KeyBindingDefs' labels of this mod.
//        static void Label_Proxy(KeyBindingDef def, Rect rect, string label)
//        {
//            if (KeyBindingHandler.Hotkeys.Contains(def))
//                Widgets.Label(rect, KeyBindingHandler.labelDict.TryGetValue(def.defName));
//            else
//                Widgets.Label(rect, label);
//        }

//        static MethodInfo Label_Method { get; } = AccessTools.Method(typeof(Widgets), "Label", new Type[] { typeof(Rect), typeof(KeyBindingDef) });
//        static MethodInfo Label_Proxy_Method { get; } = AccessTools.Method(typeof(Dialog_KeyBindings_Patch), "Label_Proxy", new Type[] { typeof(KeyBindingDef), typeof(Rect), typeof(KeyBindingDef) });

//        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//        {
//            var codes = new List<CodeInstruction>(instructions);
//            return codes.MethodReplacer(Label_Method, Label_Proxy_Method);
//        }
//    }
//}

using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using Toggles.Hotkeys;
using Verse;

namespace Toggles.Patches
{
    [HarmonyPatch]
    class Dialog_KeyBindings_Patch
    {
        [HarmonyTargetMethod]
        static MethodInfo HarmonyTargetMethod(HarmonyInstance harmonyInstance) =>
            AccessTools.Method(typeof(Dialog_KeyBindings), "DrawKeyEntry");

        // Proxy method for filtering out and replacing the KeyBindingDefs' labels of this mod.
        static string LabelCap_Proxy(KeyBindingDef instance)
        {
            if (HotkeyHandler.KeyDefs.Contains(instance))
            {
                return HotkeyHandler.hotkeyDict.TryGetValue(instance.defName).CustomLabel;
            }
            return instance.LabelCap;
        }

        static MethodInfo LabelCap_Method { get; } = AccessTools.Method(typeof(Def), "get_LabelCap");
        static MethodInfo LabelCap_Proxy_Method { get; } = AccessTools.Method(typeof(Dialog_KeyBindings_Patch), "LabelCap_Proxy", new Type[] { typeof(KeyBindingDef) });

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            return codes.MethodReplacer(LabelCap_Method, LabelCap_Proxy_Method);
        }
    }
}