using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace Toggles.Source
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

        public void CheckboxLabeled(string label, ref bool checkOn, string tooltip = null)
        {
            float lineHeight = Text.LineHeight;
            Rect rect = base.GetRect(lineHeight);
            if (!tooltip.NullOrEmpty())
            {
                if (Mouse.IsOver(rect))
                {
                    Widgets.DrawHighlight(rect);
                }
                TooltipHandler.TipRegion(rect, tooltip);
            }
            Widgets.CheckboxLabeled(rect, label, ref checkOn, false, null, null, false);
            base.Gap(this.verticalSpacing);
        }

        //public void MultiCheckBoxLabel()
        //{
        //    float lineHeight = Text.LineHeight;
        //    Rect rect = base.GetRect(lineHeight);

        //    MultiCheckBoxLabelDo(rect, label, ref checkOn, false, null, null, false);
        //    base.Gap(this.verticalSpacing);
        //}

        //public void MultiCheckBoxLabelDo(Rect rect, string label, ref bool checkOn, bool disabled = false, Texture2D texChecked = null, Texture2D texUnchecked = null, bool placeCheckboxNearText = false)
        //{
        //    TextAnchor anchor = Text.Anchor;
        //    Text.Anchor = TextAnchor.MiddleLeft;
        //    if (placeCheckboxNearText)
        //    {
        //        rect.width = Mathf.Min(rect.width, Text.CalcSize(label).x + 24f + 10f);
        //    }
        //    Widgets.Label(rect, label);
        //    if (!disabled && Widgets.ButtonInvisible(rect, false))
        //    {
        //        checkOn = !checkOn;
        //        if (checkOn)
        //        {
        //            SoundDefOf.Checkbox_TurnedOn.PlayOneShotOnCamera(null);
        //        }
        //        else
        //        {
        //            SoundDefOf.Checkbox_TurnedOff.PlayOneShotOnCamera(null);
        //        }
        //    }
        //    //Widgets.CheckboxDraw(rect.x + rect.width - 24f, rect.y, checkOn, disabled, 24f, null, null);
        //    Text.Anchor = anchor;
        //}

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