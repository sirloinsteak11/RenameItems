using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;

namespace RenameItems.UI
{
    internal class CustomUITextbox : UITextBox
    {

        public static bool focused = false;
        public static string Text { get; set; } = "";

        public int MaxCharacters { get; set; } = 20;


        public CustomUITextbox(string text, float textScale = 1f, bool large = false) : base(text, textScale, large)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (focused)
            {
                Terraria.GameInput.PlayerInput.WritingText = true;
                Main.instance.HandleIME();
                string oldText = Text;
                Text = Main.GetInputText(Text);
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Main.UIScaleMatrix);
                Main.instance.DrawWindowsIMEPanel(new Vector2(98f, (float)(Main.screenHeight - 36)), 0f);
                this.SetText(Text);
            }
            if (!focused) 
            {
                this.SetText("");
                Text = "";
            }
            base.Draw(spriteBatch);
        }

        public static bool ToggleFocus()
        {
            if (focused)
            {
                focused = false;
                return false;
            }
            else
            {
                focused = true;
                return true;
            }
        }
    }
}
