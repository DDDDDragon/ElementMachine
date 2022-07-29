using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ElementMachine.Tiles;
using Terraria.DataStructures;

namespace ElementMachine.Element.Ice.Frozen
{
	public class FrozenMinionBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("FrozenStoneFairy");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "霜寒石灵");
			Description.SetDefault("The Frozen Stone Fairy will fight for you");
			Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "霜寒石灵会为你而战");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) 
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<FrozenMinion>()] > 0) 
			{
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}

	public class FrozenMinionStaff : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("FrozenStoneFairyStaff");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "霜寒石灵召唤杖");
			Tooltip.SetDefault("Summons a Frozen Stone Fairy to fight for you");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "霜寒石灵会为你而战");
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;  
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		}

		public override void SetDefaults() {
			Item.damage = 6;
			Item.knockBack = 3f;
			Item.mana = 10;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item44;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = ModContent.BuffType<FrozenMinionBuff>();
			Item.shoot = ModContent.ProjectileType<FrozenMinion>();
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			player.AddBuff(Item.buffType, 2);
			position = Main.MouseWorld;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IceBlock, 20);
			recipe.AddIngredient(ItemID.SnowBlock, 20);
			recipe.AddIngredient(ItemID.SlushBlock, 20);
			recipe.AddIngredient(ModContent.ItemType<FrozenStone>(), 7);
			recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
	}

	  
	public class FrozenMinion : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("FrozenMinion");
			Main.projFrames[Projectile.type] = 5;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public sealed override void SetDefaults() {
			Projectile.width = 18;
			Projectile.height = 28;
			Projectile.tileCollide = false;
			Projectile.friendly = true; 
			Projectile.minion = true;
			Projectile.minionSlots = 1f;
			Projectile.penetrate = -1;
		}

		 
		public override bool? CanCutTiles() {
			return false;
		}

		 
		public override bool MinionContactDamage() {
			return true;
		}

		public override void AI() {
			Player player = Main.player[Projectile.owner];

			#region Active check
			 
			if (player.dead || !player.active) {
				player.ClearBuff(ModContent.BuffType<FrozenMinionBuff>());
			}
			if (player.HasBuff(ModContent.BuffType<FrozenMinionBuff>())) {
				Projectile.timeLeft = 2;
			}
			#endregion

			#region General behavior
			Vector2 idlePosition = player.Center;
			idlePosition.Y -= 48f;  

			 
			 
			float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -player.direction;
			idlePosition.X += minionPositionOffsetX;  

			 

			 
			Vector2 vectorToIdlePosition = idlePosition - Projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();
			if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f) {
				 
				 
				Projectile.position = idlePosition;
				Projectile.velocity *= 0.1f;
				Projectile.netUpdate = true;
			}

			 
			float overlapVelocity = 0.04f;
			for (int i = 0; i < Main.maxProjectiles; i++) {
				 
				Projectile other = Main.projectile[i];
				if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width) {
					if (Projectile.position.X < other.position.X) Projectile.velocity.X -= overlapVelocity;
					else Projectile.velocity.X += overlapVelocity;

					if (Projectile.position.Y < other.position.Y) Projectile.velocity.Y -= overlapVelocity;
					else Projectile.velocity.Y += overlapVelocity;
				}
			}
			#endregion

			#region Find target
			 
			float distanceFromTarget = 700f;
			Vector2 targetCenter = Projectile.position;
			bool foundTarget = false;

			 
			if (player.HasMinionAttackTargetNPC) {
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, Projectile.Center);
				 
				if (between < 2000f) {
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}
			if (!foundTarget) {
				 
				for (int i = 0; i < Main.maxNPCs; i++) {
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy()) {
						float between = Vector2.Distance(npc.Center, Projectile.Center);
						bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
						 
						 
						bool closeThroughWall = between < 100f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall)) {
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			 
			 
			 
			 
			Projectile.friendly = foundTarget;
			#endregion

			#region Movement

			 
			float speed = 8f;
			float inertia = 20f;

			if (foundTarget) {
				 
				if (distanceFromTarget > 40f) {
					 
					Vector2 direction = targetCenter - Projectile.Center;
					direction.Normalize();
					direction *= speed;
					Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
				}
			}
			else {
				 
				if (distanceToIdlePosition > 600f) {
					 
					speed = 12f;
					inertia = 60f;
				}
				else {
					 
					speed = 4f;
					inertia = 80f;
				}
				if (distanceToIdlePosition > 20f) {
					 

					 
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (Projectile.velocity == Vector2.Zero) {
					 
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
			}
			#endregion

			#region Animation and visuals
			 
			Projectile.rotation = Projectile.velocity.X * 0.05f;

			 
			int frameSpeed = 5;
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;
				if (Projectile.frame >= Main.projFrames[Projectile.type]) {
					Projectile.frame = 0;
				}
			}

			 
			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
			#endregion
		}
	}
}