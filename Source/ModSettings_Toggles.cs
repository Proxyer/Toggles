using Toggles.Hotkeys;
using Toggles.Patches;
using Verse;

namespace Toggles
{
    // Responsible for saving and loading mod data.
    internal class ModSettings_Toggles : ModSettings
    {
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look<string>(ref Letter_Patch.customLetters, "CustomLetters", LookMode.Undefined, new object[0]);
            //Scribe_Collections.Look<string, string>(ref KeyBindingHandler.hotkeyDict, "HotkeyLabels", LookMode.Undefined, LookMode.Value);
            Letter_Patch.UpdateCustomLetters();
            ToggleManager.Toggles.ForEach(x => x.ExposeData());
            foreach (Hotkey hotkey in HotkeyHandler.hotkeyDict.Values)
                hotkey.ExposeData();
        }
    }
}