//// Decompiled with JetBrains decompiler
//// Type: TaleWorlds.GauntletUI.CustomEditableTextWidget
//// Assembly: TaleWorlds.GauntletUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: E6088FFC-2DEF-43EB-AE11-0D35CA37F638
//// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord\bin\Win64_Shipping_Client\TaleWorlds.GauntletUI.dll

//using System;
//using System.Collections.Generic;
//using System.Numerics;
//using System.Text.RegularExpressions;
//using TaleWorlds.Engine.GauntletUI;
//using TaleWorlds.InputSystem;
//using TaleWorlds.TwoDimension;

//namespace TaleWorlds.GauntletUI
//{
//    public class CustomEditableTextWidget : Widget
//    {
//        private readonly char _obfuscationChar = '*';
//        private float _lastScale = -1f;
//        private string _realText = "";
//        private bool _isObfuscationEnabled;
//        private EditableText _editableText;
//        private CustomEditableTextWidget.MouseState _mouseState;
//        private Brush _lastFontBrush;
//        private Vector2 _mouseDownPosition;
//        private bool _cursorVisible;
//        private int _textHeight;
//        private CustomEditableTextWidget.CursorMovementDirection _cursorDirection;
//        private CustomEditableTextWidget.KeyboardAction _keyboardAction;
//        private int _nextRepeatTime;
//        private bool _isSelection;

//        public bool IsObfuscationEnabled
//        {
//            get
//            {
//                return this._isObfuscationEnabled;
//            }
//            set
//            {
//                if (value == this._isObfuscationEnabled)
//                    return;
//                this._isObfuscationEnabled = value;
//                this.OnObfuscationToggled(value);
//            }
//        }

//        private Vector2 LocalMousePosition
//        {
//            get
//            {
//                Vector2 mousePosition = this.EventManager.MousePosition;
//                Vector2 globalPosition = this.GlobalPosition;
//                return new Vector2(mousePosition.X - globalPosition.X, mousePosition.Y - globalPosition.Y);
//            }
//        }

//        [Editor(false)]
//        public string RealText
//        {
//            get
//            {
//                return this._realText;
//            }
//            set
//            {
//                if (!(this._realText != value))
//                    return;
//                if (string.IsNullOrEmpty(value))
//                    value = "";
//                this.Text = this.IsObfuscationEnabled ? this.ObfuscateText(value) : value;
//                this._realText = value;
//                this.OnPropertyChanged((object)value, nameof(RealText));
//            }
//        }

//        [Editor(false)]
//        public string Text
//        {
//            get
//            {
//                return this._editableText.VisibleText;
//            }
//            set
//            {
//                this._editableText.VisibleText = value;
//                if (string.IsNullOrEmpty(value))
//                {
//                    this._editableText.VisibleText = "";
//                    this._editableText.SetCursor(0, this.IsFocused, false);
//                }
//                this.OnPropertyChanged((object)value, nameof(Text));
//                this.SetMeasureAndLayoutDirty();
//            }
//        }

//        public CustomEditableTextWidget(UIContext context)
//          : base(context)
//        {
//            this._editableText = new EditableText((int)this.Size.X, (int)this.Size.Y, context.FontFactory.DefaultFont);
//            this.LayoutImp = (ILayout)new TextLayout((IText)this._editableText);
//            this._realText = "";
//            this._textHeight = -1;
//            this._cursorVisible = false;
//            this._lastFontBrush = (Brush)null;
//            this._cursorDirection = CustomEditableTextWidget.CursorMovementDirection.None;
//            this._keyboardAction = CustomEditableTextWidget.KeyboardAction.None;
//            this._nextRepeatTime = int.MinValue;
//            this._isSelection = false;
//            this.IsFocusable = true;
//        }

//        protected override void OnUpdate(float dt)
//        {
//            base.OnUpdate(dt);
//            this.UpdateText();
//            if (this.IsFocused)
//            {
//                this._editableText.BlinkTimer += dt;
//                if ((double)this._editableText.BlinkTimer > 0.5)
//                {
//                    this._editableText.BlinkCursor();
//                    this._editableText.BlinkTimer = 0.0f;
//                }
//                if (this.ContainsState("Selected"))
//                    this.SetState("Selected");
//            }
//            this.SetEditTextParameters();
//        }

