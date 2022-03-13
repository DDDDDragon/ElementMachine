using ElementMachine.Recipe;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Tiles;

namespace ElementMachine.Machine
{
    public class JuniorAlloy : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloy");
            DisplayName.AddTranslation(GameCulture.Chinese, "初级合金");
            Tooltip.SetDefault("In fact, Iron + Copper = nothing");
            Tooltip.AddTranslation(GameCulture.Chinese, "实际上, 铁和铜合在一起什么也不是");
        }
        public override void SetDefaults()
        {
            item.width = 30;
			item.height = 24;
			item.maxStack = 999;
			item.value = 100;
			item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 1);
            recipe.AddIngredient(ItemID.CopperBar, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();

            BlueprintRecipe blueprintRecipe = new BlueprintRecipe(mod);
            blueprintRecipe.AddIngredient(this, 1);
            blueprintRecipe.AddTile(ModContent.TileType<AlloyAnalyzer>());
            blueprintRecipe.SetResult(this);
            blueprintRecipe.AddRecipe();
        }
    }
}