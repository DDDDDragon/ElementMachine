using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Input;
using ElementMachine.Bases;
namespace ElementMachine.OriginalEdition
{
    public class WoodenChainSickle : BaseSickleItem
    {
		public override void SetSickle()
        {
			damage = 4;
			shootType = ModContent.ProjectileType<WoodenChainSickleProj>();
			defaultName = "WoodenChainSickle";
			transName = "木钩镰";
			Effect = "消耗8点命中点数恢复20点血量";
            base.SetSickle();
        }
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("Wood", 20);
			recipe.AddIngredient(ItemID.Chain, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
	public class WoodenChainSickleProj : BaseSickle
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("wood");
		}
		public override void SetDefaults()
		{
			projectile.width = 46;
			projectile.height = 28;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
            projectile.knockBack = 0;
		}
        public override void SetSprite(string a)
        {
            base.SetSprite("ElementMachine/OriginalEdition/WoodenChainSickleChain");
        }
        public override void Effect()
        {
			if(Main.LocalPlayer.GetModPlayer<BasePlayer>().SicklePoint >= 8)
			{
				Main.LocalPlayer.GetModPlayer<BasePlayer>().SicklePoint -= 8;
				player.statLife += 20;
				CombatText.NewText(player.getRect(), Color.Green, 20);
			}
        }
    
	}
}