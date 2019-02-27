using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    // Toggles visibility of the buttons for time controls.
    [HarmonyPatch(typeof(TimeControls))]
    [HarmonyPatch("DoTimeControlsGUI")]
    [HarmonyPatch(new[] { typeof(Rect) })]
    class TimeControls_Patch
    {
        internal TimeControls_Patch() =>
            ToggleManager.Add(
                label: Format(),
                root: ButtonCat.PlayScreen,
                group: ButtonCat.Misc
                );

        static string Format() => $"{ButtonCat.Misc}_TimeControls";

        // Proxy method for showing the time control buttons.
        static bool ButtonImage_Proxy(Rect rect, Texture2D tex) =>
            Widgets.ButtonImage(rect, GetTexture(tex));

        // Proxy method for showing the time control highlights.
        static void DrawTexture_Proxy(Rect rect, Texture2D tex) =>
            GUI.DrawTexture(rect, GetTexture(tex));

        // Returns empty texture to draw if toggled off.
        static Texture2D GetTexture(Texture2D tex) =>
            ToggleManager.IsActive(Format()) ? tex : Constants.TexEmpty;

        static MethodInfo _ButtonImage_Method { get; } = AccessTools.Method(typeof(Widgets), "ButtonImage", new Type[] { typeof(Rect), typeof(Texture2D) });
        static MethodInfo _ButtonImage_Proxy { get; } = AccessTools.Method(typeof(TimeControls_Patch), "ButtonImage_Proxy", new Type[] { typeof(Rect), typeof(Texture2D) });

        static MethodInfo _DrawTexture_Method { get; } = AccessTools.Method(typeof(GUI), "DrawTexture", new Type[] { typeof(Rect), typeof(Texture2D) });
        static MethodInfo _DrawTexture_Proxy { get; } = AccessTools.Method(typeof(TimeControls_Patch), "DrawTexture_Proxy", new Type[] { typeof(Rect), typeof(Texture2D) });

        // Replacing all calls for adding buttons with proxy methods.
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) =>
            instructions
                .MethodReplacer(_ButtonImage_Method, _ButtonImage_Proxy)
                .MethodReplacer(_DrawTexture_Method, _DrawTexture_Proxy);
    }
}