using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
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

        static Dictionary<Type, string> Dict = new List<Type>(typeof(Alert).AllLeafSubclasses())
            .ToDictionary(x => x, x => x.Name.Replace("Alert_", ""));

        internal override void InitToggles()
        {
            foreach (Type alert in Dict.Keys)
                ToggleFactory.Add(
                    label: Dict.TryGetValue(alert),
                    root: "InGameUI",
                    group: "Alerts",
                    patch: "AlertsReadout_Patch"
                    );
        }

        static void Postfix(ref List<Alert> ___AllAlerts, ref List<Alert> ___activeAlerts)
        {
            foreach (Type alert in Dict.Keys)
            {
                string name = Dict.TryGetValue(alert);
                if (!ToggleHandler.IsActive(name))
                {
                    ___AllAlerts.RemoveAll(x => x.GetType().Name.Equals(name));
                    ___activeAlerts.RemoveAll(x => x.GetType().Name.Equals(name));
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