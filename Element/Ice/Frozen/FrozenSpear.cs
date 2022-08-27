using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Buffs;
using ElementMachine.Tiles;
using System.Collections.Generic;
using ElementMachine.Bases;

namespace ElementMachine.Element.Ice.Frozen
{
	public class FrozenSpear : BaseShieldSpear
	{
		public override void SetSpear()
        {
			damage = 11;
            defaultName = "FrozenShieldSpear";
            transName = "霜寒盾矛";
            Effect = "击中敌人造成减速效果";
			EffectEn = "make enemys' speed lower when hitting them";
			tooltip = "冰冷刺骨";
			tooltipEn = "Cold to bones";
            SpearProjType = ModContent.ProjectileType<Frozenproj>();
			ThrownProjType = ModContent.ProjectileType<Frozenproj2>();
            shootSpeed = 10f;
			Element = 2;
			ElementLevel = 0.7f;
			base.SetSpear();
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IceBlock, 20);
			recipe.AddIngredient(ItemID.SnowBlock, 20);
			recipe.AddIngredient(ItemID.SlushBlock, 20);
			recipe.AddIngredient(ModContent.ItemType<FrozenStone>(), 7);
			recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
	}
    public class Frozenproj2 : BaseShieldSpearProjT
    {
        public override void SetDefaults()
        {
			Element = 2;
			ElementLevel = 0.7f;
            base.SetDefaults();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(ModContent.BuffType<lowerSpeed>(), 180);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }

	public class Frozenproj : BaseShieldSpearProjS
	{
		public override void SetDefaults()
		{
			Element = 2;
			ElementLevel = 0.7f;
			base.SetDefaults();
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<lowerSpeed>(), 180);
			base.OnHitNPC(target, damage, knockback, crit);
		}
	}
}
