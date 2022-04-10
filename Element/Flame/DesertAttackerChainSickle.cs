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
            item.rare = ItemRarityID.Blue;
            base.SetSickle();
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<AntlionCarapace>(), 20);
            recipe.AddIngredient(ItemID.SandBlock, 20);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			recipe.SetResult(this);
			recipe.AddRecipe();
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
			projectile.width = 46;
			projectile.height = 28;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
            projectile.knockBack = 0;
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