using Harmony;
using RimWorld;
using Toggles.Source;
using Verse;

namespace Toggles.Patches
{
    [HarmonyPatch(typeof(IncidentWorker))]
    [HarmonyPatch("TryExecute")]
    [HarmonyPatch(new[] { typeof(IncidentParms) })]
    class IncidentWorker_Patch
    {
        internal IncidentWorker_Patch() => InitToggles();

        void InitToggles()
        {
            foreach (IncidentDef incident in DefDatabase<IncidentDef>.AllDefsListForReading)
                ToggleFactory.Add(
                    label: GetLabel(incident),
                    root: ButtonCat.Play,
                    group: "Incidents"
                    );
        }

        static string GetLabel(IncidentDef incident) => "Incident_" + incident.defName;

        static bool Prefix(ref IncidentWorker __instance, ref bool __result)
        {
            if (!ToggleHandler.IsActive(GetLabel(__instance.def)))
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}