//using RimWorld;
//using UnityEngine;
//using Verse;

//namespace Toggles.Hotkeys
//{
//    internal static class KeyBindingHandler
//    {
//        public static void OnGUI()
//        {
//            bool flag = Event.current.type != EventType.KeyDown;
//            if (!flag)
//            {
//                bool justPressed = KeyBindings.OpenTogglesSettings.JustPressed;
//                if (justPressed)
//                {
//                    Find.WindowStack.Add(new Dialog_Options());
//                }
//            }
//        }
//    }
//}