//        private void SetEditTextParameters()
//        {
//            bool flag = false;
//            if (this._editableText.HorizontalAlignment != this.Brush.TextHorizontalAlignment)
//            {
//                this._editableText.HorizontalAlignment = this.Brush.TextHorizontalAlignment;
//                flag = true;
//            }
//            if (this._editableText.VerticalAlignment != this.Brush.TextVerticalAlignment)
//            {
//                this._editableText.VerticalAlignment = this.Brush.TextVerticalAlignment;
//                flag = true;
//            }
//            if (this._editableText.TextHeight != this._textHeight)
//            {
//                this._textHeight = this._editableText.TextHeight;
//                flag = true;
//            }
//            if (!flag)
//                return;
//            this.SetMeasureAndLayoutDirty();
//        }

//        private void BlinkCursor()
//        {
//            this._cursorVisible = !this._cursorVisible;
//        }

//        private void ResetSelected()
//        {
//            this._editableText.ResetSelected();
//        }

//        private void DeleteChar(bool nextChar = false)
//        {
//            int cursorPosition = this._editableText.CursorPosition;
//            if (nextChar)
//                ++cursorPosition;
//            if (cursorPosition == 0 || cursorPosition > this.Text.Length)
//                return;
//            if (this.IsObfuscationEnabled)
//            {
//                this.RealText = this.RealText.Substring(0, cursorPosition - 1) + this.RealText.Substring(cursorPosition, this.RealText.Length - cursorPosition);
//                this.Text = this.ObfuscateText(this.RealText);
//            }
//            else
//            {
//                this.Text = this.Text.Substring(0, cursorPosition - 1) + this.Text.Substring(cursorPosition, this.Text.Length - cursorPosition);
//                this.RealText = this.Text;
//            }
//            this._editableText.SetCursor(cursorPosition - 1, true, false);
//            this.ResetSelected();
//        }

//        private int FindNextWordPosition(int direction)
//        {
//            return this._editableText.FindNextWordPosition(direction);
//        }

//        private void MoveCursor(int direction, bool withSelection = false)
//        {
//            if (!withSelection)
//                this.ResetSelected();
//            this._editableText.SetCursor(this._editableText.CursorPosition + direction, true, withSelection);
//        }

//        private void AppendCharacter(int charCode)
//        {
//            int cursorPosition = this._editableText.CursorPosition;
//            char ch = Convert.ToChar(charCode);
//            if (this.IsObfuscationEnabled)
//            {
//                this.RealText = this.RealText.Substring(0, cursorPosition) + ch.ToString() + this.RealText.Substring(cursorPosition, this.RealText.Length - cursorPosition);
//                this.Text = this.ObfuscateText(this.RealText);
//            }
//            else
//            {
//                this.Text = this.Text.Substring(0, cursorPosition) + ch.ToString() + this.Text.Substring(cursorPosition, this.Text.Length - cursorPosition);
//                this.RealText = this.Text;
//            }
//            this._editableText.SetCursor(cursorPosition + 1, true, false);
//            this.ResetSelected();
//        }

//        private void AppendText(string text)
//        {
//            int cursorPosition = this._editableText.CursorPosition;
//            if (this.IsObfuscationEnabled)
//            {
//                this.RealText = this.RealText.Substring(0, cursorPosition) + text + this.RealText.Substring(cursorPosition, this.RealText.Length - cursorPosition);
//                this.Text = this.ObfuscateText(this.RealText);
//            }
//            else
//            {
//                this.Text = this.Text.Substring(0, cursorPosition) + text + this.Text.Substring(cursorPosition, this.Text.Length - cursorPosition);
//                this.RealText = this.Text;
//            }
//            this._editableText.SetCursor(cursorPosition + text.Length, true, false);
//            this.ResetSelected();
//        }

//        private void DeleteText(int beginIndex, int endIndex)
//        {
//            if (beginIndex == endIndex)
//                return;
//            if (this.IsObfuscationEnabled)
//            {
//                this.RealText = this.RealText.Substring(0, beginIndex) + this.RealText.Substring(endIndex, this.RealText.Length - endIndex);
//                this.Text = this.ObfuscateText(this.RealText);
//            }
//            else
//            {
//                this.Text = this.Text.Substring(0, beginIndex) + this.Text.Substring(endIndex, this.Text.Length - endIndex);
//                this.RealText = this.Text;
//            }
//            this._editableText.SetCursor(beginIndex, true, false);
//            this.ResetSelected();
//        }

//        private void CopyText(int beginIndex, int endIndex)
//        {
//            if (beginIndex == endIndex)
//                return;
//            Input.SetClipboardText(this.RealText.Substring(beginIndex, endIndex - beginIndex));
//        }

