using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.UI;
using RenameItems.UI;
using IL.Terraria.DataStructures;
using rail;

namespace RenameItems.Items;
public class NameTag : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Can set a custom name for any item");
    }

    public override void SetDefaults()
    {
        Item.autoReuse = false;
        Item.value = 100;
        Item.rare = ItemRarityID.Green;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 30;
        Item.useTime = 30;
        Item.width = 20;
        Item.height = 20;
        Item.noMelee = true;
        Item.notAmmo = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wood, 5);
        recipe.AddIngredient(ItemID.Gel, 1);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

    public override bool CanRightClick()
    {
        return true;
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            NameTagUI.SetVisible();
            return true;
        }
        return null;
    }

    public override void RightClick(Player player)
    {
        NameTagUI.SetVisible();
        Rain.MakeRain();
        Item.SetNameOverride("Sex");
    }
}