using Harmony;
using RimWorld;
using Toggles.Source;
using Verse;

namespace Toggles.Patches
{
    // Toggles incidents from happening.
    [HarmonyPatch(typeof(IncidentWorker))]
    [HarmonyPatch("TryExecute")]
    [HarmonyPatch(new[] { typeof(IncidentParms) })]
    class IncidentWorker_Patch
    {
        internal IncidentWorker_Patch() =>
            DefDatabase<IncidentDef>.AllDefsListForReading
                .ForEach(incidentDef =>
                    ToggleManager.Add(
                        label: Format(incidentDef),
                        root: ButtonCat.Events,
                        group: ButtonCat.Incidents
                        ));

        static string Format(IncidentDef incident) => $"{ButtonCat.Incidents}_{incident.defName}";

        // Stops incident from happening if setting is inactive.
        static bool Prefix(ref IncidentWorker __instance, ref bool __result)
        {
            if (!ToggleManager.IsActive(Format(__instance.def)))
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}