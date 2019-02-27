using HugsLib;
using Toggles.Hotkeys;

namespace Toggles
{
    class ModBase_Toggles : ModBase
    {
        public override string ModIdentifier => Constants.ModName;

        //protected override bool HarmonyAutoPatch => false;

        public override void OnGUI()
        {
            base.OnGUI();
            KeyBindingHandler.KeyListener();
        }
    }
}