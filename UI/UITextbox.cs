using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;

namespace RenameItems.UI
{
    public class UITextbox : UIElement
    {
        private RasterizerState _rasterizerState = new RasterizerState() { ScissorTestEnable = true };
		internal static Asset<Texture2D> textboxBackground;
		private static Texture2D textboxFill;

		private static Texture2D TextboxFill
		{
			get
			{
				if (textboxFill == null && textboxBackground.Value != null)
				{
					int width = textboxBackground.Width();
					int height = textboxBackground.Height();
					Color[] edgeColors = new Color[width * height];
					textboxBackground.Value.GetData(edgeColors);
					Color[] fillColors = new Color[height];
					for (int y = 0; y < fillColors.Length; y++)
					{
						fillColors[y] = edgeColors[width - 1 + y * width];
					}
					textboxFill = new Texture2D(UIView.graphics, 1, fillColors.Length);
					textboxFill.SetData(fillColors);
				}
				return textboxFill;
			}
		}

		private bool focused = false;
		public bool HadFocus { get { return focused; } }
		public bool Numeric { get; set; }
		public bool HasDecimal { get; set; }
		private static float blinkTime = 1f;
		private static float timer = 0f;

		//bool eventSet = false;
		private float width = 200;

		public delegate void KeyPressedHandler(object sender, char key);

		public event EventHandler OnTabPress;

		public event EventHandler OnEnterPress;

		public event EventHandler OnLostFocus;

		public event KeyPressedHandler KeyPressed;

		private bool drawCarrot = false;
		private UILabel label = new UILabel();
		private static int padding = 4;

		public string Text { get; set; } = "";

		public int MaxCharacters { get; set; } = 20;

        public UITextbox()
		{
			this.onLeftClick += new EventHandler(UITextbox_onLeftClick);
			this.onRightClick += (a, b) => {
				Text = "";
				KeyPressed?.Invoke(this, ' ');
			};
			label.ForegroundColor = Color.Black;
			label.Scale = Height / label.Height;
			label.TextOutline = false;
			Numeric = false;
			HasDecimal = false;
			label.Position = new Vector2(4, 4);
			this.AddChild(label);
		}

		private void UITextbox_onLeftClick(object sender, EventArgs e)
		{
			Focus();
		}

		public void Focus()
		{
			if (!focused)
			{
				Main.clrInput();
				focused = true;
				Main.blockInput = true;
				timer = 0f;
				//eventSet = true;
			}
			//ModUtils.StopListeningForKeyEvents();
			//Main.RemoveKeyEvent();
			//keyBoardInput.newKeyEvent += new Action<char>(KeyboardInput_newKeyEvent);
		}

		public void Unfocus()
		{
			if (focused)
			{
				focused = false;
				Main.blockInput = false;

				OnLostFocus?.Invoke(this, EventArgs.Empty);
			}
			//if (!eventSet) return;
			//eventSet = false;
			//keyBoardInput.newKeyEvent -= new Action<char>(KeyboardInput_newKeyEvent);
			//ModUtils.StartListeningForKeyEvents();
			//Main.AddKeyEvent();
		}

        protected override float GetWidth()
		{
			return width;
		}

		protected override void SetWidth(float width)
		{
			this.width = width;
		}

		protected override float GetHeight()
		{
			return textboxBackground.Height();
		}

		public override void Update()
		{
			base.Update();
			if (!IsMouseInside() && (MouseLeftButton || MouseRightButton))
			{
				Unfocus();
			}
			if (focused)
			{
				timer += .1f; //ModUtils.DeltaTime;
				if (timer < blinkTime / 2) drawCarrot = true;
				else drawCarrot = false;
				if (timer >= blinkTime) timer = 0;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (focused)
			{
				Terraria.GameInput.PlayerInput.WritingText = true;
				Main.instance.HandleIME();
				string oldText = Text;
				Text = Main.GetInputText(Text);
				if (oldText != Text)
				{
					KeyPressed?.Invoke(this, ' ');
				}
				if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Tab))
				{
					OnTabPress?.Invoke(this, new EventArgs());
				}
				if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
				{
					OnEnterPress?.Invoke(this, new EventArgs());
				}
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Main.UIScaleMatrix);
				Main.instance.DrawWindowsIMEPanel(new Vector2(98f, (float)(Main.screenHeight - 36)), 0f);
			}

			spriteBatch.Draw(textboxBackground.Value, DrawPosition, null, Color.White, 0f, Origin, 1f, SpriteEffects.None, 0f);
			int fillWidth = (int)Width - 2 * textboxBackground.Width();
			Vector2 pos = DrawPosition;
			pos.X += textboxBackground.Width();
			if (TextboxFill != null)
            {
				spriteBatch.Draw(TextboxFill, pos - Origin, null, Color.White, 0f, Vector2.Zero, new Vector2(fillWidth, 1f), SpriteEffects.None, 0f);
			}
			pos.X += fillWidth;
			spriteBatch.Draw(textboxBackground.Value, pos, null, Color.White, 0f, Origin, 1f, SpriteEffects.FlipHorizontally, 0f);
			string drawString = Text;
			if (PasswordBox) drawString = passwordString;
			if (drawCarrot && focused) drawString += "|";
			label.Text = drawString;

			pos = DrawPosition - Origin;

			if (pos.X <= Main.screenWidth && pos.Y <= Main.screenHeight && pos.X + Width >= 0 && pos.Y + Height >= 0)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, _rasterizerState, null, Main.UIScaleMatrix);

				Rectangle cutRect = new Rectangle((int)pos.X, (int)pos.Y, (int)Width, (int)Height);
				/*if (cutRect.X < 0)
				{
					cutRect.Width += cutRect.X;
					cutRect.X = 0;
				}
				if (cutRect.Y < 0)
				{
					cutRect.Height += cutRect.Y;
					cutRect.Y = 0;
				}
				if (cutRect.X + Width > Main.screenWidth) cutRect.Width = Main.screenWidth - cutRect.X;
				if (cutRect.Y + Height > Main.screenHeight) cutRect.Height = Main.screenHeight - cutRect.Y;*/
				cutRect = CheatSheet.GetClippingRectangle(spriteBatch, cutRect);
				Rectangle currentRect = spriteBatch.GraphicsDevice.ScissorRectangle;
				spriteBatch.GraphicsDevice.ScissorRectangle = cutRect;

				base.Draw(spriteBatch);

				spriteBatch.GraphicsDevice.ScissorRectangle = currentRect;
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, Main.UIScaleMatrix);
			}
		}
    }
}