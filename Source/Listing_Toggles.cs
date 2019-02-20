using System.Collections.Generic;
using UnityEngine;
using Verse;

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

        public void BeginScrollView(Rect rect, ref Vector2 scrollPosition)
        {
            base.Begin(rect);
            Text.Font = font;

            leftViewRect = new Rect(0f, 0f, 200f, 5000f);
            scrollPosition = GUI.BeginScrollView(rect, scrollPosition, leftViewRect);
        }

        Rect leftViewRect;

        public void EndScrollView()
        {
            //leftViewRect.height = leftY;
            GUI.EndScrollView();
            this.End();
        }

        public override void End()
        {
            base.End();
            //if (this.labelScrollbarPositions != null)
            //{
            //    for (int i = this.labelScrollbarPositions.Count - 1; i >= 0; i--)
            //    {
            //        if (!this.labelScrollbarPositionsSetThisFrame.Contains(this.labelScrollbarPositions[i].First))
            //        {
            //            this.labelScrollbarPositions.RemoveAt(i);
            //        }
            //    }
            //    this.labelScrollbarPositionsSetThisFrame.Clear();
            //}
        }

        //public void EndScrollView(ref Rect viewRect)
        //{
        //    viewRect = new Rect(0f, 0f, this.listingRect.width, this.curY);
        //    Widgets.EndScrollView();
        //    this.End();
        //}

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