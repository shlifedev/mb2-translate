// Decompiled with JetBrains decompiler
// Type: TaleWorlds.GauntletUI.TextWidget
// Assembly: TaleWorlds.GauntletUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E6088FFC-2DEF-43EB-AE11-0D35CA37F638
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.GauntletUI.dll

using System.Numerics;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
    public class CustomTextWidget : TextWidget
    {
        private readonly TaleWorlds.TwoDimension.Text _text;

        public bool AutoHideIfEmpty { get; set; }

        [Editor(false)]
        public string Text
        {
            get
            {
                return this._text.Value;
            }
            set
            {
                if (!(this._text.Value != value))
                    return;
                this.SetText(value);
            }
        }

        [Editor(false)]
        public int IntText
        {
            get
            {
                int result;
                return int.TryParse(this._text.Value, out result) ? result : -1;
            }
            set
            {
                if (!(this._text.Value != value.ToString()))
                    return;
                this.SetText(value.ToString());
            }
        }

        [Editor(false)]
        public float FloatText
        {
            get
            {
                float result;
                return float.TryParse(this._text.Value, out result) ? result : -1f;
            }
            set
            {
                if (!(this._text.Value != value.ToString()))
                    return;
                this.SetText(value.ToString());
            }
        }

        public CustomTextWidget(UIContext context)
          : base(context)
        {
            this._text = new TaleWorlds.TwoDimension.Text((int)this.Size.X, (int)this.Size.Y, context.FontFactory.DefaultFont);
            this.LayoutImp = (ILayout)new TextLayout((IText)this._text);
        }

        private void SetText(string value)
        {
            this.SetMeasureAndLayoutDirty();
            this._text.Value = value;
            this.OnPropertyChanged((object)this.FloatText, "FloatText");
            this.OnPropertyChanged((object)this.IntText, "IntText");
            this.OnPropertyChanged((object)this.Text, "Text");
            this.RefreshTextParameters();
            if (!this.AutoHideIfEmpty)
                return;
            this.IsVisible = !string.IsNullOrEmpty(this.Text);
        }

        private void RefreshTextParameters()
        {
            float num = (float) this.Brush.FontSize * this.Context.Scale;
            this._text.HorizontalAlignment = this.Brush.TextHorizontalAlignment;
            this._text.VerticalAlignment = this.Brush.TextVerticalAlignment;
            this._text.FontSize = num;
            if (this.Brush.Font != null)
                this._text.Font = this.Context.FontFactory.GetMappedFontForLocalization(this.Brush.Font.Name);
            else
                this._text.Font = this.Context.FontFactory.DefaultFont;
        }


        protected override void OnRender(
          TwoDimensionContext twoDimensionContext,
          TwoDimensionDrawContext drawContext)
        {
            this.RefreshTextParameters();
            TextMaterial textMaterial = this.BrushRenderer.CreateTextMaterial(drawContext);
            Vector2 globalPosition = this.GlobalPosition;
            drawContext.Draw(this._text, textMaterial, globalPosition.X, globalPosition.Y, this.Size.X, this.Size.Y);
        }
    }
}
