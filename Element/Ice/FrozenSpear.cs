using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Buffs;
using ElementMachine.Tiles;

namespace ElementMachine.Element.Ice
{
	public class FrozenSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("FrozenSpear");
			DisplayName.AddTranslation(GameCulture.Chinese, "霜寒长矛");
			Tooltip.SetDefault("it's so cold\nleft click to channel and attack, right click to throw it as a Thrown Spear");
			Tooltip.AddTranslation(GameCulture.Chinese, "冰冷刺骨\n左键蓄力刺出,右键可以当作投矛使用");
		}

		public override void SetDefaults()
		{
			item.damage = 13;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 28;
			item.useTime = 36;
			item.shootSpeed = 10f;
			item.width = 48;
			item.height = 48;
			item.scale = 1f;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 10);
			item.melee = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.autoReuse = true;
			item.UseSound = SoundID.Item1;
			item.shoot = ModContent.ProjectileType<Frozenproj>();
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse == 2)
            {
				item.shoot = ModContent.ProjectileType<Frozenproj2>();
			}
            else
            {
				item.shoot = ModContent.ProjectileType<Frozenproj>();
            }
			return true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			foreach(var i in Main.projectile)
			{
				if(i.type == ModContent.ProjectileType<Frozenproj>() && i.active) return false;
			}
            return true;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceBlock, 20);
			recipe.AddIngredient(ItemID.SnowBlock, 20);
			recipe.AddIngredient(ItemID.SlushBlock, 20);
			recipe.AddIngredient(ModContent.ItemType<FrozenStone>(), 7);
			recipe.AddTile(ModContent.TileType<ElementHoroScpoer>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
    public class Frozenproj2 : ModProjectile
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
		int Vtimer1 = 0;
		bool init = false;
        /*public override void AI()
        {
			Player projOwner = Main.player[projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			projectile.direction = projOwner.direction;
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			if(!init)
            {
				projectile.Center = projOwner.Center;
				init = true;
            }
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation -= MathHelper.ToRadians(90f);
			}
			if (Vtimer1 <= 360)
            {
				Vtimer1++;
				projectile.velocity += Vtimer1 * Vector2.Normalize(projectile.position - projectile.oldPosition) / 20;
            }
			Dust dus1 = Dust.NewDustDirect(projectile.Center, 1, 1, MyDustId.BlueIce);
			

		}*/
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(ModContent.BuffType<lowerSpeed>(), 180);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }

	public class Frozenproj : ModProjectile
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
			projectile.melee = true;
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
			projectile.spriteDirection = projectile.direction;
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2 + MathHelper.PiOver4;
			var player = Main.player[projectile.owner];
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
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<lowerSpeed>(), 180);
			base.OnHitNPC(target, damage, knockback, crit);
		}
	}
}
