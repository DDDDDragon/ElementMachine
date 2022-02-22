using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Buffs;

namespace ElementMachine.NPCs
{
	public class EarthStatue : ModNPC
	{
		public override void NPCLoot()
		{
			
		}
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 28;
		}

		public override void SetDefaults()
		{
			npc.friendly = false;
			npc.width = 42;
			npc.height = 50;
			npc.HitSound = SoundID.NPCHit5;
			npc.DeathSound = SoundID.NPCDeath7;
			npc.value = 5;
			npc.damage = 20;
			npc.defense = 6;
			npc.lifeMax = 100;
			npc.aiStyle = -1;
			npc.noGravity = false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0;
			//return SpawnCondition.OverworldDay.Chance * 0.1f;
		}
		public override void PostAI()
		{
			Lighting.AddLight(npc.Center, 0.1f, 0.5f, 0.5f);
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
			get { return (NPCState)(int)npc.ai[0]; }
			set { npc.ai[0] = (int)value; }
		}
		private void SwitchTo(NPCState state)
        {
			State = state;
        }
		int timer2 = 0;
		int timer3 = 0;
		bool chuansong = false;
		bool attack = false;
		public override void AI()
        {
			npc.TargetClosest(true);
			npc.spriteDirection = npc.direction;
			Player player = Main.player[npc.target];
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
			npc.velocity.Y = 0;
			if(timer1 == 10 && chuansong == false)
            {
				npc.frame.Y += 52;
				timer1 = 0;
            }			
			else if(timer1 == 10 && chuansong && npc.frame.Y > 676)
            {
				npc.frame.Y -= 52;
				timer1 = 0;
            }
			if (npc.frame.Y == 676 && !npc.collideX)
            {
				npc.frame.Y = 0;
            }
			if(npc.frame.Y <= 676 && chuansong)
            {
				chuansong = false;
            }
			if(npc.frame.Y == 988 && !attack && !player.dead)
            {
				chuansong = true;
				npc.Center = player.Center + vec;
            }
			if(!attack && chuansong == false && Vector2.Distance(npc.Center, player.Center) <= 180 && !player.dead && timer3==300)
            {
				timer3 = 0;
				attack = true;
				npc.frame.Y = 1040;
            }
			bool shoot = false;
			if(attack && npc.frame.Y >= 1300 && Vector2.Distance(npc.Center, player.Center) <= 360 && !player.dead)
            {
				player.velocity = Vector2.Zero;
				player.velocity.Y = -2;
				player.velocity.X = npc.velocity.X;
				Vector2 vec2 = Vector2.Normalize(player.Center - npc.Center) * 100f;
				player.AddBuff(ModContent.BuffType<Dizziness>(), 120);
				if (!shoot)
				{
					Projectile.NewProjectile(npc.Center, vec2, ModContent.ProjectileType<cq>(), 20, 2f);
					shoot = true;
				}
            }
			if(attack && npc.frame.Y == 1456)
            {
				attack = false;
				npc.frame.Y = 0;
				shoot = false;
            }
			switch (State)
            {
				case NPCState.Initialize:
					npc.ai[0] = Main.rand.NextBool() ? -1 : 1;
					SwitchTo(NPCState.attack);
					break;
				case NPCState.attack:
					var targetVel = Vector2.Normalize(player.Center - npc.Center) * 1f;	
					npc.velocity.X = (targetVel.X + npc.velocity.X * 1) / 3f;
					if (player.dead)
					{
						npc.velocity.X = npc.ai[0];
						npc.target = 0;
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
			projectile.width = 44;
			projectile.height = 16;
			projectile.tileCollide = false;
			projectile.ignoreWater = false;
        }
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + 0.785f + 0.785f;
		}
	}
}