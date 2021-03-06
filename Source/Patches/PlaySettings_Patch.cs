﻿using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    // Toggles the buttons for various elements on the HUD, e.g. colonist bar and learning helper.
    [HarmonyPatch(typeof(PlaySettings))]
    [HarmonyPatch("DoPlaySettingsGlobalControls")]
    [HarmonyPatch(new[] { typeof(WidgetRow), typeof(bool) })]
    class PlaySettings_Patch
    {
        internal PlaySettings_Patch()
        {
            foreach (string label in Dict.Values)
                ToggleManager.Add(
                    label: Format(label),
                    root: ButtonCat.PlayScreen,
                    group: ButtonCat.ToggleButtons
                    );
        }

        // Proxy method for filtering out which buttons to display depending on their string labels.
        static void ToggleableIcon_Proxy(WidgetRow instance, ref bool toggleable, Texture2D tex, string label, SoundDef mouseoverSound = null, string tutorTag = null)
        {
            if (!Dict.ContainsKey(label) || (Dict.ContainsKey(label) && ToggleManager.IsActive(Format(Dict.TryGetValue(label)))))
                instance.ToggleableIcon(ref toggleable, tex, label.Translate(), SoundDefOf.Mouseover_ButtonToggle, null);
        }

        // Dictionary for applying shorter label names to settings.
        static Dictionary<string, string> Dict { get; } = new Dictionary<string, string>
        {
            {"ShowLearningHelperWhenEmptyToggleButton", "LearningHelperToggle" },
            {"ZoneVisibilityToggleButton", "ZoneVisibilityToggle" },
            {"ShowBeautyToggleButton", "ShowBeautyToggle" },
            {"ShowRoomStatsToggleButton", "ShowRoomStatsToggle" },
            {"ShowColonistBarToggleButton", "ShowColonistBarToggle" },
            {"ShowRoofOverlayToggleButton", "ShowRoofOverlayToggle" },
            {"AutoHomeAreaToggleButton", "AutoHomeAreaToggle" },
            {"AutoRebuildButton", "AutoRebuildToggle" },
            {"CategorizedResourceReadoutToggleButton", "CategorizedResourceReadoutToggle" }
        };

        static string Format(string label) => $"{ButtonCat.ToggleButtons}_{label}";

        static MethodInfo _ToggleableIcon_Method { get; } = AccessTools.Method(typeof(WidgetRow), "ToggleableIcon", new Type[] { typeof(bool).MakeByRefType(), typeof(Texture2D), typeof(string), typeof(SoundDef), typeof(string) });
        static MethodInfo _ToggleableIcon_Proxy { get; } = AccessTools.Method(typeof(PlaySettings_Patch), "ToggleableIcon_Proxy", new Type[] { typeof(WidgetRow), typeof(bool).MakeByRefType(), typeof(Texture2D), typeof(string), typeof(SoundDef), typeof(string) });
        static MethodInfo Translate { get; } = AccessTools.Method(typeof(Translator), "Translate", new Type[] { typeof(string) });

        // Replacing all calls for adding buttons with proxy method.
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            // Removing all translation calls in the original method and reapplying them myself in the proxy method.
            codes.RemoveAll(x => x.opcode == OpCodes.Call && x.operand == Translate);
            return codes.MethodReplacer(_ToggleableIcon_Method, _ToggleableIcon_Proxy);
        }
    }
}