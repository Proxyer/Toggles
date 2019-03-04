using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Toggles.Hotkeys
{
    internal static class HotkeyHandler
    {
        internal static void InitHotkeys() =>
            DefDatabase<KeyBindingDef>.AllDefsListForReading
            .Where(def =>
                def.category.defName.Equals("TogglesHotkeys")).ToList()
            .ForEach(def =>
                HotKeyDict.Add(def.defName, new Hotkey(def)));

        internal static List<Hotkey> AllHotkeys { get => HotKeyDict.Values.ToList(); }

        internal static Dictionary<string, Hotkey> HotKeyDict = new Dictionary<string, Hotkey>();

        static string ActiveHotkey { get; set; } = string.Empty;

        internal static void KeyListener()
        {
            if (!(Event.current.type != EventType.KeyDown))
            {
                foreach (Hotkey hotkey in AllHotkeys)
                {
                    if (hotkey.Def.KeyDownEvent)
                    {
                        ActiveHotkey = hotkey.Def.defName;
                        ToggleManager.ToggleMany(ActiveHotkey);
                        Mod_Toggles.thisMod.WriteSettings();
                    }
                }
            }
        }
    }
}