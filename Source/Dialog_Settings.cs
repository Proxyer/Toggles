using System.Collections.Generic;
using System.Linq;
using Toggles.Hotkeys;
using Toggles.Patches;
using UnityEngine;
using Verse;

namespace Toggles
{
    // Draws settings window.
    internal static class Dialog_Settings
    {
        static string ActiveGroup { get; set; } = string.Empty;

        static Color ClickedColor { get; } = new Color(0.55f, 1f, 0.55f);
        static Color DefaultColor { get; } = GUI.color;

        static Vector2 scrollPositionLeft;
        static Vector2 scrollPositionMiddle;
        static Vector2 scrollPositionRight;

        static MultiCheckboxState state;

        internal static void DoWindowContents(Rect mainRect)
        {
            Rect leftRect = new Rect(mainRect.x, mainRect.y, mainRect.width / 4, mainRect.height);
            DoLeft(leftRect);

            Rect middleRect = new Rect(leftRect.width, mainRect.y, (mainRect.width - leftRect.width) / 2, mainRect.height);
            DoMiddle(middleRect);
            Rect rightRect = new Rect(middleRect.xMax, mainRect.y, (mainRect.width - leftRect.width) / 2, mainRect.height);
            DoRight(rightRect);

            // Button for reset.
            Rect resetRect = new Rect(0f, mainRect.height + 40f, 120f, 40f);
            if (Widgets.ButtonText(resetRect, "ResetButton".Translate(), true, false, true))
                ToggleManager.Reset();
        }

        static void DoRight(Rect rightRect)
        {
            if (ActiveGroup.Equals(ButtonCat.Letters))
            {
                List<string> loggedLetters = Letter_Patch.LoggedLetters;

                float rightY = (loggedLetters.Count() + 2) * 25f;

                var rightView = new Listing_Toggles();
                rightView.BeginListing(rightRect, ref scrollPositionRight, rightY);

                rightView.CustomLabel("LoggedLetters".Translate());

                foreach (string letter in loggedLetters)
                    if (rightView.CustomButtonText(letter))
                        Letter_Patch.AddRawLetter(letter);

                rightView.EndListing();
            }
        }

        static void DoMiddle(Rect middleRect)
        {
            List<Toggle> groupToggles = ToggleManager.Toggles.Where(x => x.Group.Equals(ActiveGroup)).ToList();
            float middleY = (groupToggles.Count() + 2) * 25f;

            var middleView = new Listing_Toggles();
            middleView.BeginListing(middleRect, ref scrollPositionMiddle, middleY);

            // Establishes references for checking if Option buttons are disabled further down.
            string optionsEntryButton = "ButtonsEntry_Options";
            string optionsPlayButton = "ButtonsPlay_Options";
            bool optionsEntryFlag = ToggleManager.IsActive(optionsEntryButton);
            bool optionsPlayFlag = ToggleManager.IsActive(optionsPlayButton);

            // Draw multi picker.
            // Only show if any button has been clicked at start.
            if (!ActiveGroup.NullOrEmpty())
            {
                Rect multiRect = new Rect(middleRect.width - 45f, 0f, 25f, 25f);

                bool wasPartial = false;

                if (groupToggles.All(x => x.active))
                    state = MultiCheckboxState.On;
                else if (groupToggles.All(x => !x.active))
                    state = MultiCheckboxState.Off;
                else
                {
                    state = MultiCheckboxState.Partial;
                    wasPartial = true;
                }

                // Checks if keygroup is the same for all toggles in activegroup. If not, remain empty, otherwise, set to that value.
                string keyGroup = string.Empty;
                if (groupToggles.Distinct().Skip(1).All(x => x.KeyGroup.Equals(groupToggles.First().KeyGroup)))
                    keyGroup = groupToggles.First().KeyGroup;
                //else if (!groupToggles.All(x => groupToggles.First().KeyGroup.Equals(x.KeyGroup)))
                //    keyGroup = "______";

                state = middleView.MultiCheckBoxLabel(state, GetHotkeyFloatOptions(groupToggles), keyGroup);

                // If partial is clicked, it defaults to off. This workaround turns all on instead, by checking if it was partial before clicking.
                if (state == MultiCheckboxState.On || (wasPartial && state == MultiCheckboxState.Off))
                    groupToggles.ForEach(x => x.active = true);
                else if (state == MultiCheckboxState.Off)
                    groupToggles.ForEach(x => x.active = false);
            }

            // Draw toggles in middle view depending on what button is active in left view.
            foreach (Toggle toggle in groupToggles)
                middleView.CheckboxLabeled(toggle.PrettyLabel, toggle.KeyGroup, ref toggle.active, GetHotkeyFloatOptions(toggle));

            // Opens confirmation window if user has deactivated the Options button.
            CheckOptionsActive(optionsEntryButton, optionsEntryFlag);
            CheckOptionsActive(optionsPlayButton, optionsPlayFlag);

            middleView.EndListing();
        }

