using Terraria;
using ElementMachine.Tiles;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ElementMachine.Element.Nature
{
    public class StingerChakram : ModItem
    {
        public override void SetStaticDefaults() 
		{   
            DisplayName.SetDefault("StingerChakram");
            DisplayName.AddTranslation(GameCulture.Chinese, "毒刺旋刃");
			Tooltip.SetDefault("fall as fallen leaves, sharp as sharpen knives, broke as broken lives");
            Tooltip.AddTranslation(GameCulture.Chinese, "落叶般飘零, 刀刃般锋利, 毒液般致命");
		}
        public override void SetDefaults() 
		{
            item.noMelee = true;
            item.useStyle = 1;
            item.shootSpeed = 11f;
            item.shoot = ProjectileType<StingerChakramprojectile>();
            item.damage = 25;
            item.knockBack = 8f;
            item.width = 30;
            item.height = 30;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 15;
            item.useTime = 15;
            item.noUseGraphic = true;
            item.rare = 5;
            item.value = 1000;
            item.autoReuse = true;
		}
        public override void AddRecipes() 
        {
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<VineChakram>(), 1);
			recipe.AddIngredient(ItemID.Stinger, 20);
            recipe.AddIngredient(ItemID.JungleSpores, 20);
			recipe.AddTile(ModContent.TileType<ElementHoroScpoer>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        public override bool UseItem(Player player)
        {
            return base.UseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 3000);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
    public class StingerChakramprojectile : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.CloneDefaults(ProjectileID.ThornChakram);
        } 
    }
}