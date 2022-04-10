using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Buffs;
using Microsoft.Xna.Framework.Graphics;

namespace ElementMachine.NPCs.SandDiablosMonsters
{
	public class SandAntlion : ModNPC
	{
        public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 5;
		}
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.Antlion);
            base.SetDefaults();
        }
        public override bool PreAI()
        {
            npc.frameCounter++;
            if(npc.frameCounter == 15)
            {
                if(npc.frame.Y < 160) npc.frame.Y += 40;
                else npc.frame.Y = 0;
                npc.frameCounter = 0;
            }
            return true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D tex = ModContent.GetTexture("ElementMachine/NPCs/SandDiablosMonsters/SandAntlionBody");
            Main.spriteBatch.Draw(tex, npc.position + new Vector2(0, 16) - Main.screenPosition, Color.White);
            return true;
        }
    }
}