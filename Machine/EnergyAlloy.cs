using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Tiles;

namespace ElementMachine.Machine
{
    public class EnergyAlloy : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EnergyAlloy");
            DisplayName.AddTranslation(GameCulture.Chinese, "能量合金");
            Tooltip.SetDefault("Alloy that full of energy,can be made into better things");
            Tooltip.AddTranslation(GameCulture.Chinese, "充满能量的合金, 可以制作更好的物品");
        }
        public override void SetDefaults()
        {
            item.width = 30;
			item.height = 24;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EnergyCore>(), 1);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}