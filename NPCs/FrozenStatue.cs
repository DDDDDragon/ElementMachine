using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace ElementMachine.NPCs
{
	public class FrozenStatue : ModNPC
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
			DisplayName.SetDefault("FrozenStatue");
			DisplayName.AddTranslation(GameCulture.Chinese, "霜寒石像");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
		{
			npc.friendly = false;
			npc.width = 30;
			npc.height = 46;
			npc.HitSound = SoundID.NPCHit5;
			npc.DeathSound = SoundID.NPCDeath7;
			npc.value = 5;
			npc.damage = 10;
			npc.defense = 6;
			npc.lifeMax = 40;
			npc.aiStyle = 3;
			npc.noGravity = false;
		}
		public override void PostAI()
		{
			npc.TargetClosest();
			Player player = Main.player[npc.target];
			npc.spriteDirection = npc.direction;
			npc.velocity.X = (Vector2.Normalize(player.Center - npc.Center) / 1.5f).X;
			npc.frameCounter++;
			if(npc.frameCounter == 15)
			{
				if(npc.frame.Y != 156)
				{
					npc.frame.Y += 52;
					npc.frameCounter = 0;
				}
				else 
				{
					npc.frame.Y = 0;
					npc.frameCounter = 0;
				}
				
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDaySnowCritter.Chance * 0.1f;
		}
		public override void AI()
		{
		}
    }
}