using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using ElementMachine.NPCs.ElementCreatures;

namespace ElementMachine.Element
{
    public class ElementCatcher : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ElementCatcher");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "元素捕手");
			Tooltip.SetDefault("Pokemon Get⭐Daze! Wait a second... What is Pokemon?");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "宝可梦Get⭐Daze！等会...啥事宝可梦啊？");
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.channel = true;
			Item.width = 20;
			Item.height = 20;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.Green;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<ElementCatcherProj>();
			Item.shootSpeed = 10f;
			Item.autoReuse = true;
		}
		public override bool CanShoot(Player player)
		{
			foreach (var i in Main.projectile)
			{
				if (i.type == ModContent.ProjectileType<ElementCatcherProj>() && i.active) return false;
			}
			return base.CanShoot(player);
		}
	}
	public class ElementCatcherProj : ModProjectile
    {
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1.3f;
			Projectile.alpha = 255;

			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
		}
		public int Timer
		{
			get { return (int)Projectile.ai[0]; }
			set { Projectile.ai[0] = value; }
		}
		Vector2 vec;
		int DustType = 0;
		bool end = false;
		int Timer2 = 0;
		public override void AI()
		{
			var player = Main.player[Projectile.owner];
			player.direction = (Main.MouseWorld.X - player.Center.X) > 0 ? 1 : -1;
			Projectile.spriteDirection = Projectile.direction = (Main.MouseWorld.X - player.Center.X) > 0 ? 1 : -1;
			Projectile.rotation = (Main.MouseWorld - player.Center).ToRotation();
			vec = Vector2.Normalize(Main.MouseWorld - player.Center) * 20;
			if (Timer >= player.itemAnimationMax)
			{
				Projectile.Kill();
				return;
			}
			if (player.channel) Projectile.damage = 0;
			Vector2 unit = Vector2.Normalize(Projectile.velocity);
			Projectile.velocity = unit;
			float factor = 0;
			if (player.channel)
			{
				player.velocity *= 0.9f;
				factor = (Timer + 1) / (float)(player.itemAnimationMax / 4 * 3);
				Projectile.Center = player.RotatedRelativePoint(player.MountedCenter, true) + vec;
				player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2);
				Vector2 pos = Projectile.Center + Vector2.Normalize(Main.MouseWorld - player.Center) * 15;
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
			else Projectile.Kill();
			foreach(var target in Main.npc)
            {
				if (target.ModNPC is ElementCreaturesBase && target.active)
				{
					if ((target.ModNPC as ElementCreaturesBase).Level == 2) (target.ModNPC as ElementCreaturesBase).OnCatch();
				}
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			var tex = TextureAssets.Projectile[Projectile.type].Value;
			Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() * 0.5f, 1f, SpriteEffects.None, 0);
			//if(!player.channel) spriteBatch.Draw(tex2, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			return false;
		}
    }
}
