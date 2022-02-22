using ElementMachine.Tiles;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementMachine.Machine
{
    public class EnergyCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EnergyCore");
            DisplayName.AddTranslation(GameCulture.Chinese, "能量核心");
            Tooltip.SetDefault("Completely energy!");
            Tooltip.AddTranslation(GameCulture.Chinese, "从强大金属中提取出的纯粹能量");
        }
        public override void SetDefaults()
        {
            item.width = 30;
			item.height = 24;
			item.maxStack = 999;
			item.value = 800;
			item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DemoniteBar, 1);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemID.CrimtaneBar, 1);
            recipe1.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe1.SetResult(this);
            recipe1.AddRecipe();
        }

    }
}