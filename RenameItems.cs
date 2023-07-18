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

        public override void Load()
        {
            if (!Main.dedServ)
            {
                nametagUI = new NameTagUI();
                nametagUI.Initialize();
                nametagInterface = new UserInterface();
                nametagInterface.SetState(nametagUI);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            // it will only draw if the player is not on the main menu
            if (!Main.gameMenu
                && NameTagUI.visible)
            {
                nametagInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Add(new LegacyGameInterfaceLayer("Nametag UI", DrawNametagUI, InterfaceScaleType.UI));
        }

        private bool DrawNametagUI()
        {
            // it will only draw if the player is not on the main menu
            if (!Main.gameMenu
                && NameTagUI.visible)
            {
                nametagInterface.Draw(Main.spriteBatch, new GameTime());
            }
            return true;
        }
    }
}