using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.Graphics;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

class UITest : UIState
{
    public override void OnInitialize()
    {
        UIPanel panel = new UIPanel();
        panel.Top.Set(0f, 0.063f);
        panel.Left.Set(0f, 0.423f);
        panel.Width.Set(0f, 0.158f);
        panel.Height.Set(0f, 0.151f);
        Append(panel);

        UIText element1 = new UIText("UITextBox");
        element1.Top.Set(0f, 0.15f);
        element1.Left.Set(0f, 0.422f);
        Append(element1);

        UITextPanel<string> textBox = new UITextPanel<string>("rename item", 2, false);
        element1.Top.Set(0f, 0.063f);
        element1.Left.Set(0f, 0.421f);
        element1.MinWidth.Set(188f, 0f);
        Append(element1);

    }
}
