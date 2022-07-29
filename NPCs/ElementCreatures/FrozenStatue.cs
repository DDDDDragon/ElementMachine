using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using ElementMachine.Element.Ice.Frozen;
using Terraria.GameContent.ItemDropRules;

namespace ElementMachine.NPCs.ElementCreatures
{
	public class FrozenStatue : ElementCreaturesBase
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("FrozenStatue");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "霜寒石像");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
		{
			NPC.friendly = false;
			NPC.width = 30;
			NPC.height = 46;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath7;
			NPC.value = 5;
			NPC.damage = 10;
			NPC.defense = 6;
			NPC.lifeMax = 40;
			NPC.aiStyle = 3;
			NPC.noGravity = false;
			Level = 2;
		}
		public override void PostAI()
		{
			NPC.TargetClosest();
			Player player = Main.player[NPC.target];
			NPC.spriteDirection = NPC.direction;
			NPC.velocity.X = (Vector2.Normalize(player.Center - NPC.Center) / 1.5f).X;
			NPC.frameCounter++;
			if(NPC.frameCounter == 15)
			{
				if(NPC.frame.Y != 156)
				{
					NPC.frame.Y += 52;
					NPC.frameCounter = 0;
				}
				else 
				{
					NPC.frame.Y = 0;
					NPC.frameCounter = 0;
				}
				
			}
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrozenStone>(), 1, 1, 4));
			base.ModifyNPCLoot(npcLoot);
		}
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.Player.ZoneSnow ? 0.7f : 0f;
		}
    }
}