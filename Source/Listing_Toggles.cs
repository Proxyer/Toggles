using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Toggles
{
    // Set color, font, size, style, etc.

    public class Listing_Toggles : Listing
    {
        public Listing_Toggles()
        {
            this.font = GameFont.Small;
        }

        //public void BeginScrollView(Rect rect, ref Vector2 scrollPosition, ref Rect viewRect)
        //{
        //Widgets.BeginScrollView(rect, ref scrollPosition, viewRect, true);
        //    rect.height = 100000f;
        //    rect.width -= 20f;
        //    this.Begin(rect.AtZero());
        //}

        public void BeginListing(Rect rect, ref Vector2 scrollPosition, float height)
        {
            Rect viewRect = new Rect(0f, 0f, rect.width - 16f, height);
            Widgets.BeginScrollView(rect, ref scrollPosition, viewRect, true);
            rect.height = height;
            rect.width -= 20f;
            base.Begin(rect.AtZero());
            Text.Font = font;
        }

        public void EndListing()
        {
            Widgets.EndScrollView();
            this.End();
        }

        public void CustomLabel(string label, float maxHeight = -1f, string tooltip = null)
        {
            float num = Text.CalcHeight(label, base.ColumnWidth);
            bool flag = false;
            if (maxHeight >= 0f && num > maxHeight)
            {
                num = maxHeight;
                flag = true;
            }
            Rect rect = base.GetRect(num);
            if (flag)
            {
                Vector2 labelScrollbarPosition = this.GetLabelScrollbarPosition(this.curX, this.curY);
                Widgets.LabelScrollable(rect, label, ref labelScrollbarPosition, false, true);
                this.SetLabelScrollbarPosition(this.curX, this.curY, labelScrollbarPosition);
            }
            else
            {
                Widgets.Label(rect, label);
            }
            if (tooltip != null)
            {
                TooltipHandler.TipRegion(rect, tooltip);
            }
            base.Gap(this.verticalSpacing);
        }

        public void Label(string label, float maxHeight = -1f, string tooltip = null)
        {
            float num = Text.CalcHeight(label, base.ColumnWidth);
            bool flag = false;
            if (maxHeight >= 0f && num > maxHeight)
            {
                num = maxHeight;
                flag = true;
            }
            Rect rect = base.GetRect(num);
            if (flag)
            {
                Vector2 labelScrollbarPosition = this.GetLabelScrollbarPosition(this.curX, this.curY);
                Widgets.LabelScrollable(rect, label, ref labelScrollbarPosition, false, true);
                this.SetLabelScrollbarPosition(this.curX, this.curY, labelScrollbarPosition);
            }
            else
            {
                Widgets.Label(rect, label);
            }
            if (tooltip != null)
            {
                TooltipHandler.TipRegion(rect, tooltip);
            }
            base.Gap(this.verticalSpacing);
        }

        // First entry from GUI.
        public void CheckboxLabeled(string label, string keyGroup, ref bool checkOn, List<FloatMenuOption> floatMenuList = null)
        {
            float lineHeight = Text.LineHeight;
            Rect rect = base.GetRect(lineHeight);

            ToggleCheckbox(rect, label, keyGroup, ref checkOn, floatMenuList);
            base.Gap(this.verticalSpacing);
        }

        // From widgets, painting checkboxlabeled.
        public static void ToggleCheckbox(Rect rect, string label, string keyGroup, ref bool checkOn, List<FloatMenuOption> floatMenuList)
        {
            TextAnchor anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleLeft;

            Widgets.Label(rect, label);
            //Rect leftRectClickable = new Rect(rect.x, rect.y, rect.width - 48f, rect.height);
            Rect rightRectClickable = new Rect(rect.x + rect.width - 24f, rect.y, 24f, rect.height);
            if (Widgets.ButtonInvisible(rightRectClickable, false))
            {
                checkOn = !checkOn;
                if (checkOn)
                {
                    SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
                }
                else
                {
                    SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);
                }
            }

            Color color = Widgets.NormalOptionColor;
            Rect buttonRect = new Rect((rect.x + rect.width) - 96f, rect.y, 72f, 24f);

            bool flag = AnyPressed(ButtonText2(buttonRect, keyGroup, false, false, color, true, true));
            if (flag)
            {
                if (!floatMenuList.NullOrEmpty())
                    MakeFloatMenu(floatMenuList);
            }
            CheckboxDraw(rect.x + rect.width - 24f, rect.y, checkOn, false, 24f, null, null);
            Text.Anchor = anchor;
        }

        static void MakeFloatMenu(List<FloatMenuOption> list)
        {
            Find.WindowStack.Add(new FloatMenu(list));
        }

        public static bool AnyPressed(Widgets.DraggableResult result)
        {
            return result == Widgets.DraggableResult.Pressed || result == Widgets.DraggableResult.DraggedThenPressed;
        }

        private static readonly Color InactiveColor = new Color(0.37f, 0.37f, 0.37f, 0.8f);

        // From widgets. draw.
        private static void CheckboxDraw(float x, float y, bool active, bool disabled, float size = 24f, Texture2D texChecked = null, Texture2D texUnchecked = null)
        {
            Color color = GUI.color;
            if (disabled)
            {
                GUI.color = InactiveColor;
            }
            Texture2D image;
            if (active)
            {
                image = ((!(texChecked != null)) ? Widgets.CheckboxOnTex : texChecked);
            }
            else
            {
                image = ((!(texUnchecked != null)) ? Widgets.CheckboxOffTex : texUnchecked);
            }
            Rect position = new Rect(x, y, size, size);
            GUI.DrawTexture(position, image);
            if (disabled)
            {
                GUI.color = color;
            }
        }

        // From Widgets. Actually ButtonTextWorker.
        private static Widgets.DraggableResult ButtonText2(Rect rect, string label, bool drawBackground, bool doMouseoverSound, Color textColor, bool active, bool draggable)
        {
            if (doMouseoverSound)
            {
                MouseoverSounds.DoRegion(rect);
            }
            Texture2D atlas = Widgets.ButtonSubtleAtlas;
            //Widgets.DrawAtlas(rect, atlas);

            Color color = GUI.color;
            GUI.color = textColor;
            if (Mouse.IsOver(rect))
            {
                //GUI.color = Widgets.MouseoverOptionColor;
                Widgets.DrawAtlas(rect, atlas);
            }

            TextAnchor anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleRight;

            bool wordWrap = Text.WordWrap;
            if (rect.height < Text.LineHeight * 2f)
            {
                Text.WordWrap = false;
            }
            Widgets.Label(rect, label);
            Text.Anchor = anchor;
            GUI.color = color;
            Text.WordWrap = wordWrap;
            if (active && draggable)
            {
                return Widgets.ButtonInvisibleDraggable(rect, false);
            }
            if (active)
            {
                return (!Widgets.ButtonInvisible(rect, false)) ? Widgets.DraggableResult.Idle : Widgets.DraggableResult.Pressed;
            }
            return Widgets.DraggableResult.Idle;
        }

        public MultiCheckboxState MultiCheckBoxLabel(MultiCheckboxState state, List<FloatMenuOption> floatMenuList, string keyGroup = "")
        {
            float lineHeight = Text.LineHeight;
            Rect rect = base.GetRect(lineHeight);

            base.Gap(this.verticalSpacing);
            return CheckboxMulti(rect, state, floatMenuList, keyGroup);
        }

        public static MultiCheckboxState CheckboxMulti(Rect rect, MultiCheckboxState state, List<FloatMenuOption> floatMenuList, string keyGroup, bool paintable = false)
        {
            bool checkboxPainting = false;
            bool checkboxPaintingState = false;

            Texture2D tex;
            if (state == MultiCheckboxState.On)
            {
                tex = Widgets.CheckboxOnTex;
            }
            else if (state == MultiCheckboxState.Off)
            {
                tex = Widgets.CheckboxOffTex;
            }
            else
            {
                tex = Widgets.CheckboxPartialTex;
            }

            // Test Rect for the tex itself, instead of entire label elements.
            Rect texRect = new Rect(rect.x + rect.width - 24f, rect.y, 24f, 24f);
            Color color = Widgets.NormalOptionColor;
            Rect buttonRect = new Rect((rect.x + rect.width) - 96f, rect.y, 72f, 24f);

            bool pressed = AnyPressed(ButtonText2(buttonRect, keyGroup, false, false, color, true, true));
            if (pressed)
            {
                if (!floatMenuList.NullOrEmpty())
                    MakeFloatMenu(floatMenuList);
            }

            MouseoverSounds.DoRegion(texRect);
            MultiCheckboxState multiCheckboxState = (state != MultiCheckboxState.Off) ? MultiCheckboxState.Off : MultiCheckboxState.On;
            bool flag = false;
            Widgets.DraggableResult draggableResult = Widgets.ButtonImageDraggable(texRect, tex);
            if (paintable && draggableResult == Widgets.DraggableResult.Dragged)
            {
                checkboxPainting = true;
                checkboxPaintingState = (multiCheckboxState == MultiCheckboxState.On);
                flag = true;
            }
            else if (AnyPressed(draggableResult))
            {
                flag = true;
            }
            else if (paintable && checkboxPainting && Mouse.IsOver(texRect))
            {
                multiCheckboxState = ((!checkboxPaintingState) ? MultiCheckboxState.Off : MultiCheckboxState.On);
                if (state != multiCheckboxState)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                if (multiCheckboxState == MultiCheckboxState.On)
                {
                    SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
                }
                else
                {
                    SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);
                }
                return multiCheckboxState;
            }
            return state;
        }

        public bool ButtonText(string label, string highlightTag = null)
        {
            Rect rect = base.GetRect(30f);
            bool result = Widgets.ButtonText(rect, label, true, false, true);
            if (highlightTag != null)
            {
                UIHighlighter.HighlightOpportunity(rect, highlightTag);
            }
            base.Gap(this.verticalSpacing);
            return result;
        }

        public bool CustomButtonText(string label, string highlightTag = null)
        {
            Rect rect = base.GetRect(30f);
            bool result = Widgets.ButtonText(rect, label, false, true, true);
            if (highlightTag != null)
            {
                UIHighlighter.HighlightOpportunity(rect, highlightTag);
            }
            base.Gap(this.verticalSpacing);
            return result;
        }

        public bool CustomButtonTextSelectable(string label, ref bool selected, ref bool checkOn)
        {
            float lineHeight = Text.LineHeight;
            Rect rect = base.GetRect(lineHeight);
            bool result = Widgets.CheckboxLabeledSelectable(rect, label, ref selected, ref checkOn);
            base.Gap(this.verticalSpacing);
            return result;
        }

        Vector2 GetLabelScrollbarPosition(float x, float y)
        {
            if (this.labelScrollbarPositions == null)
            {
                return Vector2.zero;
            }
            for (int i = 0; i < this.labelScrollbarPositions.Count; i++)
            {
                Vector2 first = this.labelScrollbarPositions[i].First;
                if (first.x == x && first.y == y)
                {
                    return this.labelScrollbarPositions[i].Second;
                }
            }
            return Vector2.zero;
        }

        void SetLabelScrollbarPosition(float x, float y, Vector2 scrollbarPosition)
        {
            if (this.labelScrollbarPositions == null)
            {
                this.labelScrollbarPositions = new List<Pair<Vector2, Vector2>>();
                this.labelScrollbarPositionsSetThisFrame = new List<Vector2>();
            }
            this.labelScrollbarPositionsSetThisFrame.Add(new Vector2(x, y));
            for (int i = 0; i < this.labelScrollbarPositions.Count; i++)
            {
                Vector2 first = this.labelScrollbarPositions[i].First;
                if (first.x == x && first.y == y)
                {
                    this.labelScrollbarPositions[i] = new Pair<Vector2, Vector2>(new Vector2(x, y), scrollbarPosition);
                    return;
                }
            }
            this.labelScrollbarPositions.Add(new Pair<Vector2, Vector2>(new Vector2(x, y), scrollbarPosition));
        }

        GameFont font;

        List<Pair<Vector2, Vector2>> labelScrollbarPositions;

        List<Vector2> labelScrollbarPositionsSetThisFrame;
    }
}