using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.Localization;
using ElementMachine.Machine;

namespace ElementMachine.Tiles
{
    public class AlloyAnalyzer : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 18 };
            TileObjectData.addTile(Type); 
			AddMapEntry(new Color(196, 199, 205));
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Alloy Analyzer");
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<AlloyAnalyzerItem>());
        }
    }
    public class AlloyAnalyzerItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AlloyAnalyzer");
            DisplayName.AddTranslation(GameCulture.Chinese, "合金分析仪");
            Tooltip.SetDefault("Bi————————Hey, this is not any alloy!\nsome material will be sold in Guide's shop after you analyzer it by this");
            Tooltip.AddTranslation(GameCulture.Chinese, "哔————————日, 这tm不是合金!\n一些材料在你用这玩意分析过后会在向导的商店出售!");
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
			item.createTile = ModContent.TileType<AlloyAnalyzer>();
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