using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Buffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.Utilities;

namespace ElementMachine.NPCs.SandDiablosMonsters
{
	public class SandAntlion : ModNPC
	{
        public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 5;
		}
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Antlion);
            base.SetDefaults();
        }
        public override bool PreAI()
        {
            NPC.frameCounter++;
            if(NPC.frameCounter == 15)
            {
                if(NPC.frame.Y < 160) NPC.frame.Y += 40;
                else NPC.frame.Y = 0;
                NPC.frameCounter = 0;
            }
            return true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("ElementMachine/NPCs/SandDiablosMonsters/SandAntlionBody").Value;
            Main.spriteBatch.Draw(tex, NPC.position + new Vector2(0, 16) - Main.screenPosition, Color.White);
            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (MyWorld.SandDiablos && spawnInfo.Player.ZoneDesert) return SpawnCondition.OverworldDayDesert.Chance;
            else return SpawnCondition.OverworldDayDesert.Chance / 2;
        }
    }
}