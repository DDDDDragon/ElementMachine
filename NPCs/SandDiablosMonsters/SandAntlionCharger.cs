using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Buffs;
using Microsoft.Xna.Framework.Graphics;

namespace ElementMachine.NPCs.SandDiablosMonsters
{
	public class SandAntlionCharger : ModNPC
	{
        public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 6;
		}
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.WalkingAntlion);
            npc.aiStyle = 3;
            aiType = NPCID.WalkingAntlion;
            animationType = NPCID.WalkingAntlion;
            base.SetDefaults();
        }
    }
}