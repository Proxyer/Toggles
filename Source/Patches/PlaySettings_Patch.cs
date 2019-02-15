using Harmony;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace Toggles.Patches
{
    internal class PlaySettings_Patch : Patch
    {
        internal PlaySettings_Patch() : base(
            patchType: typeof(PlaySettings_Patch),
            originType: typeof(PlaySettings),
            originMethod: "DoPlaySettingsGlobalControls",
            paramTypes: new[] { typeof(WidgetRow), typeof(bool) }
            )
        { }

        public static bool Check(string str)
        {
            DebugUtil.Log("Check for bool, return false");
            return false;
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            var codes = new List<CodeInstruction>(instructions);
            var instructionsToInsert = new List<CodeInstruction>();
            Label endIcon = il.DefineLabel();

            int insertionIndex = -1;
            int endIndex = -1;
            int jumpToIndex = -1;

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldstr && (string)codes[i].operand == "ShowLearningHelperWhenEmptyToggleButton")
                {
                    // Backtrack to find first code of toggleicon
                    for (int j = i; j > -1; j--)
                    {
                        // Ldarg_1 marks the first code of the toggleicon pattern.
                        if (codes[j].opcode == OpCodes.Ldarg_1)
                        {
                            insertionIndex = j;

                            break;
                        }
                    }

                    bool foundCall = false;
                    if (!foundCall)
                    {
                        for (int k = i; k < codes.Count; k++)
                        {
                            if (codes[k].opcode == OpCodes.Callvirt)
                            {
                                if (codes[k].operand == AccessTools.Method(typeof(WidgetRow), "ToggleableIcon", new[] { typeof(bool).MakeByRefType(), typeof(Texture2D), typeof(string), typeof(SoundDef), typeof(string), }))
                                {
                                    endIndex = k;
                                    jumpToIndex = endIndex + 1;
                                    codes[k + 1].labels.Add(endIcon);
                                    foundCall = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //instructionsToInsert.Add(new CodeInstruction(OpCodes.Ldstr, "ShowLearningHelperWhenEmptyToggleButton"));
            //instructionsToInsert.Add(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PlaySettings_Patch), "Check", new System.Type[] { typeof(string) })));
            instructionsToInsert.Add(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(Dialog_Settings), "Check")));
            instructionsToInsert.Add(new CodeInstruction(OpCodes.Brfalse, endIcon));

            if (insertionIndex != -1)
            {
                Log.Message("Inserted instructions.");
                codes.InsertRange(insertionIndex, instructionsToInsert);
            }

            // Just works. Nopping.
            //CodeInstruction ci = codes.Find(x => x.labels.Contains(endIcon));
            //Log.Message("Start index " + insertionIndex);
            //Log.Message("CI: " + ci.opcode.ToString() + " index: " + codes.IndexOf(ci));
            //for (int i = insertionIndex; i < 79; i++)
            //{
            //    codes[i].opcode = OpCodes.Nop;
            //}
            //codes.ForEach(k => DebugUtil.Log(k.opcode + " " + (k?.operand?.ToString() ?? "")));

            return codes.AsEnumerable();
        }
    }
}