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
    public class ElementHoroscoper : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 18 };
            TileObjectData.addTile(Type); 
			AddMapEntry(new Color(126, 67, 0));
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Element Horoscoper");
            TileID.Sets.DisableSmartCursor[Type] = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 16, ModContent.ItemType<ElementHoroscoperItem>());
        }
    }
    public class ElementHoroscoperItem : ElementItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ElementHoroscoper");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "元素星象仪");
            Tooltip.SetDefault("provide star's force for crafting element things\nusing for crafting many earlier stage things!");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "为你前期的元素合成提供星象之力\n用于合成许多前期物品!");
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
			Item.createTile = ModContent.TileType<ElementHoroscoper>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 10);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 20);
            recipe.AddTile(TileID.Anvils);
            
            recipe.Register();
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.FallenStar, 1);
            recipe1.AddRecipeGroup(RecipeGroupID.Wood, 20);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }

    }
}