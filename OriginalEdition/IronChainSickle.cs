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
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 20);
			recipe.AddIngredient(ItemID.Chain, 1);
			recipe.AddTile(TileID.WorkBenches);
			
			recipe.Register();
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
			Projectile.width = 46;
			Projectile.height = 28;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.knockBack = 0;
		}
        public override void SetSprite(string a)
        {
            base.SetSprite("ElementMachine/OriginalEdition/IronChainSickleChain");
        }
	}
}