        static List<FloatMenuOption> GetHotkeyFloatOptions(Toggle toggle)
        {
            List<FloatMenuOption> list = new List<FloatMenuOption>();
            list.Add(new FloatMenuOption("None", delegate () { toggle.KeyGroup = ""; }));
            foreach (string keyGroup in KeyBindingHandler.Hotkeys.Select(x => x.label))
                list.Add(new FloatMenuOption(keyGroup, delegate () { toggle.KeyGroup = keyGroup; }));

            return list;
        }

        static List<FloatMenuOption> GetHotkeyFloatOptions(List<Toggle> toggles)
        {
            List<FloatMenuOption> list = new List<FloatMenuOption>();
            list.Add(new FloatMenuOption("None", delegate ()
            {
                toggles.ForEach(toggle => toggle.KeyGroup = "");
            }));
            foreach (string keyGroup in KeyBindingHandler.Hotkeys.Select(x => x.label))
                list.Add(new FloatMenuOption(keyGroup, delegate ()
                {
                    toggles.ForEach(toggle => toggle.KeyGroup = keyGroup);
                }));

            return list;
        }

        static void DoLeft(Rect leftRect)
        {
            List<string> rootMembers = ToggleManager.Toggles.Select(x => x.Root).Distinct().ToList();
            float leftY = (rootMembers.Count() + 14) * 30f;

            var leftView = new Listing_Toggles();
            leftView.BeginListing(leftRect, ref scrollPositionLeft, leftY);

            // Sets up category labels in the left view according to each unique toggle root.
            foreach (string root in ToggleManager.Toggles.Select(x => x.Root).Distinct())
            {
                leftView.Label(root.Translate());
                // Populates each root label with each respective toggles according to their group.
                foreach (string group in ToggleManager.Toggles.Where(x => x.Root.Equals(root)).Select(x => x.Group).Distinct())
                {
                    if (ActiveGroup.Equals(group))
                        GUI.color = ClickedColor;
                    if (leftView.ButtonText(group.Translate()))
                        ActiveGroup = group;
                    GUI.color = DefaultColor;
                }
                leftView.Gap();
            }

            leftView.EndListing();

            //testString = Widgets.TextField(new Rect(leftRect.x, leftRect.yMax - 25f, 50f, 24f), testString);
            //if (Widgets.ButtonText(new Rect(leftRect.x + 51f, leftRect.yMax - 25f, 50f, 24f), "Click"))
            //    ModBase_Toggles.ChangeBindingLabel(testString);
        }

        //static string testString = "Test";

        // Asks for confirmation of deactiving Options buttons.
        static void CheckOptionsActive(string optionsString, bool optionsFlag)
        {
            if (optionsFlag != ToggleManager.IsActive(optionsString) && !ToggleManager.IsActive(optionsString))
            {
                Toggle toggle = ToggleManager.Toggles.Find(x => x.Label.Equals(optionsString));
                toggle.active = true;
                Find.WindowStack.Add(Dialog_MessageBox.CreateConfirmation("DeactivateOptions".Translate(), delegate { toggle.active = false; }, true, null));
            }
        }
    }
}