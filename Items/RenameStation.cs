using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RenameItems.Items;
public class RenameStation : ModItem
{
    public override void SetStaticDefaults()
    {
        Tooltip.SetDefault("Used to rename items");
    }

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 22;
        Item.useTurn = true;
        Item.autoReuse = true;
        Item.useAnimation = 15;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.value = 150;
        Item.createTile = ModContent.TileType<Tiles.RenameStation>();
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.WorkBench, 1);
        recipe.AddIngredient(ItemID.LeadAnvil, 1);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();

        Recipe recipe2 = CreateRecipe();
        recipe2.AddIngredient(ItemID.WorkBench, 1);
        recipe2.AddIngredient(ItemID.IronAnvil, 1);
        recipe2.AddTile(TileID.WorkBenches);
        recipe2.Register();
    }
}