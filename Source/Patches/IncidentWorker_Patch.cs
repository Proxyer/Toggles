using RimWorld;

namespace Toggles.Patches
{
    internal class IncidentWorker_Patch : Patch
    {
        internal IncidentWorker_Patch() : base(
            patchType: typeof(IncidentWorker_Patch),
            originType: typeof(IncidentWorker),
            originMethod: "TryExecute",
            paramTypes: new[] { typeof(IncidentParms) }
            )
        { }

        static string PrefixLabel { get; } = "Incident";

        static bool Prefix(ref IncidentWorker __instance, ref bool __result)
        {
            if (!ToggleHandler.IsActive(PrefixLabel + __instance.def.defName))
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}