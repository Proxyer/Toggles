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

        static bool Prefix(ref IncidentWorker __instance)
        {
            DebugUtil.Log("Incident: " + __instance.def.defName);
            return true;
        }
    }
}