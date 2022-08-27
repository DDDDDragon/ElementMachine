using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Buffs;

namespace ElementMachine.NPCs.ElementCreatures
{
	public class EarthStatue : ElementCreaturesBase
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 28;
		}

		public override void SetDefaults()
		{
			NPC.friendly = false;
			NPC.width = 42;
			NPC.height = 50;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath7;
			NPC.value = 5;
			NPC.damage = 20;
			NPC.defense = 6;
			NPC.lifeMax = 100;
			NPC.aiStyle = -1;
			NPC.noGravity = false;
			ElementLevel = 1.4f;
			Element = 3;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0;
			//return SpawnCondition.OverworldDay.Chance * 0.1f;
		}
		public override void PostAI()
		{
			Lighting.AddLight(NPC.Center, 0.1f, 0.5f, 0.5f);
		}
		public int timer1;
		private enum NPCState
        {
			Initialize,
			attack,
			down
        }
		private NPCState State
		{
			get { return (NPCState)(int)NPC.ai[0]; }
			set { NPC.ai[0] = (int)value; }
		}
		private void SwitchTo(NPCState state)
        {
			State = state;
        }
#pragma warning disable CS0414 // 字段“EarthStatue.timer2”已被赋值，但从未使用过它的值
#pragma warning disable CS0414 // The field 'EarthStatue.timer2' is assigned but its value is never used
		int timer2 = 0;
#pragma warning restore CS0414 // The field 'EarthStatue.timer2' is assigned but its value is never used
#pragma warning restore CS0414 // 字段“EarthStatue.timer2”已被赋值，但从未使用过它的值
		int timer3 = 0;
		bool chuansong = false;
		bool attack = false;
		public override void AI()
        {
			NPC.TargetClosest(true);
			NPC.spriteDirection = NPC.direction;
			Player player = Main.player[NPC.target];
			Vector2 vec = new Vector2(60, -5);
			timer1++;
			timer3++;
			if(timer3 == 0 && attack)
            {
				timer3 = 0;
            }
			if(timer3 >= 300)
            {
				timer3 = 300;
            }
			NPC.velocity.Y = 0;
			if(timer1 == 10 && chuansong == false)
            {
				NPC.frame.Y += 52;
				timer1 = 0;
            }			
			else if(timer1 == 10 && chuansong && NPC.frame.Y > 676)
            {
				NPC.frame.Y -= 52;
				timer1 = 0;
            }
			if (NPC.frame.Y == 676 && !NPC.collideX)
            {
				NPC.frame.Y = 0;
            }
			if(NPC.frame.Y <= 676 && chuansong)
            {
				chuansong = false;
            }
			if(NPC.frame.Y == 988 && !attack && !player.dead)
            {
				chuansong = true;
				NPC.Center = player.Center + vec;
            }
			if(!attack && chuansong == false && Vector2.Distance(NPC.Center, player.Center) <= 180 && !player.dead && timer3==300)
            {
				timer3 = 0;
				attack = true;
				NPC.frame.Y = 1040;
            }
			bool shoot = false;
			if(attack && NPC.frame.Y >= 1300 && Vector2.Distance(NPC.Center, player.Center) <= 360 && !player.dead)
            {
				player.velocity = Vector2.Zero;
				player.velocity.Y = -2;
				player.velocity.X = NPC.velocity.X;
				Vector2 vec2 = Vector2.Normalize(player.Center - NPC.Center) * 100f;
				player.AddBuff(ModContent.BuffType<Dizziness>(), 120);
				if (!shoot)
				{
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vec2, ModContent.ProjectileType<cq>(), 20, 2f);
					shoot = true;
				}
            }
			if(attack && NPC.frame.Y == 1456)
            {
				attack = false;
				NPC.frame.Y = 0;
				shoot = false;
            }
			switch (State)
            {
				case NPCState.Initialize:
					NPC.ai[0] = Main.rand.NextBool() ? -1 : 1;
					SwitchTo(NPCState.attack);
					break;
				case NPCState.attack:
					var targetVel = Vector2.Normalize(player.Center - NPC.Center) * 1f;	
					NPC.velocity.X = (targetVel.X + NPC.velocity.X * 1) / 3f;
					if (player.dead)
					{
						NPC.velocity.X = NPC.ai[0];
						NPC.target = 0;
					}
					break;
			}
		}
    }
	public class cq : ModProjectile
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("cq");
		}
        public override void SetDefaults()
        {
			Projectile.width = 44;
			Projectile.height = 16;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = false;
        }
		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation() + 0.785f + 0.785f;
		}
	}
}