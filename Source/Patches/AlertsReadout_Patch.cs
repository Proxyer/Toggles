using RimWorld;
using System;
using System.Collections.Generic;
using Verse;

namespace Toggles.Patches
{
    // Toggles all alerts on the HUD.
    internal class AlertsReadout_Patch : Patch
    {
        internal AlertsReadout_Patch() : base(
            patchType: typeof(AlertsReadout_Patch),
            originType: typeof(AlertsReadout),
            originMethod: "AlertsReadoutUpdate"
            )
        { }

        static List<Type> AllAlerts;

        static void Postfix(ref List<Alert> ___AllAlerts, ref List<Alert> ___activeAlerts)
        {
            // Saves all available alerts for reference.
            if (AllAlerts == null)
            {
                AllAlerts = new List<Type>();
                foreach (Type type in typeof(Alert).AllLeafSubclasses())
                    AllAlerts.Add(type);
            }

            foreach (Type alert in AllAlerts)
            {
                string name = alert.Name;
                if (!ToggleHandler.IsActive(name))
                {
                    if (___AllAlerts.Exists(x => x.GetType().Name.Equals(name)))
                    {
                        ___AllAlerts.RemoveAll(x => x.GetType().Name.Equals(name));
                        ___activeAlerts.RemoveAll(x => x.GetType().Name.Equals(name));
                    }
                }
                else
                {
                    if (!___AllAlerts.Exists(x => x.GetType().Name.Equals(name)))
                        ___AllAlerts.Add((Alert)Activator.CreateInstance(alert));
                }
            }
        }
    }
}