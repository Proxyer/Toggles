﻿using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
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
        static Dictionary<string, int> SleepingAlerts { get; } = new Dictionary<string, int>();
        internal static void AddSleepingAlert(Alert alert)
        {
            if (!SleepingAlerts.ContainsKey(Format(alert)))
                SleepingAlerts.Add(Format(alert), Find.TickManager.TicksGame);
        }

        static string Format(Alert alert) => alert.GetType().Name;

        static int hourMultiplier = 1;

        // Checks if alert to be read out is active in settings.
        // If not, the alert is removed from games' available alerts and the list of active alerts. Otherwise readded.
        static void Postfix(ref List<Alert> ___AllAlerts, ref List<Alert> ___activeAlerts)
        {
            foreach (Alert alert in Alerts)
            {
                string label = Format(alert);

                if (SleepingAlerts.ContainsKey(label))
                {
                    if (Find.TickManager.TicksGame - SleepingAlerts[label] < GenDate.TicksPerHour * hourMultiplier)
                    {
                        //___AllAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
                        //___activeAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
                        RemoveAlert(ref ___AllAlerts, ref ___activeAlerts, label);
                    }
                    else
                    {
                        //if (!___AllAlerts.Exists(x => x.GetType().Name.Equals(label)))
                        //    ___AllAlerts.Add((Alert)Activator.CreateInstance(alert.GetType()));
                        //SleepingAlerts.Remove(label);
                        AddAlert(ref ___AllAlerts, ref ___activeAlerts, alert);
                    }
                }
                else if (!ToggleManager.IsActive(label))
                {
                    RemoveAlert(ref ___AllAlerts, ref ___activeAlerts, label);
                    //___AllAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
                    //___activeAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
                }
                else
                {
                    AddAlert(ref ___AllAlerts, ref ___activeAlerts, alert);
                    //if (!___AllAlerts.Exists(x => x.GetType().Name.Equals(label)))
                    //    ___AllAlerts.Add((Alert)Activator.CreateInstance(alert.GetType()));
                }
            }
        }

        static void AddAlert(ref List<Alert> ___AllAlerts, ref List<Alert> ___activeAlerts, Alert alert)
        {
            if (!___AllAlerts.Exists(x => x.GetType().Name.Equals(Format(alert))))
                ___AllAlerts.Add((Alert)Activator.CreateInstance(alert.GetType()));
            SleepingAlerts.Remove(Format(alert));
        }

        static void RemoveAlert(ref List<Alert> ___AllAlerts, ref List<Alert> ___activeAlerts, string label)
        {
            ___AllAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
            ___activeAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
        }
    }
}

//foreach (Alert alert in Alerts)
//            {
//                string label = Format(alert);
//                if ((SleepingAlerts.ContainsKey(label)))
//                {
//                    if (Find.TickManager.TicksGame - SleepingAlerts[label] < GenDate.TicksPerHour)
//                    {
//                        ___AllAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
//                        ___activeAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
//                    }
//                    else
//                    {
//                        if (!___AllAlerts.Exists(x => x.GetType().Name.Equals(label)))
//                            ___AllAlerts.Add((Alert) Activator.CreateInstance(alert.GetType()));
//                        SleepingAlerts.Remove(label);
//                    }
//                }
//                else if (!ToggleManager.IsActive(label))
//                {
//                    ___AllAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
//                    ___activeAlerts.RemoveAll(x => x.GetType().Name.Equals(label));
//                }
//                else
//                {
//                    if (!___AllAlerts.Exists(x => x.GetType().Name.Equals(label)))
//                        ___AllAlerts.Add((Alert) Activator.CreateInstance(alert.GetType()));
//                }
//            }