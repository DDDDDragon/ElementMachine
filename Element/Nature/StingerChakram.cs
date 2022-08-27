using Terraria;
using ElementMachine.Tiles;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementMachine.Element.Nature
{
    public class StingerChakram : ElementItem
    {
        public override void SetStaticDefaults() 
		{   
            DisplayName.SetDefault("StingerChakram");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "毒刺旋刃");
			Tooltip.SetDefault("fall as fallen leaves, sharp as sharpen knives, broke as broken lives");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "落叶般飘零, 刀刃般锋利, 毒液般致命");
		}
        public override void SetDefaults() 
		{
            Item.noMelee = true;
            Item.useStyle = 1;
            Item.shootSpeed = 11f;
            Item.shoot = ProjectileType<StingerChakramprojectile>();
            Item.damage = 25;
            Item.knockBack = 8f;
            Item.width = 30;
            Item.height = 30;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.noUseGraphic = true;
            Item.rare = 5;
            Item.value = 1000;
            Item.autoReuse = true;
            Element = 5;
            ElementLevel = 1f;
        }
        public override void AddRecipes() 
        {
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VineChakram>(), 1);
			recipe.AddIngredient(ItemID.Stinger, 20);
            recipe.AddIngredient(ItemID.JungleSpores, 20);
			recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 180);
        }
    }
    public class StingerChakramprojectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.CloneDefaults(ProjectileID.ThornChakram);
        } 
    }
}