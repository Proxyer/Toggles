using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Toggles.Hotkeys
{
    internal static class HotkeyHandler
    {
        internal static void InitHotkeys()
        {
            foreach (KeyBindingDef keyDef in KeyDefs)
            {
                Hotkey hotkey = new Hotkey(keyDef);

                hotkeyDict.Add(hotkey.Def.defName, hotkey);
            }
        }

        internal static List<KeyBindingDef> KeyDefs { get; set; } = new List<KeyBindingDef>
        {
            KeyBindingDef.Named("Hotkey1"),
            KeyBindingDef.Named("Hotkey2"),
            KeyBindingDef.Named("Hotkey3"),
            KeyBindingDef.Named("Hotkey4"),
            KeyBindingDef.Named("Hotkey5")
        };

        internal static Dictionary<string, Hotkey> hotkeyDict = new Dictionary<string, Hotkey>();

        internal static void KeyListener()
        {
            if (!(Event.current.type != EventType.KeyDown))
                foreach (Hotkey hotkey in hotkeyDict.Values)
                    if (hotkey.Def.KeyDownEvent)
                        ToggleManager.ToggleMany(hotkey.Def.defName);
        }
    }
}