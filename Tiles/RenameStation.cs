using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RenameItems.Tiles
{
    public class RenameStation : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.CoordinateHeights = new[] { 18 };
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Rename Station");
            AddMapEntry(new Color(200, 200, 200), name);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override bool RightClick(int i, int j)
        {
            return base.RightClick(i, j);
        }

        /*public override void KillMultiTile(int i, int j, int frameX, int frameY)
       {
           Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Items.RenameStation>());
       }*/
    }
}