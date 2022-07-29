using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using ElementMachine.Buffs;
using ElementMachine.Tiles;
using System.Collections.Generic;
using Terraria.GameContent;

namespace ElementMachine.Bases
{
    public abstract class BaseShieldSpear : ElementItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            SetSpear();
            DisplayName.SetDefault(defaultName);
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), transName);
			Tooltip.SetDefault(tooltipEn + "left click to channel and attack, while channeling, your defense will increase, right click to throw it as a Thrown Spear\n[c/8c6640:「Special Effect」]" + EffectEn);
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), tooltip + "\n[c/00FF00:「盾矛」]\n左键蓄力刺出,蓄力时防御力增加,右键可以当作投矛使用\n[c/8c6640:「特效」]\n" + Effect);
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset) 
		{
			base.PreDrawTooltipLine(line, ref yOffset);
			if (!line.OneDropLogo) 
			{
				string sepText = "[c/00FF00:「" + (Main.LocalPlayer.GetModPlayer<BasePlayer>().ShieldSpearDefensePer - 1) * 100 + "%防御加成」],[c/00FF00:「" + Main.LocalPlayer.GetModPlayer<BasePlayer>().ShieldSpearExtraDefense + "额外防御」]";
				if (line.Name == "Damage" && line.Mod == "Terraria")
				{
					float drawX = line.X + line.Font.MeasureString(sepText).X - 300;
					float drawY = line.Y;
					ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, sepText,
						new Vector2(drawX, drawY), line.Color, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);
				}
				else 
				{
					yOffset = 0;
				}
			}
			return true;
		}
        public virtual void SetSpear()
		{

		}
        public override void SetDefaults()
		{
            SetSpear();
			Item.damage = damage;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 28;
			Item.useTime = 36;
			Item.shootSpeed = shootSpeed;
			Item.width = 48;
			Item.height = 48;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 10);
			Item.DamageType = DamageClass.Summon;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item1;
			Item.shoot = SpearProjType;
		}
        public string defaultName = " ";
		public string transName = " ";
		public string Effect = " ";
        public string EffectEn = " ";
        public string tooltip;
		public string tooltipEn = " ";
        public int damage = 0;
		public float shootSpeed = 15.1f;
        public int SpearProjType = 0;
        public int ThrownProjType = 0;
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse == 2)
            {
				Item.shoot = ThrownProjType;
			}
            else
            {
				Item.shoot = SpearProjType;
            }
			return true;
		}
        public override bool CanShoot(Player player)
        {
			foreach (var i in Main.projectile)
			{
				if (i.type == SpearProjType && i.active) return false;
			}
			return base.CanShoot(player);
        }
    }
    public abstract class BaseShieldSpearProjT : ModProjectile
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("ThrownSpear");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
			Projectile.width = 5;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.CloneDefaults(ProjectileID.JavelinFriendly);
            base.SetDefaults();
        }
    }
    public abstract class BaseShieldSpearProjS : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spear");
		}

		public override void SetDefaults()
		{
			Projectile.width = 72;
			Projectile.height = 76;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1.3f;
			Projectile.alpha = 255;

			Projectile.ownerHitCheck = true;
			Projectile.minion = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
		}
		public int Timer
		{
			get { return (int)Projectile.ai[0]; }
			set { Projectile.ai[0] = value; }
		}
		public override bool ShouldUpdatePosition()
		{
			return false;
		}
		bool init = false; 
		Vector2 vec;
		int DustType = 0;
		bool end = false;
		int Timer2 = 0;
		public override void AI()
		{
            var player = Main.player[Projectile.owner];
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
			if(!init) 
			{
				vec = Vector2.Normalize(Main.MouseWorld - player.Center) * 25;
				init = true;
			}
			if(Timer >= player.itemAnimationMax)
			{
				Projectile.Kill();
				return;
			}
			if(player.channel) Projectile.damage = 0;
			Vector2 unit = Vector2.Normalize(Projectile.velocity);
			Projectile.velocity = unit;
			float factor = 0;
			if(player.channel)
			{
				player.velocity *= 0.9f;
				factor = (Timer + 1) / (float)(player.itemAnimationMax / 4 * 3);
				Projectile.Center = player.RotatedRelativePoint(player.MountedCenter, true) + vec - unit * 3f * factor;
				Vector2 pos = Projectile.Center;
				for (int k = 0; k < factor + 1; k++)
				{
					float size = 0.4f;
					if (k % 2 == 1)
					{
						size = 0.65f;
					}
					Vector2 spawnPos = pos + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (12f - (float)(factor * 2));
					Dust d = Dust.NewDustDirect(spawnPos - Vector2.One * 8f, 16, 16, DustType, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 0, default(Color), 1f);
					d.velocity = Vector2.Normalize(pos - spawnPos) * 1.5f * (10f - factor * 2f) / 10f;
					d.noGravity = true;
					d.scale = size;
					d.customData = player;
				}
			}
			else if(!player.channel)
			{
				factor = (player.itemAnimationMax - Timer) / (float)(player.itemAnimationMax / 4 * 3);
				Projectile.Center = player.RotatedRelativePoint(player.MountedCenter, true) + vec + unit * 5f * factor * Timer * 2 / player.itemAnimationMax + unit * 30 + unit * 30f * factor;
				end = true;
				Projectile.damage = (int)((player.HeldItem.damage + Timer) / 1.2f);
			}
			if(end) Timer2++;
			if(Timer2 == player.itemAnimationMax / 2) Projectile.Kill();
			if(player.channel && Timer < player.itemAnimationMax / 4 * 3) Timer++;
		}
        public override bool PreDraw(ref Color lightColor)
		{
			var player = Main.player[Projectile.owner];
			Vector2 unit = Vector2.Normalize(Projectile.velocity);
			var tex = TextureAssets.Projectile[Projectile.type].Value;
			Main.EntitySpriteDraw(tex, Projectile.Center - unit * 30 - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() * 0.5f, 1f, SpriteEffects.None, 0);
			//if(!player.channel) spriteBatch.Draw(tex2, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			return false;
		}
    }
}