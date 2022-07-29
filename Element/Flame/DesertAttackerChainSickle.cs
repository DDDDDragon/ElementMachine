using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using ElementMachine.Bases;
using ElementMachine.Tiles;

namespace ElementMachine.Element.Flame
{
    public class DesertAttackerChainSickle : BaseSickleItem
    {
        public override void SetSickle()
        {
            damage = 10;
            defaultName = "DesertAttackerChainSickle";
            transName = "荒漠突袭者钩镰";
            Effect = "消耗5点命中点数获得三秒钟加速";
            shootType = ModContent.ProjectileType<DesertAttackerChainSickleProj>();
            shootSpeed = 18.5f;
            Item.rare = ItemRarityID.Blue;
            base.SetSickle();
        }
        public override void AddRecipes()
		{
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AntlionCarapace>(), 20);
            recipe.AddIngredient(ItemID.SandBlock, 20);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
            recipe.Register();
		}
    }
    public class DesertAttackerChainSickleProj : BaseSickle
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DACSP");
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
            base.SetSprite("ElementMachine/Element/Flame/DesertAttackerChainSickleChain");
        }
        public override void Effect()
        {
			if(Main.LocalPlayer.GetModPlayer<BasePlayer>().SicklePoint >= 5)
			{
				Main.LocalPlayer.GetModPlayer<BasePlayer>().SicklePoint -= 5;
                player.AddBuff(BuffID.Swiftness, 180);
			}
        }
    }
}