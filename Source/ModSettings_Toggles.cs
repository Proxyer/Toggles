using Verse;

namespace Toggles
{
    // Responsible for saving and loading mod data.
    internal class ModSettings_Toggles : ModSettings
    {
        public override void ExposeData()
        {
            base.ExposeData();
            //ToggleHandler.Toggles.ForEach(x => Scribe_Values.Look(ref x.active, x.Label, true));
            ToggleHandler.Toggles.ForEach(x => x.ExposeData());
        }
    }
}