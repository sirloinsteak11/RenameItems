using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using RenameItems.UI;
namespace RenameItems
{
    public class RenameItems: ModSystem
    {
        internal NameTagUI nametagUI;
        public UserInterface nametagInterface;
        private GameTime lastUpdateUiGameTime;
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
            lastUpdateUiGameTime = gameTime;
            // it will only draw if the player is not on the main menu
            if (MyInterface?.CurrentState != null)
            {
                MyInterface.Update(gameTime);
            }
        }
        public override void ModifyInterfaceLayers(List < GameInterfaceLayer > layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("RenameItems: NametagInterface", delegate
                {
                    if (_lastUpdateUiGameTime != null && nametagInterface?.CurrentState != null)
                    {
                        MyInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
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
            } else {
                nametagInterface?.SetState(null);
            }
        }
    }
}