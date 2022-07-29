using ElementMachine.Tiles;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace ElementMachine.Element.Nature
{
    public class VineChakram : ElementItem
    {
        public override void SetStaticDefaults() 
		{   
            DisplayName.SetDefault("VineChakram");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "藤蔓旋刃");
			Tooltip.SetDefault("fall as fallen leaves, sharp as sharpen knives");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "落叶般飘零, 刀刃般锋利");
		}
        public override void SetDefaults() 
		{
            Item.noMelee = true;
            Item.useStyle = 1;
            Item.shootSpeed = 11f;
            Item.shoot = ProjectileType<VineChakramprojectile>();
            Item.damage = 10;
            Item.knockBack = 8f;
            Item.width = 28;
            Item.height = 28;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.noUseGraphic = true;
            Item.rare = 5;
            Item.value = 500;
            Item.autoReuse = true;
		}
        public override void AddRecipes() 
        {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Vine, 5);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 20);
			recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
    }
    public class VineChakramprojectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 23;
            Projectile.height = 23;
            Projectile.CloneDefaults(ProjectileID.ThornChakram);
        } 
    }
}