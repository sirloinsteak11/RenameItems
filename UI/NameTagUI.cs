using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Mono.Cecil.Cil;

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
            Append(panel); //appends the panel to the UIState

            UIText header = new UIText("Test test");
            header.HAlign = 0.5f;  // 1
            header.Top.Set(15, 0); // 2
            panel.Append(header);
        }

       /* public static void SetVisible()
        {
            visible = true;
        }

        public static void SetInvisible()
        {
            visible = false;
        }

        public static void ToggleVisible()
        {
            if (visible)
                visible = false;
            else
                visible = true;
        } */

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