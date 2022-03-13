using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.Localization;
using ElementMachine.Machine;

namespace ElementMachine.Tiles
{
    public class AlloyWorkBench : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.CoordinateHeights = new int[]{ 18 };
            TileObjectData.addTile(Type); 
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			AddMapEntry(new Color(154, 158, 167));
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Alloy WorkBench");
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<AlloyWorkBenchItem>());
        }
    }
    public class AlloyWorkBenchItem : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AlloyWorkBench");
            DisplayName.AddTranslation(GameCulture.Chinese, "合金工作台");
            Tooltip.SetDefault("shiny, streamlined, cold, how awesome!");
            Tooltip.AddTranslation(GameCulture.Chinese, "闪亮亮, 流线型, 冷冰冰, 好jb酷!");
        }
        public override void SetDefaults()
        {
            item.width = 32;
			item.height = 18;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = ModContent.TileType<AlloyWorkBench>();
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}