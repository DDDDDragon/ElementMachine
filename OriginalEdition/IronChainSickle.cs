using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using ElementMachine.Bases;
namespace ElementMachine.OriginalEdition
{
    public class IronChainSickle : BaseSickleItem
    {
		public override void SetSickle()
        {
			damage = 8;
			shootType = ModContent.ProjectileType<IronChainSickleProj>();
			defaultName = "IronChainSickle";
			transName = "铁钩镰";
			Effect = "消耗4点命中点数恢复20点血量";
            base.SetSickle();
        }
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 20);
			recipe.AddIngredient(ItemID.Chain, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
    public class IronChainSickleProj : BaseSickle
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("iron");
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
            base.SetSprite("ElementMachine/OriginalEdition/IronChainSickleChain");
        }
	}
}