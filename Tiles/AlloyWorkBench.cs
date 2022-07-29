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
    public class AlloyWorkBench : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.CoordinateHeights = new int[]{ 18 };
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.addTile(Type); 
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			AddMapEntry(new Color(154, 158, 167));
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Alloy WorkBench");
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 16, ModContent.ItemType<AlloyWorkBenchItem>());
        }
    }
    public class AlloyWorkBenchItem : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AlloyWorkBench");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "合金工作台");
            Tooltip.SetDefault("shiny, streamlined, cold, how awesome!");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "闪亮亮, 流线型, 冷冰冰, 好jb酷!");
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
			Item.createTile = ModContent.TileType<AlloyWorkBench>();
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