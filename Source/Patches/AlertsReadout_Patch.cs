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
        internal AlertsReadout_Patch()
        {
            foreach (Type type in typeof(Alert).AllLeafSubclasses())
            {
                Alert alert = (Alert)Activator.CreateInstance(type);
                Alerts.Add(alert);
                ToggleManager.Add(
                    label: Format(alert),
                    root: ButtonCat.Events,
                    group: ButtonCat.Alerts
                    );
            }
        }

        static List<Alert> Alerts { get; } = new List<Alert>();

        static string Format(Alert alert) => alert.GetType().Name;

        // Checks if alert to be read out is active in settings.
        // If not, the alert is removed from games' available alerts and the list of active alerts. Otherwise readded.
        static void Postfix(ref List<Alert> ___AllAlerts, ref List<Alert> ___activeAlerts)
        {
            foreach (Alert alert in Alerts)
            {
                string label = Format(alert);
                if (!ToggleManager.IsActive(label))
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