//        private void PasteText()
//        {
//            this.AppendText(Regex.Replace(Input.GetClipboardText(), "[<>]+", " "));
//        }

//        public override void HandleInput(IReadOnlyList<int> lastKeysPressed)
//        {
//            int count = lastKeysPressed.Count;
//            for (int index = 0; index < count; ++index)
//            {
//                int charCode = lastKeysPressed[index];
//                if (charCode >= 32 && (charCode < (int)sbyte.MaxValue || charCode >= 160))
//                {
//                    if (charCode != 60 && charCode != 62)
//                    {
//                        this.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
//                        this.AppendCharacter(charCode);
//                    }
//                    this._cursorDirection = CustomEditableTextWidget.CursorMovementDirection.None;
//                    this._isSelection = false;
//                }
//            }
//            int tickCount = Environment.TickCount;
//            bool flag1 = false;
//            bool flag2 = false;
//            if (Input.IsKeyPressed(InputKey.Left))
//            {
//                this._cursorDirection = CustomEditableTextWidget.CursorMovementDirection.Left;
//                flag1 = true;
//            }
//            else if (Input.IsKeyPressed(InputKey.Right))
//            {
//                this._cursorDirection = CustomEditableTextWidget.CursorMovementDirection.Right;
//                flag1 = true;
//            }
//            else if (this._cursorDirection == CustomEditableTextWidget.CursorMovementDirection.Left && Input.IsKeyReleased(InputKey.Left) || this._cursorDirection == CustomEditableTextWidget.CursorMovementDirection.Right && Input.IsKeyReleased(InputKey.Right))
//            {
//                this._cursorDirection = CustomEditableTextWidget.CursorMovementDirection.None;
//                if (!Input.IsKeyDown(InputKey.LeftShift))
//                    this._isSelection = false;
//            }
//            else if (Input.IsKeyReleased(InputKey.LeftShift))
//                this._isSelection = false;
//            else if (Input.IsKeyDown(InputKey.Home))
//            {
//                this._cursorDirection = CustomEditableTextWidget.CursorMovementDirection.Left;
//                flag2 = true;
//            }
//            else if (Input.IsKeyDown(InputKey.End))
//            {
//                this._cursorDirection = CustomEditableTextWidget.CursorMovementDirection.Right;
//                flag2 = true;
//            }
//            if (flag1 | flag2)
//            {
//                this._nextRepeatTime = tickCount + 500;
//                if (Input.IsKeyDown(InputKey.LeftShift))
//                {
//                    if (!this._editableText.IsAnySelected())
//                        this._editableText.BeginSelection();
//                    this._isSelection = true;
//                }
//            }
//            if (this._cursorDirection != CustomEditableTextWidget.CursorMovementDirection.None && (flag1 | flag2 || tickCount >= this._nextRepeatTime))
//            {
//                if (flag1)
//                {
//                    int direction = (int) this._cursorDirection;
//                    if (Input.IsKeyDown(InputKey.LeftControl))
//                        direction = this.FindNextWordPosition(direction) - this._editableText.CursorPosition;
//                    this.MoveCursor(direction, this._isSelection);
//                    if (tickCount >= this._nextRepeatTime)
//                        this._nextRepeatTime = tickCount + 30;
//                }
//                else if (flag2)
//                {
//                    this.MoveCursor(this._cursorDirection == CustomEditableTextWidget.CursorMovementDirection.Left ? -this._editableText.CursorPosition : this._editableText.VisibleText.Length - this._editableText.CursorPosition, this._isSelection);
//                    if (tickCount >= this._nextRepeatTime)
//                        this._nextRepeatTime = tickCount + 30;
//                }
//            }
//            bool flag3 = false;
//            if (Input.IsKeyPressed(InputKey.BackSpace))
//            {
//                flag3 = true;
//                this._keyboardAction = CustomEditableTextWidget.KeyboardAction.BackSpace;
//                this._nextRepeatTime = tickCount + 500;
//            }
//            else if (Input.IsKeyPressed(InputKey.Delete))
//            {
//                flag3 = true;
//                this._keyboardAction = CustomEditableTextWidget.KeyboardAction.Delete;
//                this._nextRepeatTime = tickCount + 500;
//            }
//            if (this._keyboardAction == CustomEditableTextWidget.KeyboardAction.BackSpace && !Input.IsKeyDown(InputKey.BackSpace) || this._keyboardAction == CustomEditableTextWidget.KeyboardAction.Delete && !Input.IsKeyDown(InputKey.Delete))
//                this._keyboardAction = CustomEditableTextWidget.KeyboardAction.None;
//            if (Input.IsKeyReleased(InputKey.Enter))
//                this.EventFired("TextEntered");
//            else if (this._keyboardAction == CustomEditableTextWidget.KeyboardAction.BackSpace || this._keyboardAction == CustomEditableTextWidget.KeyboardAction.Delete)
//            {
//                if (!flag3 && tickCount < this._nextRepeatTime)
//                    return;
//                if (this._editableText.IsAnySelected())
//                    this.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
//                else if (Input.IsKeyDown(InputKey.LeftControl))
//                    this.DeleteText(this._editableText.CursorPosition + (this.FindNextWordPosition(-1) - this._editableText.CursorPosition), this._editableText.CursorPosition);
//                else
//                    this.DeleteChar(this._keyboardAction == CustomEditableTextWidget.KeyboardAction.Delete);
//                if (tickCount < this._nextRepeatTime)
//                    return;
//                this._nextRepeatTime = tickCount + 30;
//            }
//            else
//            {
//                if (!Input.IsKeyDown(InputKey.LeftControl))
//                    return;
//                if (Input.IsKeyPressed(InputKey.A))
//                    this._editableText.SelectAll();
//                else if (Input.IsKeyPressed(InputKey.C))
//                    this.CopyText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
//                else if (Input.IsKeyPressed(InputKey.X))
//                {
//                    this.CopyText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
//                    this.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
//                }
//                else
//                {
//                    if (!Input.IsKeyPressed(InputKey.V))
//                        return;
//                    this.DeleteText(this._editableText.SelectedTextBegin, this._editableText.SelectedTextEnd);
//                    this.PasteText();
//                }
//            }
//        }

