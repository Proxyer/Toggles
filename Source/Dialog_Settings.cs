using RimWorld;
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

            DoResetButton(mainRect);
        }

        // Button for reset.
        static void DoResetButton(Rect mainRect)
        {
            Rect resetRect = new Rect(0f, mainRect.height + 40f, 120f, 40f);
            if (Widgets.ButtonText(resetRect, "ResetButton".Translate(), true, false, true))
                ToggleManager.Reset();
        }

        // Right view
        static void DoRight(Rect rightRect)
        {
            // Logged letters
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

            // Alert me later
            if (ActiveGroup.Equals(ButtonCat.Alerts))
            {
                float rightY = 10 * 25f;
                var rightView = new Listing_Toggles();
                rightView.BeginListing(rightRect, ref scrollPositionRight, rightY);

                rightView.CustomLabel("AlertMeLater".Translate());
                rightView.GapLine();

                int alertSleepHours = AlertsReadout_Patch.hourMultiplier;
                rightView.CustomLabel("AlertSleepCount".Translate(alertSleepHours), -1, "AlertMeLaterTooltip".Translate(alertSleepHours));
                AlertsReadout_Patch.hourMultiplier = Mathf.RoundToInt(rightView.Slider((float)alertSleepHours, 1f, 24f)); ;
                rightView.EndListing();
            }
        }

        // Middle view
        static void DoMiddle(Rect middleRect)
        {
            if (!ActiveGroup.NullOrEmpty())
            {
                if (ActiveGroup.Equals(ButtonCat.MetaModSettings))
                    DoMetaSettingsView(middleRect);
                else
                    DoToggleView(middleRect);
            }
        }

        // View of standard toggles, with multipicker.
        static void DoToggleView(Rect middleRect)
        {
            List<Toggle> groupToggles = ToggleManager.Toggles.Where(x => x.Group.Equals(ActiveGroup)).ToList();
            float middleY = (groupToggles.Count() + 5) * 25f;
            var middleView = new Listing_Toggles();
            middleView.BeginListing(middleRect, ref scrollPositionMiddle, middleY);

            // Establishes references for checking if Option buttons are disabled further down.
            string optionsEntryButton = $"{ButtonCat.ButtonsEntry}_Options";
            string optionsPlayButton = $"{ButtonCat.ButtonsPlay}_Options";
            bool optionsEntryFlag = ToggleManager.IsActive(optionsEntryButton);
            bool optionsPlayFlag = ToggleManager.IsActive(optionsPlayButton);

            // Draw multi picker.
            // Only show if any button has been clicked at start.
            if (!ActiveGroup.NullOrEmpty())
            {
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

                state = middleView.MultiCheckBoxLabel(state, GetHotkeyFloatOptions(groupToggles), "Hotkey".Translate(), ActiveGroup.Translate(), "Test".Translate());

                // If partial is clicked, it defaults to off. This workaround turns all on instead, by checking if it was partial before clicking.
                if (state == MultiCheckboxState.On || (wasPartial && state == MultiCheckboxState.Off))
                    groupToggles.ForEach(x => x.active = true);
                else if (state == MultiCheckboxState.Off)
                    groupToggles.ForEach(x => x.active = false);
            }

            middleView.GapLine();

            // Draw toggles in middle view depending on what button is active in left view.
            foreach (Toggle toggle in groupToggles.OrderBy(x => x.PrettyLabel))
                middleView.CheckboxLabeled(toggle.PrettyLabel, toggle.PrettyHotkey, ref toggle.active, GetHotkeyFloatOptions(toggle));

            // Opens confirmation window if user has deactivated the Options button.
            CheckOptionsActive(optionsEntryButton, optionsEntryFlag);
            CheckOptionsActive(optionsPlayButton, optionsPlayFlag);

            middleView.EndListing();
        }

        static List<FloatMenuOption> GetHotkeyFloatOptions(Toggle toggle)
        {
            List<FloatMenuOption> list = new List<FloatMenuOption>();
            list.Add(new FloatMenuOption("None", delegate () { toggle.Hotkey = string.Empty; }));

            foreach (Hotkey hotkey in HotkeyHandler.HotKeyDict.Values)
                list.Add(new FloatMenuOption(hotkey.CustomLabel, delegate () { toggle.Hotkey = hotkey.Def.defName; }));

            return list;
        }

        static List<FloatMenuOption> GetHotkeyFloatOptions(List<Toggle> toggles)
        {
            List<FloatMenuOption> list = new List<FloatMenuOption>();

            list.Add(new FloatMenuOption("None", delegate ()
            {
                toggles.ForEach(toggle => toggle.Hotkey = string.Empty);
            }));
            foreach (Hotkey hotkey in HotkeyHandler.HotKeyDict.Values)
                list.Add(new FloatMenuOption(hotkey.CustomLabel, delegate ()
                {
                    toggles.ForEach(toggle => toggle.Hotkey = hotkey.Def.defName);
                }));

            return list;
        }

        static void DoLeft(Rect leftRect)
        {
            //List<string> rootMembers = ToggleManager.Toggles.Select(x => x.Root).Distinct().ToList();
            //float leftY = (rootMembers.Count() + 20) * 30f;
            float leftY = 25 * 30f;

            var leftView = new Listing_Toggles();
            leftView.BeginListing(leftRect, ref scrollPositionLeft, leftY);

            // Hotkey button
            MarkButton(leftView, ButtonCat.MetaModSettings);

            leftView.GapLine(24);

            // Sets up category labels in the left view according to each unique toggle root.
            foreach (string root in ToggleManager.Toggles.Select(x => x.Root).Distinct().OrderBy(x => x))
            {
                leftView.Label(root.Translate());
                // Populates each root label with each respective toggles according to their group.
                foreach (string group in ToggleManager.Toggles.Where(x => x.Root.Equals(root)).Select(x => x.Group).Distinct().OrderBy(x => x))
                    MarkButton(leftView, group);

                leftView.Gap();
            }

            leftView.EndListing();
        }

        static void MarkButton(Listing_Toggles view, string group)
        {
            if (ActiveGroup.Equals(group))
                GUI.color = ClickedColor;
            if (view.ButtonText(group.Translate()))
                ActiveGroup = group;
            GUI.color = DefaultColor;
        }

        static void DoMetaSettingsView(Rect middleRect)
        {
            float middleY = 10 * 25f;

            var view = new Listing_Toggles();
            view.BeginListing(middleRect, ref scrollPositionMiddle, middleY);

            view.Label("HotkeysHeader".Translate());
            foreach (string defName in HotkeyHandler.HotKeyDict.Keys)
            {
                Hotkey hotkey = HotkeyHandler.HotKeyDict.TryGetValue(defName);
                hotkey.CustomLabel = view.TextEntry(hotkey.CustomLabel);
            }
            view.Gap();
            if (view.ButtonText("KeyBindingsButton".Translate(), width: 100f))
                Find.WindowStack.Add(new Dialog_KeyBindings());
            view.EndListing();
        }

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