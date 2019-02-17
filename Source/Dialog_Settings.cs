using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Toggles
{
    // Draws settings window.
    internal static class Dialog_Settings
    {
        static Dialog_Settings()
        {
            // Copies current settings for reference to changes made.
            RefChanged = new Dictionary<string, bool>();
            ToggleHandler.Toggles.ForEach(x => RefChanged.Add(x.Label, x.active));
        }

        static string ActiveGroup { get; set; } = string.Empty;
        static Dictionary<string, bool> RefChanged { get; set; }
        static Color ClickedColor { get; } = new Color(0.55f, 1f, 0.55f);
        static Color DefaultColor { get; } = GUI.color;

        static Vector2 scrollPositionLeft;
        static Vector2 scrollPositionRight;

        static MultiCheckboxState state;

        internal static void DoWindowContents(Rect mainRect)
        {
            // Determines length of scrollviews. Based on nr of elements in each view, plus some buffer values.
            float leftY = (ToggleHandler.Toggles.Select(x => x.Group).Distinct().Count() + 1) * 30f;
            float rightY = (ToggleHandler.Toggles.Where(x => x.Group.Equals(ActiveGroup)).Count() + 2) * 25f;

            // --------------------
            // Left scrollview.
            // --------------------
            Rect leftRect = new Rect(mainRect.x, mainRect.y, mainRect.width / 2, mainRect.height);
            Rect leftInRect = new Rect(0f, 0f, (leftRect.width / 2) - 20f, leftRect.height);
            Rect leftViewRect = new Rect(0f, 0f, 200f, leftY);
            scrollPositionLeft = GUI.BeginScrollView(leftRect, scrollPositionLeft, leftViewRect);
            var leftView = new Listing_Standard();
            leftView.ColumnWidth = leftInRect.width;
            leftView.Begin(leftInRect);

            // Sets up category labels in the left view according to each unique toggle root.
            foreach (string root in ToggleHandler.Toggles.Select(x => x.Root).Distinct())
            {
                leftView.Label(root.Translate());
                // Populates each root label with each respective toggles according to their group.
                foreach (string group in ToggleHandler.Toggles.Where(x => x.Root.Equals(root)).Select(x => x.Group).Distinct())
                {
                    if (ActiveGroup.Equals(group))
                        GUI.color = ClickedColor;
                    if (leftView.ButtonText(group.CanTranslate() ? group.Translate() : group))
                        ActiveGroup = group;
                    GUI.color = DefaultColor;
                }
                leftView.Gap();
            }
            //
            if (leftView.ButtonText("Incidents gen?"))
            {
                //DebugUtil.Log("Incidents " + DefDatabase<IncidentDef>.AllDefsListForReading.Count.ToString());
            }
            //
            leftViewRect.height = leftY;
            leftView.End();
            GUI.EndScrollView();

            // --------------------
            // Right scrollview.
            // --------------------
            Rect rightRect = new Rect(mainRect.width / 4, mainRect.y, mainRect.width / 2, mainRect.height);
            Rect rightInRect = new Rect(0f, 0f, rightRect.width - 20f, rightY);
            Rect rightViewRect = new Rect(0f, 0f, 200f, rightY);
            scrollPositionRight = GUI.BeginScrollView(rightRect, scrollPositionRight, rightViewRect);
            var rightView = new Listing_Standard();
            rightView.ColumnWidth = rightInRect.width;
            rightView.Begin(rightInRect);

            bool optionsEntryFlag = ToggleHandler.IsActive("OptionsEntry");
            bool optionsPlayFlag = ToggleHandler.IsActive("OptionsPlay");

            // Draw multi picker.
            // Only show if any button has been clicked at start.
            if (!ActiveGroup.NullOrEmpty())
            {
                Rect multiRect = new Rect(rightInRect.width - 25f, 0f, 25f, 25f);
                List<Toggle> groupToggles = ToggleHandler.Toggles.Where(x => x.Group.Equals(ActiveGroup)).ToList();
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

                state = Widgets.CheckboxMulti(multiRect, state);

                // If partial is clicked, it defaults to off. This workaround turns all on instead, by checking if it was partial before clicking.
                if (state == MultiCheckboxState.On || (wasPartial && state == MultiCheckboxState.Off))
                    groupToggles.ForEach(x => x.active = true);
                else if (state == MultiCheckboxState.Off)
                    groupToggles.ForEach(x => x.active = false);

                rightView.Gap(24f);
            }

            // Draw toggles in right view depending on what button is active in left view.
            foreach (Toggle toggle in ToggleHandler.Toggles.Where(x => x.Group.Equals(ActiveGroup)))
            {
                rightView.CheckboxLabeled(toggle.Label.CanTranslate() ? toggle.Label.Translate() : toggle.Label,
                    ref toggle.active,
                    toggle.Description.CanTranslate() ? toggle.Description.Translate() : toggle.Description);
            }

            // Opens confirmation window if user has deactivated the Options button.
            CheckOptionsActive("OptionsEntry", optionsEntryFlag);
            CheckOptionsActive("OptionsPlay", optionsPlayFlag);

            rightView.End();
            GUI.EndScrollView();

            // Button for reset.
            Rect resetRect = new Rect(0f, mainRect.height + 40f, 120f, 40f);
            if (Widgets.ButtonText(resetRect, "ResetButton".Translate(), true, false, true))
                ToggleHandler.Toggles.ForEach(x => x.active = true);

            // --------------------
            CheckChanges();
        }

        // Asks for confirmation of deactiving Options buttons.
        static void CheckOptionsActive(string optionsString, bool optionsFlag)
        {
            if (optionsFlag != ToggleHandler.IsActive(optionsString) && !ToggleHandler.IsActive(optionsString))
            {
                Toggle toggle = ToggleHandler.Toggles.Find(x => x.Label.Equals(optionsString));
                toggle.active = true;
                Find.WindowStack.Add(Dialog_MessageBox.CreateConfirmation("DeactivateOptions".Translate(), delegate { toggle.active = false; }, true, null));
            }
        }

        // Checks whether changes have been made, updates if they have.
        static void CheckChanges()
        {
            foreach (Toggle toggle in ToggleHandler.Toggles)
                if (RefChanged.TryGetValue(toggle.Label) != toggle.active)
                    Update();
        }

        // Updates settings references with new settings, and tells mod to reapply patches according to new settings.
        static void Update()
        {
            RefChanged = new Dictionary<string, bool>();
            ToggleHandler.Toggles.ForEach(x => RefChanged.Add(x.Label, x.active));
            Patcher.DoPatches();
        }
    }
}