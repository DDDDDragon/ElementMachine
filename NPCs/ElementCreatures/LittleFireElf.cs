using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using ElementMachine.Element.Ice.Frozen;
using Terraria.GameContent.ItemDropRules;
using System;

namespace ElementMachine.NPCs.ElementCreatures
{
    public class LittleFireElf : ElementCreaturesBase
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("LittleFireElf");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "小火灵");
			Main.npcFrameCount[NPC.type] = 6;
		}
		public override void SetDefaults()
		{
			NPC.friendly = false;
			NPC.width = 30;
			NPC.height = 46;
			NPC.HitSound = SoundID.NPCHit5;
			NPC.DeathSound = SoundID.NPCDeath7;
			NPC.value = 5;
			NPC.damage = 5;
			NPC.defense = 2;
			NPC.lifeMax = 10;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			Level = 2;
		}
		bool wait = true;
		int waitTimer = 0;
		bool init = false;
		Vector2 StartCen = new Vector2();
		Vector2 nex = new Vector2();
		public override void PostAI()
		{
			if (!init)
			{
				StartCen = NPC.Center;
				init = true;
			}
            if (wait)
			{
				NPC.velocity = Vector2.Zero;
				waitTimer++;
			}
			if(!wait)
            {
				if (NPC.Center.Distance(nex) >= 5) NPC.velocity = Vector2.Normalize(nex - NPC.Center) * 2f * Vector2.Distance(nex, NPC.Center) / 50;
				else wait = true;
			}
			if (waitTimer == 180)
			{
				wait = false;
				waitTimer = 0;
				float t = Main.rand.NextFloat(0, (float)Math.PI * 2);
				nex = StartCen + new Vector2((float)Math.Sin(t), (float)Math.Cos(t)) * Main.rand.Next(30, 80);
			}
            NPC.frameCounter++;
			if (NPC.frameCounter == 15)
			{
				if (NPC.frame.Y != 230)
				{
					NPC.frame.Y += 46;
					NPC.frameCounter = 0;
				}
				else
				{
					NPC.frame.Y = 0;
					NPC.frameCounter = 0;
				}

			}
		}
        public override void OnCatch()
        {
			Item.NewItem(null, Main.LocalPlayer.Center, ItemID.Wood);
            base.OnCatch();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemNPC.type<FrozenStone>(), 1, 1, 4));
			base.ModifyNPCLoot(npcLoot);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.Player.ZoneForest ? 0.7f : 0f;
		}
	}
}