//        protected internal override void OnGainFocus()
//        {
//            base.OnGainFocus();
//            this._editableText.SetCursor(this.Text.Length, true, false);
//        }

//        protected internal override void OnLoseFocus()
//        {
//            base.OnLoseFocus();
//            this._editableText.ResetSelected();
//            this._isSelection = false;
//            this._editableText.SetCursor(0, false, false);
//        }

//        private void UpdateText()
//        {
//            if (this.IsDisabled)
//                this.SetState("Disabled");
//            else if (this.IsPressed)
//                this.SetState("Pressed");
//            else if (this.IsHovered)
//                this.SetState("Hovered");
//            else
//                this.SetState("Default");
//        }

//        private void UpdateFontData()
//        {
//            if (this._lastFontBrush == this.Brush && (double)this._lastScale == (double)this.Context.Scale)
//                return;
//            this._editableText.StyleFontContainer.ClearFonts();
//            Font font = UIResourceManager.FontFactory.DefaultFont;
//            foreach (Style style in this.Brush.Styles)
//                this._editableText.StyleFontContainer.Add(style.Name, font, (float)this.Brush.FontSize * this.Context.Scale);
//            this._lastFontBrush = this.Brush;
//            this._lastScale = this.Context.Scale;
//        }

//        private Font GetFont(Style style = null)
//        {
//            return UIResourceManager.FontFactory.DefaultFont;
//        }

//        protected override void OnLateUpdate(float dt)
//        {
//            base.OnLateUpdate(dt);
//            if ((double)this.Size.X <= 0.0 || (double)this.Size.Y <= 0.0)
//                return;
//            Vector2 localMousePosition = this.LocalMousePosition;
//            bool focus = this._mouseState == CustomEditableTextWidget.MouseState.Down;
//            this._editableText.UpdateSize((int)this.Size.X, (int)this.Size.Y);
//            this.SetEditTextParameters();
//            this.UpdateFontData();
//            this._editableText.Update(this.Context.SpriteData, localMousePosition, focus);
//        }

