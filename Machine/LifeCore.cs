using ElementMachine.Tiles;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ElementMachine.Machine
{
    public class LifeCore : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("LifeCore");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "生命核心");
            Tooltip.SetDefault("Completely life!");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "从浓缩生命力中提取出的纯粹能量");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 999;
            Item.value = 1000;
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HealingPotion, 10);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe.Register();
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.LesserHealingPotion, 20);
            recipe1.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe1.Register();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.SuperHealingPotion, 1);
            recipe2.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe2.Register();
            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ItemID.GreaterHealingPotion, 5);
            recipe3.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe3.Register();
        }

    }
}