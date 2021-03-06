using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.Localization;
using ElementMachine.Machine;

namespace ElementMachine.Tiles
{
    public class ElementHoroScoper : ModTile
    {
        public override void SetDefaults()
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
            name.SetDefault("Element HoroScoper");
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<ElementHoroScoperItem>());
        }
    }
    public class ElementHoroScoperItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ElementHoroScoper");
            DisplayName.AddTranslation(GameCulture.Chinese, "元素星象仪");
            Tooltip.SetDefault("provide star's force for crafting element things\nusing for crafting many earlier stage things!");
            Tooltip.AddTranslation(GameCulture.Chinese, "为你前期的元素合成提供星象之力\n用于合成许多前期物品!");
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
			item.createTile = ModContent.TileType<ElementHoroScoper>();
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 10);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemID.FallenStar, 1);
            recipe1.AddRecipeGroup(RecipeGroupID.Wood, 20);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }

    }
}