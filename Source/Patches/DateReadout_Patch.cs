﻿using RimWorld;
using UnityEngine;

namespace Toggles.Patches
{
    // Toggles the date on the HUD.
    internal class DateReadout_Patch : Patch
    {
        internal DateReadout_Patch() : base(
            patchType: typeof(DateReadout_Patch),
            originType: typeof(DateReadout),
            originMethod: "DateOnGUI",
            paramTypes: new[] { typeof(Rect) }
            )
        { }

        internal override void InitToggles()
        {
            ToggleFactory.Add(
                    label: Label,
                    root: "InGameUI",
                    group: "HUD",
                    patch: "DateReadout_Patch"
                    );
        }

        static string Label { get; } = "Date";

        // Stops the date from being drawn if setting is inactive.
        static bool Prefix()
        {
            return ToggleHandler.IsActive(Label);
        }
    }
}