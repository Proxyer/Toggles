using RimWorld;
using Verse;

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

        internal override void InitToggles()
        {
            foreach (IncidentDef incident in DefDatabase<IncidentDef>.AllDefsListForReading)
                ToggleFactory.Add(
                    label: incident.defName,
                    root: "InGameUI",
                    group: "Incidents",
                    patch: "IncidentWorker_Patch"
                    );
        }

        static bool Prefix(ref IncidentWorker __instance, ref bool __result)
        {
            if (!ToggleHandler.IsActive(__instance.def.defName))
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}