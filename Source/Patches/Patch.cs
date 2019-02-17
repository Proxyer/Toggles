using Harmony;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Toggles.Patches
{
    internal abstract class Patch
    {
        internal Patch(Type patchType, Type originType, string originMethod, Type[] paramTypes = null)
        {
            Method = AccessTools.Method(originType, originMethod, paramTypes ?? Type.EmptyTypes);

            List<MethodInfo> methods = AccessTools.GetDeclaredMethods(patchType);

            if (methods.Exists(x => x.Name.Equals("Prefix")))
            {
                PrefixPatchMethod = methods.Find(x => x.Name.Equals("Prefix"));

                Prefix = new HarmonyMethod(PrefixPatchMethod);
            }
            if (methods.Exists(x => x.Name.Equals("Postfix")))
            {
                PostfixPatchMethod = methods.Find(x => x.Name.Equals("Postfix"));
                Postfix = new HarmonyMethod(PostfixPatchMethod);
            }
            if (methods.Exists(x => x.Name.Equals("Transpiler")))
            {
                TranspilerPatchMethod = methods.Find(x => x.Name.Equals("Transpiler"));
                Transpiler = new HarmonyMethod(TranspilerPatchMethod);
            }
        }

        MethodInfo Method { get; set; }
        MethodInfo PrefixPatchMethod { get; set; }
        MethodInfo PostfixPatchMethod { get; set; }
        MethodInfo TranspilerPatchMethod { get; set; }
        HarmonyMethod Prefix { get; set; } = null;
        HarmonyMethod Transpiler { get; set; } = null;
        HarmonyMethod Postfix { get; set; } = null;

        internal virtual void Apply(HarmonyInstance harmony)
        {
            harmony.Patch(Method, Prefix, Postfix, Transpiler);
        }

        internal virtual void Undo(HarmonyInstance harmony)
        {
            harmony.Unpatch(Method, HarmonyPatchType.All);
        }

        internal abstract void InitToggles();
    }
}