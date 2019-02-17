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
    internal class TimeControls_Patch : Patch
    {
        internal TimeControls_Patch() : base(
            patchType: typeof(TimeControls_Patch),
            originType: typeof(TimeControls),
            originMethod: "DoTimeControlsGUI",
            paramTypes: new[] { typeof(Rect) }
            )
        { }

        internal override void InitToggles()
        {
            ToggleFactory.Add(
                    label: Label,
                    root: "InGameUI",
                    group: "HUD",
                    patch: "TimeControls_Patch"
                    );
        }

        static string Label { get; } = "TimeControls";

        // Proxy method for showing the time control buttons.
        static bool ButtonImage_Proxy(Rect rect, Texture2D tex)
        {
            return Widgets.ButtonImage(rect, GetTexture(tex));
        }

        // Proxy method for showing the time control highlights.
        static void DrawTexture_Proxy(Rect rect, Texture2D tex)
        {
            GUI.DrawTexture(rect, GetTexture(tex));
        }

        // Returns empty texture to draw if toggled off.
        static Texture2D GetTexture(Texture2D tex)
        {
            return ToggleHandler.IsActive(Label) ? tex : Constants.TexEmpty;
        }

        static MethodInfo _ButtonImage_Method { get; } = AccessTools.Method(typeof(Widgets), "ButtonImage", new Type[] { typeof(Rect), typeof(Texture2D) });
        static MethodInfo _ButtonImage_Proxy { get; } = AccessTools.Method(typeof(TimeControls_Patch), "ButtonImage_Proxy", new Type[] { typeof(Rect), typeof(Texture2D) });

        static MethodInfo _DrawTexture_Method { get; } = AccessTools.Method(typeof(GUI), "DrawTexture", new Type[] { typeof(Rect), typeof(Texture2D) });
        static MethodInfo _DrawTexture_Proxy { get; } = AccessTools.Method(typeof(TimeControls_Patch), "DrawTexture_Proxy", new Type[] { typeof(Rect), typeof(Texture2D) });

        // Replacing all calls for adding buttons with proxy methods.
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return instructions
                .MethodReplacer(_ButtonImage_Method, _ButtonImage_Proxy)
                .MethodReplacer(_DrawTexture_Method, _DrawTexture_Proxy);
        }
    }
}