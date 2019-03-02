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
        static string LabelCap_Proxy(KeyBindingDef instance) =>
            HotkeyHandler.AllHotkeys.Exists(hotkey => hotkey.Def == instance) ?
                HotkeyHandler.HotKeyDict.TryGetValue(instance.defName).CustomLabel :
                instance.LabelCap;

        static MethodInfo LabelCap_Method { get; } = AccessTools.Method(typeof(Def), "get_LabelCap");
        static MethodInfo LabelCap_Proxy_Method { get; } = AccessTools.Method(typeof(Dialog_KeyBindings_Patch), "LabelCap_Proxy", new Type[] { typeof(KeyBindingDef) });

        // Proxies the LabelCap-method with my own.
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) =>
            instructions.MethodReplacer(LabelCap_Method, LabelCap_Proxy_Method);
    }
}