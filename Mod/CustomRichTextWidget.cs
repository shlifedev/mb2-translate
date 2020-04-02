// Decompiled with JetBrains decompiler
// Type: TaleWorlds.GauntletUI.RichTextWidget
// Assembly: TaleWorlds.GauntletUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E6088FFC-2DEF-43EB-AE11-0D35CA37F638
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.GauntletUI.dll

using System;
using System.Numerics;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
    public class CustomRichTextWidget : RichTextWidget
    {
        private readonly RichText _richText;
        private Brush _lastFontBrush;
        private string _lastLanguageCode;
        private float _lastContextScale;
        private CustomRichTextWidget.MouseState _mouseState;
        private Vector2 _mouseDownPosition;
        private int _textHeight;
        private string _linkHoverCursorState;

        private Vector2 LocalMousePosition
        {
            get
            {
                Vector2 mousePosition = this.EventManager.MousePosition;
                Vector2 globalPosition = this.GlobalPosition;
                return new Vector2(mousePosition.X - globalPosition.X, mousePosition.Y - globalPosition.Y);
            }
        }

        [Editor(false)]
        public string LinkHoverCursorState
        {
            get
            {
                return this._linkHoverCursorState;
            }
            set
            {
                if (!(this._linkHoverCursorState != value))
                    return;
                this._linkHoverCursorState = value;
            }
        }

        [Editor(false)]
        public string Text
        {
            get
            {
                return this._richText.Value;
            }
            set
            {
                if (!(this._richText.Value != value))
                    return;
                this._richText.Value = value;
                this.OnPropertyChanged((object)value, nameof(Text));
                this.SetMeasureAndLayoutDirty();
            }
        }

        public CustomRichTextWidget(UIContext context)
          : base(context)
        {
            FontFactory fontFactory = context.FontFactory;
            this._textHeight = -1;
            this._richText = new RichText((int)this.Size.X, (int)this.Size.Y, this.Context.FontFactory.DefaultFont);
            this._lastFontBrush = (Brush)null;
            this.LayoutImp = (ILayout)new TextLayout((IText)this._richText);
            this.AddState("Pressed");
            this.AddState("Hovered");
            this.AddState("Disabled");
        }

        protected override void OnParallelUpdate(float dt)
        {
            base.OnParallelUpdate(dt);
            this.SetRichTextParameters();
        }

        private void SetRichTextParameters()
        {
            bool flag = false;
            this.UpdateFontData();
            if (this._richText.HorizontalAlignment != this.Brush.TextHorizontalAlignment)
            {
                this._richText.HorizontalAlignment = this.Brush.TextHorizontalAlignment;
                flag = true;
            }
            if (this._richText.VerticalAlignment != this.Brush.TextVerticalAlignment)
            {
                this._richText.VerticalAlignment = this.Brush.TextVerticalAlignment;
                flag = true;
            }
            if (this._richText.TextHeight != this._textHeight)
            {
                this._textHeight = this._richText.TextHeight;
                flag = true;
            }
            if (this._richText.CurrentStyle != this.CurrentState && !string.IsNullOrEmpty(this.CurrentState))
            {
                this._richText.CurrentStyle = this.CurrentState;
                flag = true;
            }
            if (!flag)
                return;
            this.SetMeasureAndLayoutDirty();
        }

        protected override void RefreshState()
        {
            base.RefreshState();
            this.UpdateText();
        }

        private void UpdateText()
        {
            if (this.IsDisabled)
                this.SetState("Disabled");
            else if (this.IsPressed)
                this.SetState("Pressed");
            else if (this.IsHovered)
                this.SetState("Hovered");
            else
                this.SetState("Default");
        }

        private void UpdateFontData()
        {
            if (this._lastFontBrush == this.Brush && (double)this._lastContextScale == (double)this.Context.Scale && this._lastLanguageCode == this.Context.FontFactory.CurrentLangageID)
                return;
            this._richText.StyleFontContainer.ClearFonts();
            foreach (Style style in this.Brush.Styles)
            {
                Font fontForLocalization = this.Context.FontFactory.GetMappedFontForLocalization((style.Font == null ? (this.Brush.Font == null ? this.Context.FontFactory.DefaultFont : this.Brush.Font) : style.Font).Name);
                this._richText.StyleFontContainer.Add(style.Name, fontForLocalization, (float)style.FontSize * this.Context.Scale);
            }
            this._lastFontBrush = this.Brush;
            this._lastLanguageCode = this.Context.FontFactory.CurrentLangageID;
            this._lastContextScale = this.Context.Scale;
        }

        private Font GetFont(Style style = null)
        {
            if (style?.Font != null)
                return this.Context.FontFactory.GetMappedFontForLocalization(style.Font.Name);
            return this.Brush.Font != null ? this.Context.FontFactory.GetMappedFontForLocalization(this.Brush.Font.Name) : this.Context.FontFactory.DefaultFont;
        }

        protected override void OnLateUpdate(float dt)
        {
            base.OnLateUpdate(dt);
            if ((double)this.Size.X <= 0.0 || (double)this.Size.Y <= 0.0)
                return;
            Vector2 focusPosition = this.LocalMousePosition;
            bool focus = this._mouseState == CustomRichTextWidget.MouseState.Down || this._mouseState == CustomRichTextWidget.MouseState.AlternateDown;
            int num = this._mouseState == CustomRichTextWidget.MouseState.Up ? 1 : (this._mouseState == CustomRichTextWidget.MouseState.AlternateUp ? 1 : 0);
            if (focus)
                focusPosition = this._mouseDownPosition;
            RichTextLinkGroup focusedLinkGroup1 = this._richText.FocusedLinkGroup;
            this._richText.UpdateSize((int)this.Size.X, (int)this.Size.Y);

            //if (focusedLinkGroup1 != null && this.LinkHoverCursorState != null)
            //    this.Context.ActiveCursorOfContext = (UIContext.MouseCursors)Enum.Parse(typeof(UIContext.MouseCursors), this.LinkHoverCursorState);

            this.SetRichTextParameters();
            this._richText.Update(this.Context.SpriteData, focusPosition, focus);
            if (num == 0)
                return;
            RichTextLinkGroup focusedLinkGroup2 = this._richText.FocusedLinkGroup;
            if (focusedLinkGroup1 != null && focusedLinkGroup1 == focusedLinkGroup2)
            {
                string href = focusedLinkGroup1.Href;
                string[] strArray = href.Split(':');
                if (strArray.Length == 2)
                    href = strArray[1];
                if (this._mouseState == CustomRichTextWidget.MouseState.Up)
                    this.EventFired("LinkClick", (object)href);
                else if (this._mouseState == CustomRichTextWidget.MouseState.AlternateUp)
                    this.EventFired("LinkAlternateClick", (object)href);
            }
            this._mouseState = CustomRichTextWidget.MouseState.None;
        }

        protected override void OnRender(
          TwoDimensionContext twoDimensionContext,
          TwoDimensionDrawContext drawContext)
        {
            base.OnRender(twoDimensionContext, drawContext);
            if (string.IsNullOrEmpty(this._richText.Value))
                return;
            foreach (RichTextPart part in this._richText.GetParts())
            {
                DrawObject2D drawObject2D = part.DrawObject2D;
                Material material = (Material) null;
                Vector2 globalPosition = this.GlobalPosition;
                if (part.Type == RichTextPartType.Text)
                {
                    Style styleOrDefault = this.Brush.GetStyleOrDefault(part.Style);
                    Font font = this.GetFont(styleOrDefault);
                    float num = (float) styleOrDefault.FontSize * this.Context.Scale;
                    TextMaterial textMaterial1 = styleOrDefault.CreateTextMaterial(drawContext);
                    textMaterial1.ColorFactor *= this.Brush.GlobalColorFactor;
                    textMaterial1.AlphaFactor *= this.Brush.GlobalAlphaFactor;
                    textMaterial1.Color *= this.Brush.GlobalColor;
                    textMaterial1.Texture = font.FontSprite.Texture;
                    textMaterial1.ScaleFactor = num;
                    textMaterial1.SmoothingConstant = font.SmoothingConstant;
                    textMaterial1.Smooth = font.Smooth;
                    if ((double)textMaterial1.GlowRadius > 0.0 || (double)textMaterial1.Blur > 0.0 || (double)textMaterial1.OutlineAmount > 0.0)
                    {
                        TextMaterial textMaterial2 = styleOrDefault.CreateTextMaterial(drawContext);
                        textMaterial2.CopyFrom(textMaterial1);
                        drawContext.Draw(globalPosition.X, globalPosition.Y, (Material)textMaterial2, drawObject2D, this.Size.X, this.Size.Y);
                    }
                    textMaterial1.GlowRadius = 0.0f;
                    textMaterial1.Blur = 0.0f;
                    textMaterial1.OutlineAmount = 0.0f;
                    material = (Material)textMaterial1;
                }
                else if (part.Type == RichTextPartType.Sprite)
                {
                    SimpleMaterial simpleMaterial = new SimpleMaterial(part.Sprite?.Texture);
                    simpleMaterial.ColorFactor *= this.Brush.GlobalColorFactor;
                    simpleMaterial.AlphaFactor *= this.Brush.GlobalAlphaFactor;
                    simpleMaterial.Color *= this.Brush.GlobalColor;
                    material = (Material)simpleMaterial;
                }
                drawContext.Draw(globalPosition.X, globalPosition.Y, material, drawObject2D, this.Size.X, this.Size.Y);
            }
        }

        protected override void OnMousePressed()
        {
            if (this._mouseState != CustomRichTextWidget.MouseState.None)
                return;
            this._mouseDownPosition = this.LocalMousePosition;
            this._mouseState = CustomRichTextWidget.MouseState.Down;
        }

        protected void OnMouseReleased()
        {
            if (this._mouseState != CustomRichTextWidget.MouseState.Down)
                return;
            this._mouseState = CustomRichTextWidget.MouseState.Up;
        }

        protected override void OnMouseAlternatePressed()
        {
            if (this._mouseState != CustomRichTextWidget.MouseState.None)
                return;
            this._mouseDownPosition = this.LocalMousePosition;
            this._mouseState = CustomRichTextWidget.MouseState.AlternateDown;
        }

        protected override void OnMouseAlternateReleased()
        {
            if (this._mouseState != CustomRichTextWidget.MouseState.AlternateDown)
                return;
            this._mouseState = CustomRichTextWidget.MouseState.AlternateUp;
        }

        private enum MouseState
        {
            None,
            Down,
            Up,
            AlternateDown,
            AlternateUp,
        }
    }
}
