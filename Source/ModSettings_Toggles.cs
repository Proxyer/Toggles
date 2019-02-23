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
            //DebugUtil.Log("Scribe_Collections");
            Scribe_Collections.Look<string>(ref Letter_Patch.customLetters, "CustomLetters", LookMode.Undefined, new object[0]);
            ////DebugUtil.Log("UpdateCustomLetters");
            Letter_Patch.UpdateCustomLetters();
            //DebugUtil.Log("ExposeData");
            ToggleHandler.Toggles.ForEach(x => x.ExposeData());
        }
    }
}