//        protected override void OnRender(
//          TwoDimensionContext twoDimensionContext,
//          TwoDimensionDrawContext drawContext)
//        {
//            base.OnRender(twoDimensionContext, drawContext);
//            if (string.IsNullOrEmpty(this._editableText.Value))
//                return;
//            Vector2 globalPosition = this.GlobalPosition;
//            foreach (RichTextPart part in this._editableText.GetParts())
//            {
//                DrawObject2D drawObject2D = part.DrawObject2D;
//                Style styleOrDefault = this.Brush.GetStyleOrDefault(part.Style);
//                Font font = this.GetFont(styleOrDefault);
//                int fontSize;
//                float num1 = (float) (fontSize = styleOrDefault.FontSize) * this.Context.Scale;
//                float num2 = (float) fontSize / (float) font.Size;
//                float height = (float) font.LineHeight * num2 * this.Context.Scale;
//                TextMaterial textMaterial1 = styleOrDefault.CreateTextMaterial(drawContext);
//                textMaterial1.ColorFactor *= this.Brush.GlobalColorFactor;
//                textMaterial1.AlphaFactor *= this.Brush.GlobalAlphaFactor;
//                textMaterial1.Color *= this.Brush.GlobalColor;
//                textMaterial1.Texture = font.FontSprite.Texture;
//                textMaterial1.ScaleFactor = num1;
//                textMaterial1.Smooth = font.Smooth;
//                textMaterial1.SmoothingConstant = font.SmoothingConstant;
//                if ((double)textMaterial1.GlowRadius > 0.0 || (double)textMaterial1.Blur > 0.0 || (double)textMaterial1.OutlineAmount > 0.0)
//                {
//                    TextMaterial textMaterial2 = styleOrDefault.CreateTextMaterial(drawContext);
//                    textMaterial2.CopyFrom(textMaterial1);
//                    drawContext.Draw(globalPosition.X, globalPosition.Y, (Material)textMaterial2, drawObject2D, this.Size.X, this.Size.Y);
//                }
//                textMaterial1.GlowRadius = 0.0f;
//                textMaterial1.Blur = 0.0f;
//                textMaterial1.OutlineAmount = 0.0f;
//                Material material = (Material) textMaterial1;
//                if (part.Style == "Highlight")
//                {
//                    Sprite sprite = this.Context.SpriteData.GetSprite("warm_overlay");
//                    SimpleMaterial simpleMaterial = drawContext.CreateSimpleMaterial();
//                    simpleMaterial.Reset(sprite?.Texture);
//                    drawContext.DrawSprite(sprite, simpleMaterial, globalPosition.X + part.PartPosition.X, globalPosition.Y + part.PartPosition.Y, 1f, part.WordWidth, height, false, false);
//                }
//                drawContext.Draw(globalPosition.X, globalPosition.Y, material, drawObject2D, this.Size.X, this.Size.Y);
//            }
//            if (!this._editableText.IsCursorVisible())
//                return;
//            Style styleOrDefault1 = this.Brush.GetStyleOrDefault("Default");
//            Font font1 = this.GetFont(styleOrDefault1);
//            int fontSize1 = styleOrDefault1.FontSize;
//            float num = (float) fontSize1 / (float) font1.Size;
//            float height1 = (float) font1.LineHeight * num * this.Context.Scale;
//            Sprite sprite1 = this.Context.SpriteData.GetSprite("BlankWhiteSquare_9");
//            SimpleMaterial simpleMaterial1 = drawContext.CreateSimpleMaterial();
//            simpleMaterial1.Reset(sprite1?.Texture);
//            Vector2 cursorPosition = this._editableText.GetCursorPosition(font1, (float) fontSize1, this.Context.Scale);
//            drawContext.DrawSprite(sprite1, simpleMaterial1, (float)(int)((double)globalPosition.X + (double)cursorPosition.X), globalPosition.Y + cursorPosition.Y, 1f, 1f, height1, false, false);
//        }

//        protected internal override void OnMousePressed()
//        {
//            this._mouseDownPosition = this.LocalMousePosition;
//            this._mouseState = CustomEditableTextWidget.MouseState.Down;
//            this._editableText.HighlightStart = true;
//            this._editableText.HighlightEnd = false;
//        }

//        protected internal override void OnMouseReleased()
//        {
//            this._mouseState = CustomEditableTextWidget.MouseState.Up;
//            this._editableText.HighlightEnd = true;
//            this.IsPointInsideMeasuredArea(this.EventManager.MousePosition);
//        }

//        private void OnObfuscationToggled(bool isEnabled)
//        {
//            if (isEnabled)
//                this.Text = this.ObfuscateText(this.RealText);
//            else
//                this.Text = this.RealText;
//        }

//        private string ObfuscateText(string stringToObfuscate)
//        {
//            return new string(this._obfuscationChar, stringToObfuscate.Length);
//        }

//        private enum MouseState
//        {
//            None,
//            Down,
//            Up,
//        }

//        private enum CursorMovementDirection
//        {
//            Left = -1, // 0xFFFFFFFF
//            None = 0,
//            Right = 1,
//        }

//        private enum KeyboardAction
//        {
//            None,
//            BackSpace,
//            Delete,
//        }
//    }
//}
