using HugsLib;
using RimWorld;
using Toggles.Hotkeys;
using UnityEngine;
using Verse;

namespace Toggles.News
{
    class ModBase_Toggles : ModBase
    {
        public override string ModIdentifier => Constants.ModName;

        //protected override bool HarmonyAutoPatch => false;

        //public static KeyBindingDef mapkey = KeyBindingDef.Named("OpenTogglesSettings");

        public override void OnGUI()
        {
            base.OnGUI();

            KeyListener();
        }

        void KeyListener()
        {
            bool flag = Event.current.type != EventType.KeyDown;
            if (!flag)
            {
                bool justPressed2 = KeyBindings.OpenTogglesSettings.JustPressed;
                if (justPressed2)
                {
                    if (!Find.WindowStack.IsOpen(typeof(Dialog_Options)))
                        Find.WindowStack.Add(new Dialog_Options());
                }
            }
        }
    }
}