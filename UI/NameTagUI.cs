using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;

namespace RenameItems.UI
{
    internal class NameTagUI : UIState
    {
        public static bool visible;  
        private UIPanel panel;
        private UIElement testButton;
        public float oldScale;

        public override void OnInitialize()
        {
            panel = new UIPanel(); //initialize the panel
            // ignore these extra 0s
            panel.Left.Set(800, 0); //this makes the distance between the left of the screen and the left of the panel 800 pixels (somewhere by the middle).
            panel.Top.Set(100, 0); //this is the distance between the top of the screen and the top of the panel
            panel.Width.Set(100, 0);
            panel.Height.Set(100, 0);

            testButton = new UIElement();
            testButton.Width = StyleDimension.FromPercent(35f);
            testButton.Height = StyleDimension.FromPercent(35f);
            panel.Append(testButton);

            Append(panel); //appends the panel to the UIState
        }

        public static void SetVisible()
        {
            visible = true;
        }

        public static void SetInvisible()
        {
            visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (oldScale != Main.inventoryScale)
            {
                oldScale = Main.inventoryScale;
                Recalculate();
            }
        }
    }
}