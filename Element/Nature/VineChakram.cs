using ElementMachine.Tiles;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementMachine.Element.Nature
{
    public class VineChakram : ModItem
    {
        public override void SetStaticDefaults() 
		{   
            DisplayName.SetDefault("VineChakram");
            DisplayName.AddTranslation(GameCulture.Chinese, "藤蔓旋刃");
			Tooltip.SetDefault("fall as fallen leaves, sharp as sharpen knives");
            Tooltip.AddTranslation(GameCulture.Chinese, "落叶般飘零, 刀刃般锋利");
		}
        public override void SetDefaults() 
		{
            item.noMelee = true;
            item.useStyle = 1;
            item.shootSpeed = 11f;
            item.shoot = ProjectileType<VineChakramprojectile>();
            item.damage = 10;
            item.knockBack = 8f;
            item.width = 28;
            item.height = 28;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 15;
            item.useTime = 15;
            item.noUseGraphic = true;
            item.rare = 5;
            item.value = 500;
            item.autoReuse = true;
		}
        public override void AddRecipes() 
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Vine, 5);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 20);
			recipe.AddTile(ModContent.TileType<ElementHoroScpoer>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
    public class VineChakramprojectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 23;
            projectile.height = 23;
            projectile.CloneDefaults(ProjectileID.ThornChakram);
        } 
    }
}