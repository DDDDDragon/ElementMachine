using ElementMachine.Tiles;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementMachine.Machine
{
    public class LifeCore : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("LifeCore");
            DisplayName.AddTranslation(GameCulture.Chinese, "生命核心");
            Tooltip.SetDefault("Completely life!");
            Tooltip.AddTranslation(GameCulture.Chinese, "从浓缩生命力中提取出的纯粹能量");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 999;
            item.value = 1000;
            item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HealingPotion, 10);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemID.LesserHealingPotion, 20);
            recipe1.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe1.SetResult(this);
            recipe1.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ItemID.SuperHealingPotion, 1);
            recipe2.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe2.SetResult(this);
            recipe2.AddRecipe();
            ModRecipe recipe3 = new ModRecipe(mod);
            recipe3.AddIngredient(ItemID.GreaterHealingPotion, 5);
            recipe3.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe3.SetResult(this);
            recipe3.AddRecipe();
        }

    }
}