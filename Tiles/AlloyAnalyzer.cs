using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.Localization;
using ElementMachine.Machine;
using Terraria.DataStructures;

namespace ElementMachine.Tiles
{
    public class AlloyAnalyzer : ModTile
    {
        public override void SetStaticDefaults()
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
            TileID.Sets.DisableSmartCursor[Type] = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 16, ModContent.ItemType<AlloyAnalyzerItem>());
        }
    }
    public class AlloyAnalyzerItem : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AlloyAnalyzer");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "合金分析仪");
            Tooltip.SetDefault("Bi————————Hey, this is not any alloy!\nsome material will be sold in Guide's shop after you analyzer it by this");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "哔————————日, 这tm不是合金!\n一些材料在你用这玩意分析过后会在向导的商店出售!");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
			Item.height = 18;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<AlloyAnalyzer>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 10);
            recipe.AddTile(TileID.Anvils);
            
            recipe.Register();
        }

    }
}