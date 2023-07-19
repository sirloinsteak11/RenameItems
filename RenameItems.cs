using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using RenameItems.UI;

namespace RenameItems
{
	public class RenameItems : ModSystem
	{
		internal NameTagUI nametagUI;
		public UserInterface nametagInterface;
        private GameTime lastUpdateUiGameTime;
        private Main Main;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                nametagUI = new NameTagUI();
                nametagUI.Initialize();
                nametagInterface = new UserInterface();
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            lastUpdateUiGameTime = gameTime;
            if (nametagInterface?.CurrentState != null)
            {
                nametagInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("RenameItems: NametagInterface", delegate
                {
                    if (lastUpdateUiGameTime != null && nametagInterface?.CurrentState != null)
                    {
                        nametagInterface.Draw(Main.spriteBatch, lastUpdateUiGameTime);
                    }
                    return true;
                }, InterfaceScaleType.UI));
            }
        }

        internal void ToggleUI()
        {
            if (nametagInterface?.CurrentState == null)
            {
                nametagInterface?.SetState(nametagUI);
            }
            else
            {
                nametagInterface?.SetState(null);
            }
        }
    }
}