using ElementMachine.Tiles;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ElementMachine.MyRecipe;
using System.Collections.Generic;

namespace ElementMachine.Machine
{
    public class MagicLoop : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MagicLoop");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "魔力回路");
            Tooltip.SetDefault("the dust of fallen star, can help transport mana");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "坠落之星的粉尘, 可以帮助你传输魔力");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
			Item.height = 30;
			Item.maxStack = 999;
			Item.value = 1000;
			Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(10);
            recipe.AddIngredient(ItemID.FallenStar, 1);
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 1);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
            recipe.Register();

            Recipe blueprintRecipe = CreateRecipe(20);
            blueprintRecipe.AddIngredient(this, 20);
            blueprintRecipe.AddTile(ModContent.TileType<AlloyAnalyzer>());
            blueprintRecipe.AddOnCraftCallback(delegate (Recipe recipe, Item item, List<Item> consumedItems) {
                BlueprintRecipe.OnCraft(recipe, item, consumedItems);
            });
            blueprintRecipe.Register();
        }
    }
}