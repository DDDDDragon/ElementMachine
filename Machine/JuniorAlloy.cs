using ElementMachine.MyRecipe;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ElementMachine.Tiles;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace ElementMachine.Machine
{
    public class JuniorAlloy : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloy");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "初级合金");
            Tooltip.SetDefault("In fact, Iron + Copper = nothing");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "实际上, 铁和铜合在一起什么也不是");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
			Item.height = 24;
			Item.maxStack = 999;
			Item.value = 100;
			Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 1);
            recipe.AddIngredient(ItemID.CopperBar, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddRecipeGroup(RecipeGroupID.IronBar, 1);
            recipe2.AddIngredient(ItemID.TinBar, 1);
            recipe2.AddTile(TileID.Furnaces);
            recipe2.Register();
            Recipe blueprintRecipe = CreateRecipe(10);
            blueprintRecipe.AddIngredient(this, 10);
            blueprintRecipe.AddTile(ModContent.TileType<AlloyAnalyzer>());
            blueprintRecipe.AddOnCraftCallback(delegate (Recipe recipe, Item item, List<Item> consumedItems) {
                BlueprintRecipe.OnCraft(recipe, item, consumedItems);
                });
            blueprintRecipe.Register();
        }
    }
}