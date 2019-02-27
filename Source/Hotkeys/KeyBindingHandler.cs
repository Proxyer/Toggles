using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Toggles.Hotkeys
{
    internal class KeyBindingHandler
    {
        internal KeyBindingHandler()
        {
        }

        static KeyBindingHandler keyManager;

        internal static KeyBindingHandler KeyManager
        {
            get => keyManager != null ? keyManager : keyManager = new KeyBindingHandler();
            private set => keyManager = value;
        }

        internal static List<KeyBindingDef> Hotkeys { get; set; } = new List<KeyBindingDef>
        {
            KeyBindingDef.Named("ToggleOneDef"),
            KeyBindingDef.Named("ToggleTwoDef"),
            KeyBindingDef.Named("ToggleThreeDef"),
            KeyBindingDef.Named("ToggleFourDef"),
            KeyBindingDef.Named("ToggleFiveDef")
    };

        internal static void KeyListener()
        {
            if (!(Event.current.type != EventType.KeyDown))
                foreach (KeyBindingDef hotkey in Hotkeys)
                    if (hotkey.KeyDownEvent)
                        ToggleManager.ToggleMany(hotkey.label);
        }

        //internal static void ChangeBindingLabel(string label)
        //{
        //    one.label = label;
        //}

        //public void ExposeData()
        //{
        //    Log.Message("ExposeData " + hotkey1.label);
        //    Scribe_Values.Look<string>(ref hotkey1.label, "keyOne", "Default str", true);
        //}
    }
}