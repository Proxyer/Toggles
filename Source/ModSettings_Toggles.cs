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
            Letter_Patch.UpdateCustomLetters();
            ToggleManager.Toggles.ForEach(toggle => toggle.ExposeData());
            HotkeyHandler.AllHotkeys.ForEach(hotkey => hotkey.ExposeData());
            AlertsReadout_Patch.ExposeData();
        }
    }
}