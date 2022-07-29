using ElementMachine.Tiles;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ElementMachine.Machine
{
    public class EnergyCore : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EnergyCore");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "能量核心");
            Tooltip.SetDefault("Completely energy!");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "从强大金属中提取出的纯粹能量");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
			Item.height = 24;
			Item.maxStack = 999;
			Item.value = 800;
			Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 1);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe.Register();
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.CrimtaneBar, 1);
            recipe1.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe1.Register();
        }

    }
}