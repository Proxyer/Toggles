//using System.Collections.Generic;
//using System.Linq;
//using Toggles.Source;
//using UnityEngine;
//using Verse;

//namespace Toggles
//{
//    // Draws settings window.
//    internal static class Dialog_Settings
//    {
//        static string ActiveGroup { get; set; } = string.Empty;
//        static Dictionary<string, bool> RefChanged { get; set; }
//        static Color ClickedColor { get; } = new Color(0.55f, 1f, 0.55f);
//        static Color DefaultColor { get; } = GUI.color;

//        static Vector2 scrollPositionLeft;
//        static Vector2 scrollPositionMiddle;
//        static Vector2 scrollPositionRight;

//        static MultiCheckboxState state;

//        internal static void DoWindowContents(Rect mainRect)
//        {
//            Rect leftRect = new Rect(mainRect.x, mainRect.y, mainRect.width / 4, mainRect.height);
//            DoLeft(leftRect);
//            float f = 100f;

//            Rect middleRect = new Rect(leftRect.width, mainRect.y, (mainRect.width - leftRect.width) / 2, mainRect.height);
//            DoMiddle(middleRect);
//            //Rect rightRect = new Rect(middleRect.xMax, mainRect.y, (mainRect.width - leftRect.width) / 2, mainRect.height);
//            //DoRight(rightRect);

//            // Button for reset.
//            Rect resetRect = new Rect(0f, mainRect.height + 40f, 120f, 40f);
//            if (Widgets.ButtonText(resetRect, "ResetButton".Translate(), true, false, true))
//                ToggleHandler.Toggles.ForEach(x => x.active = true);
//        }

//        static void DoRight(Rect rightRect)
//        {
//            //List<Toggle> groupMembers = ToggleHandler.Toggles.Where(x => x.Group.Equals(ActiveGroup)).ToList();
//            //float rightY = (groupMembers.Count() + 2) * 25f;

//            float rightY = 400f;

//            Rect viewRect = new Rect(0f, 0f, rightRect.width - 16f, rightRect.height);
//            Rect rightInRect = new Rect(0f, 0f, width: rightRect.width - 20f, height: rightY);
//            Rect rightViewRect = new Rect(0f, 0f, 200f, rightY);
//            scrollPositionRight = GUI.BeginScrollView(rightRect, scrollPositionRight, rightViewRect);
//            var rightView = new Listing_Toggles();
//            rightView.ColumnWidth = rightInRect.width;
//            rightView.Begin(rightInRect);

//            // Draw toggles in right view depending on what button is active in left view.
//            //foreach (Toggle toggle in ToggleHandler.Toggles.Where(x => x.Group.Equals(ActiveGroup)))
//            //    rightView.CheckboxLabeled(toggle.PrettyLabel, ref toggle.active);

//            rightView.EndListing(ref viewRect);
//            GUI.EndScrollView();
//        }

//        static void DoMiddle(Rect middleRect)
//        {
//            List<Toggle> groupMembers = ToggleHandler.Toggles.Where(x => x.Group.Equals(ActiveGroup)).ToList();
//            float middleY = (groupMembers.Count() + 2) * 25f;

//            var middleView = new Listing_Toggles();
//            Rect viewRect = new Rect(0f, 0f, middleRect.width - 16f, middleY);
//            middleView.BeginListing(middleRect, ref scrollPositionMiddle, ref viewRect, middleY);

//            bool optionsEntryFlag = ToggleHandler.IsActive("OptionsEntry");
//            bool optionsPlayFlag = ToggleHandler.IsActive("OptionsPlay");

//            // Draw multi picker.
//            // Only show if any button has been clicked at start.
//            if (!ActiveGroup.NullOrEmpty())
//            {
//                Rect multiRect = new Rect(middleRect.width - 25f, 0f, 25f, 25f);
//                List<Toggle> groupToggles = ToggleHandler.Toggles.Where(x => x.Group.Equals(ActiveGroup)).ToList();
//                bool wasPartial = false;

//                if (groupToggles.All(x => x.active))
//                    state = MultiCheckboxState.On;
//                else if (groupToggles.All(x => !x.active))
//                    state = MultiCheckboxState.Off;
//                else
//                {
//                    state = MultiCheckboxState.Partial;
//                    wasPartial = true;
//                }

//                state = Widgets.CheckboxMulti(multiRect, state);

//                // If partial is clicked, it defaults to off. This workaround turns all on instead, by checking if it was partial before clicking.
//                if (state == MultiCheckboxState.On || (wasPartial && state == MultiCheckboxState.Off))
//                    groupToggles.ForEach(x => x.active = true);
//                else if (state == MultiCheckboxState.Off)
//                    groupToggles.ForEach(x => x.active = false);

//                middleView.Gap(24f);
//            }

//            // Draw toggles in right view depending on what button is active in left view.
//            foreach (Toggle toggle in ToggleHandler.Toggles.Where(x => x.Group.Equals(ActiveGroup)))
//            {
//                middleView.CheckboxLabeled(toggle.PrettyLabel, ref toggle.active);
//            }

//            // Opens confirmation window if user has deactivated the Options button.
//            CheckOptionsActive("Toggles_Entry_Buttons_Options", optionsEntryFlag);
//            CheckOptionsActive("Toggles_Play_Buttons_Options", optionsPlayFlag);

//            middleView.EndListing(ref viewRect);
//        }

//        static void DoLeft(Rect leftRect)
//        {
//            List<string> groupMembers = ToggleHandler.Toggles.Select(x => x.Group).Distinct().ToList();
//            float leftY = (groupMembers.Count() + 8) * 30f;

//            var leftView = new Listing_Toggles();
//            Rect viewRect = new Rect(0f, 0f, leftRect.width, leftRect.height);
//            leftView.BeginListing(leftRect, ref scrollPositionLeft, ref viewRect, leftY);

//            // Sets up category labels in the left view according to each unique toggle root.
//            foreach (string root in ToggleHandler.Toggles.Select(x => x.Root).Distinct())
//            {
//                leftView.Label(root.Translate());
//                // Populates each root label with each respective toggles according to their group.
//                foreach (string group in ToggleHandler.Toggles.Where(x => x.Root.Equals(root)).Select(x => x.Group).Distinct())
//                {
//                    if (ActiveGroup.Equals(group))
//                        GUI.color = ClickedColor;
//                    if (leftView.ButtonText(group.Translate()))
//                        ActiveGroup = group;
//                    GUI.color = DefaultColor;
//                }
//                leftView.Gap();
//            }

//            leftView.EndListing(ref viewRect);
//        }

//        // Asks for confirmation of deactiving Options buttons.
//        static void CheckOptionsActive(string optionsString, bool optionsFlag)
//        {
//            if (optionsFlag != ToggleHandler.IsActive(optionsString) && !ToggleHandler.IsActive(optionsString))
//            {
//                Toggle toggle = ToggleHandler.Toggles.Find(x => x.Label.Equals(optionsString));
//                toggle.active = true;
//                Find.WindowStack.Add(Dialog_MessageBox.CreateConfirmation("DeactivateOptions".Translate(), delegate { toggle.active = false; }, true, null));
//            }
//        }
//    }
//}