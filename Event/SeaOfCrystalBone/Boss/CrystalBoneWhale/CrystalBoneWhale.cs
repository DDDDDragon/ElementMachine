using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Event.SeaOfCrystalBone.BossItem.CrystalBoneWhale;
using System;

namespace ElementMachine.Event.SeaOfCrystalBone.Boss.CrystalBoneWhale
{
	public class CrystalBoneWhale : ModNPC
	{
		public override void SetStaticDefaults()
		{
            Main.npcFrameCount[NPC.type] = 8;
		}

		public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.damage = 20;
			NPC.defense = 10;
			NPC.lifeMax = 2000;
			NPC.height = 118;
			NPC.width = 250;
			NPC.boss = true;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath7;
			NPC.noGravity = true;
			NPC.value = 5;
		}
		public bool dig = false;
        public override bool CheckDead()
        {
			if(!dig)
            {
				NPC.life = 50;
				dig = true;
				return false;
			}
            return base.CheckDead();
        }
        public override void AI()
		{
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			NPC.friendly = false;
			NPC.noGravity = false;
			
			NPC.spriteDirection = NPC.direction;
			if(NPC.wet)
			{
				if(NPC.life > 50)
				{
					NPC.frameCounter++;
					var targetVel = Vector2.Normalize(player.Center - NPC.Center) * 3f;
					NPC.velocity = (targetVel + NPC.velocity * 3) / 4f;
					NPC.noGravity = true;
					NPC.rotation = NPC.velocity.Y * (float)NPC.direction * 0.1f;
					if((double)NPC.rotation < -0.2)
					{
						NPC.rotation = -0.2f;
					}
					if((double)NPC.rotation > 0.2)
					{
						NPC.rotation = 0.2f;
					}
					if(NPC.frameCounter == 5 && NPC.frame.Y != 700)
					{
						NPC.frame.Y += 100;
						NPC.frameCounter = 0;
					}
					if(NPC.frameCounter == 5 && NPC.frame.Y == 700) 
					{
						NPC.frame.Y = 0;
						NPC.frameCounter = 0;
					}
				}
				if(NPC.life <= 50)
				{
					if(!dig)
                    {
						NPC.life = 50;
						dig = true;
                    }
					NPC.immortal = true;
					NPC.velocity = Vector2.Zero;
					NPC.rotation = 0;
					NPC.frame.Y = 0;
					Rectangle MouseRec = new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1);
					if(NPC.getRect().Intersects(MouseRec) && Main.mouseLeft && Main.LocalPlayer.HeldItem.pick > 0)
					{
						digTimer++;
						if(digTimer == 120)
						{
							if(Main.LocalPlayer.HeldItem.pick <= 40)
							{
								Main.LocalPlayer.QuickSpawnItem(null, ModContent.ItemType<WhaleBonePiece>(), Main.rand.Next(1, 4));
								NPC.life -= 2;
								digTimer = 0;
							}
							if(Main.LocalPlayer.HeldItem.pick > 40 && Main.LocalPlayer.HeldItem.pick <= 80)
							{
								if(Main.rand.Next(1, 3) == 1)
								{
									Main.LocalPlayer.QuickSpawnItem(null, ModContent.ItemType<WhaleOre>(), Main.rand.Next(1, 3));
									NPC.life -= 4;
									digTimer = 0;
								}
								else
								{
									Main.LocalPlayer.QuickSpawnItem(null, ModContent.ItemType<WhaleBonePiece>(), Main.rand.Next(1, 4));
									NPC.life -= 2;
									digTimer = 0;
								}
							}
						}
					}
				}
			}
		}
		int digTimer = 0;
    }
}