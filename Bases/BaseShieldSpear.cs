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

namespace ElementMachine.Bases
{
    public abstract class BaseShieldSpear : ElementItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            SetSpear();
            DisplayName.SetDefault(defaultName);
			DisplayName.AddTranslation(GameCulture.Chinese, transName);
			Tooltip.SetDefault(tooltipEn + "left click to channel and attack, while channeling, your defense will increase, right click to throw it as a Thrown Spear\n[c/8c6640:「Special Effect」]" + EffectEn);
			Tooltip.AddTranslation(GameCulture.Chinese, tooltip + "\n[c/00FF00:「盾矛」]\n左键蓄力刺出,蓄力时防御力增加,右键可以当作投矛使用\n[c/8c6640:「特效」]\n" + Effect);
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset) 
		{
			base.PreDrawTooltipLine(line, ref yOffset);
			if (!line.oneDropLogo) 
			{
				string sepText = "[c/00FF00:「" + (Main.LocalPlayer.GetModPlayer<BasePlayer>().ShieldSpearDefensePer - 1) * 100 + "%防御加成」],[c/00FF00:「" + Main.LocalPlayer.GetModPlayer<BasePlayer>().ShieldSpearExtraDefense + "额外防御」]";
				if (line.Name == "Damage" && line.mod == "Terraria")
				{
					Color color = new Color(220,220,157);
					float drawX = line.X + line.font.MeasureString(sepText).X - 300;
					float drawY = line.Y;
					ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.font, sepText,
						new Vector2(drawX, drawY), line.color, line.rotation, line.origin, line.baseScale, line.maxWidth, line.spread);
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
			item.damage = damage;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 28;
			item.useTime = 36;
			item.shootSpeed = shootSpeed;
			item.width = 48;
			item.height = 48;
			item.scale = 1f;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 10);
			item.summon = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.autoReuse = true;
			item.UseSound = SoundID.Item1;
			item.shoot = SpearProjType;
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
				item.shoot = ThrownProjType;
			}
            else
            {
				item.shoot = SpearProjType;
            }
			return true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			foreach(var i in Main.projectile)
			{
				if(i.type == SpearProjType && i.active) return false;
			}
            return true;
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
			projectile.width = 5;
			projectile.height = 20;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.CloneDefaults(ProjectileID.JavelinFriendly);
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
			projectile.width = 72;
			projectile.height = 76;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.scale = 1.3f;
			projectile.alpha = 255;

			projectile.ownerHitCheck = true;
			projectile.minion = true;
			projectile.tileCollide = false;
			projectile.friendly = true;
		}
		public int Timer
		{
			get { return (int)projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}
		public override bool ShouldUpdatePosition()
		{
			return false;
		}
		bool init = false; 
		Vector2 vec;
		Vector2 vec2;
		bool end = false;
		int Timer2 = 0;
		public override void AI()
		{
            var player = Main.player[projectile.owner];
			projectile.spriteDirection = projectile.direction;
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
			if(!init) 
			{
				vec = Vector2.Normalize(Main.MouseWorld - player.Center) * 25;
				init = true;
			}
			if(Timer >= player.itemAnimationMax)
			{
				projectile.Kill();
				return;
			}
			if(player.channel) projectile.damage = 0;
			Vector2 unit = Vector2.Normalize(projectile.velocity);
			projectile.velocity = unit;
			float factor = 0;
			if(player.channel)
			{
				factor = (Timer + 1) / (float)(player.itemAnimationMax / 4 * 3);
				projectile.Center = player.RotatedRelativePoint(player.MountedCenter, true) + vec - unit * 3f * factor;
				Vector2 pos = projectile.Center;
				for (int k = 0; k < factor + 1; k++)
				{
					int type = 226;
					float size = 0.4f;
					if (k % 2 == 1)
					{
						type = 226;
						size = 0.65f;
					}
					Vector2 spawnPos = pos + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (12f - (float)(factor * 2));
					Dust d = Dust.NewDustDirect(spawnPos - Vector2.One * 8f, 16, 16, type, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 0, default(Color), 1f);
					d.velocity = Vector2.Normalize(pos - spawnPos) * 1.5f * (10f - factor * 2f) / 10f;
					d.noGravity = true;
					d.scale = size;
					d.customData = player;
				}
			}
			else if(!player.channel)
			{
				factor = (player.itemAnimationMax - Timer) / (float)(player.itemAnimationMax / 4 * 3);
				projectile.Center = player.RotatedRelativePoint(player.MountedCenter, true) + vec + unit * 5f * factor * Timer * 2 / player.itemAnimationMax + unit * 30 + unit * 30f * factor;
				end = true;
				projectile.damage = 1 + Timer;
			}
			if(end) Timer2++;
			if(Timer2 == player.itemAnimationMax / 2) projectile.Kill();
			if(player.channel && Timer < player.itemAnimationMax / 4 * 3) Timer++;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var player = Main.player[projectile.owner];
			Vector2 unit = Vector2.Normalize(projectile.velocity);
			var tex = Main.projectileTexture[projectile.type];
			spriteBatch.Draw(tex, projectile.Center - unit * 30 - Main.screenPosition, null, Color.White, projectile.rotation, tex.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			//if(!player.channel) spriteBatch.Draw(tex2, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation, tex.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			return false;
		}
    }
}