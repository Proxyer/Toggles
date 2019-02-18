using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using Toggles.Source;
using Verse;

namespace Toggles.Patches
{
    // Toggles all alerts on the HUD.
    [HarmonyPatch(typeof(AlertsReadout))]
    [HarmonyPatch("AlertsReadoutUpdate")]
    class AlertsReadout_Patch
    {
        internal AlertsReadout_Patch() => InitToggles();

        static List<Alert> Alerts { get; } = new List<Alert>();

        static void InitToggles()
        {
            foreach (Type type in typeof(Alert).AllLeafSubclasses())
            {
                Alert alert = (Alert)Activator.CreateInstance(type);
                Alerts.Add(alert);
                ToggleFactory.Add(
                    label: GetLabel(alert),
                    root: ButtonCat.Play,
                    group: "Alerts"
                    );
            }
        }

        static string GetLabel(Alert alert) => alert.GetType().Name;

        static void Postfix(ref List<Alert> ___AllAlerts, ref List<Alert> ___activeAlerts)
        {
            foreach (Alert alert in Alerts)
            {
                string label = GetLabel(alert);
                if (!ToggleHandler.IsActive(GetLabel(alert)))
                {
                    ___AllAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
                    ___activeAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
                }
                else
                {
                    if (!___AllAlerts.Exists(x => x.GetType().Name.Equals(label)))
                        ___AllAlerts.Add((Alert)Activator.CreateInstance(alert.GetType()));
                }
            }
        }
    }
}