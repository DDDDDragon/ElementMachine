using ElementMachine.Tiles;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Recipe;

namespace ElementMachine.Machine
{
    public class MagicLoop : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MagicLoop");
            DisplayName.AddTranslation(GameCulture.Chinese, "魔力回路");
            Tooltip.SetDefault("the dust of fallen star, can help transport mana");
            Tooltip.AddTranslation(GameCulture.Chinese, "坠落之星的粉尘, 可以帮助你传输魔力");
        }
        public override void SetDefaults()
        {
            item.width = 22;
			item.height = 30;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FallenStar, 1);
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 1);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe.SetResult(this, 10);
            recipe.AddRecipe();

            BlueprintRecipe blueprintRecipe = new BlueprintRecipe(mod);
            blueprintRecipe.AddIngredient(this, 20);
            blueprintRecipe.AddTile(ModContent.TileType<AlloyAnalyzer>());
            blueprintRecipe.SetResult(this, 20);
            blueprintRecipe.AddRecipe();
        }
    }
}