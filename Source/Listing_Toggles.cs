using RimWorld;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Toggles
{
    internal class Listing_Toggles : Listing
    {
        internal Listing_Toggles()
        {
            font = GameFont.Small;
        }

        internal void BeginListing(Rect rect, ref Vector2 scrollPosition, float height)
        {
            Rect viewRect = new Rect(0f, 0f, rect.width - 16f, height);
            Widgets.BeginScrollView(rect, ref scrollPosition, viewRect, true);
            rect.height = height;
            rect.width -= 20f;
            Rect newRect = new Rect(rect);
            //newRect.x = 10f;
            //newRect.width = rect.width - 20f;
            //newRect.xMax = newRect.width - 20f;
            base.Begin(rect.AtZero());
            Text.Font = font;
        }

        internal void EndListing()
        {
            Widgets.EndScrollView();
            End();
        }

        internal void CustomLabel(string label, float maxHeight = -1f, string tooltip = null)
        {
            float num = Text.CalcHeight(label, base.ColumnWidth);
            bool flag = false;
            if (maxHeight >= 0f && num > maxHeight)
            {
                num = maxHeight;
                flag = true;
            }
            Rect rect = base.GetRect(num);
            rect.x += 10f;
            rect.width -= 20f;
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

        internal void Label(string label, float maxHeight = -1f, string tooltip = null)
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

        internal string TextEntry(string label)
        {
            float lineHeight = Text.LineHeight;
            Rect rect = GetRect(lineHeight);
            rect.width = rect.width / 3;
            rect.x += 10f;

            Gap(verticalSpacing);

            //TextAnchor anchor = Text.Anchor;
            //Text.Anchor = TextAnchor.MiddleLeft;
            //string newLabel = Widgets.TextField(rect, label);
            if (label.Length < 11)
                return Widgets.TextField(rect, label);
            return Widgets.TextField(rect, label.Substring(0, 10));
        }

        internal float Slider(float val, float min, float max)
        {
            Rect rect = base.GetRect(22f);
            rect.x += 10f;
            rect.width -= 20f;
            float result = Widgets.HorizontalSlider(rect, val, min, max, false, null, null, null, -1f);
            base.Gap(this.verticalSpacing);
            return result;
        }

        internal void CheckboxLabeled(string label, string keyGroup, ref bool checkOn, List<FloatMenuOption> floatMenuList = null)
        {
            float lineHeight = Text.LineHeight;
            Rect rect = GetRect(lineHeight);
            rect.x += 10f;
            rect.width -= 10f;

            Gap(verticalSpacing);

            TextAnchor anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleLeft;

            Widgets.Label(rect, label);
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
            rect.x -= 10f;
            MakeFloatMenu(rect, keyGroup, floatMenuList);

            CheckboxDraw(rect.x + rect.width - 24f, rect.y, checkOn, false, 24f, null, null);
            Text.Anchor = anchor;
        }

        // FLOAT MENU
        static void MakeFloatMenu(Rect rect, string keyGroup, List<FloatMenuOption> floatMenuList)
        {
            Color color = Widgets.NormalOptionColor;
            Rect keyGroupRect = new Rect(rect.width * 2 / 3, rect.y, (rect.width / 3) - texSize.x, texSize.y);

            bool pressed = KeyGroupButton(keyGroupRect, keyGroup, floatMenuList, color);
            if (pressed)
            {
                if (!floatMenuList.NullOrEmpty())
                    Find.WindowStack.Add(new FloatMenu(floatMenuList));
            }
        }

        public static bool AnyPressed(Widgets.DraggableResult result)
        {
            return result == Widgets.DraggableResult.Pressed || result == Widgets.DraggableResult.DraggedThenPressed;
        }

        static readonly Color InactiveColor = new Color(0.37f, 0.37f, 0.37f, 0.8f);

        static void CheckboxDraw(float x, float y, bool active, bool disabled, float size = 24f, Texture2D texChecked = null, Texture2D texUnchecked = null)
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

        static Vector2 texSize = new Vector2(24f, 24f);

        internal MultiCheckboxState MultiCheckBoxLabel(MultiCheckboxState state, List<FloatMenuOption> floatMenuList, string keyGroup, bool paintable = false)
        {
            float lineHeight = Text.LineHeight;
            Rect rect = base.GetRect(lineHeight);
            base.Gap(this.verticalSpacing);
            rect.x += 10f;
            rect.width -= 10f;

            Texture2D tex;
            if (state == MultiCheckboxState.On)
                tex = Widgets.CheckboxOnTex;
            else if (state == MultiCheckboxState.Off)
                tex = Widgets.CheckboxOffTex;
            else
                tex = Widgets.CheckboxPartialTex;

            Color color = Widgets.NormalOptionColor;

            // Checkbox
            Rect texRect = new Rect(rect.width - texSize.x, rect.y, texSize.x, texSize.y);
            MouseoverSounds.DoRegion(texRect);

            // KeyGroup Button
            MakeFloatMenu(rect, keyGroup, floatMenuList);

            MultiCheckboxState stateCheck = (state != MultiCheckboxState.Off) ? MultiCheckboxState.Off : MultiCheckboxState.On;

            if (AnyPressed(Widgets.ButtonImageDraggable(texRect, tex)))
            {
                if (stateCheck == MultiCheckboxState.On)
                    SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
                else
                    SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);
                return stateCheck;
            }
            return state;
        }

        static bool KeyGroupButton(Rect rect, string keyGroup, List<FloatMenuOption> floatMenuList, Color textColor)
        {
            MouseoverSounds.DoRegion(rect);

            //Texture2D atlas = Widgets.ButtonSubtleAtlas;
            Texture2D atlas = TexUI.HighlightTex;

            Color color = GUI.color;
            GUI.color = textColor;

            if (Mouse.IsOver(rect))
                Widgets.DrawAtlas(rect, atlas);

            TextAnchor anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleRight;

            Widgets.Label(rect, keyGroup);
            Text.Anchor = anchor;
            GUI.color = color;

            Widgets.DraggableResult result = (!Widgets.ButtonInvisible(rect, false)) ? Widgets.DraggableResult.Idle : Widgets.DraggableResult.Pressed;

            return result == Widgets.DraggableResult.Pressed || result == Widgets.DraggableResult.DraggedThenPressed;
        }

        public bool ButtonText(string label, string highlightTag = null, float width = -1)
        {
            Rect rect = base.GetRect(30f);

            if (width >= 0)
                rect.width = width;
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