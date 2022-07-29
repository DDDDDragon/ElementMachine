using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Tiles;
using Terraria;

namespace ElementMachine.Machine
{
    public class EnergyAlloy : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EnergyAlloy");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "能量合金");
            Tooltip.SetDefault("Alloy that full of energy,can be made into better things");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "充满能量的合金, 可以制作更好的物品");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
			Item.height = 24;
			Item.maxStack = 999;
			Item.value = 1000;
			Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EnergyCore>(), 1);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
            
            recipe.Register();
        }
    }
}