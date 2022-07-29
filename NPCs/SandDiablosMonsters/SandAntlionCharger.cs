using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Buffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.Utilities;

namespace ElementMachine.NPCs.SandDiablosMonsters
{
	public class SandAntlionCharger : ModNPC
	{
        public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 6;
		}
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.WalkingAntlion);
            NPC.aiStyle = 3;
            AIType = NPCID.WalkingAntlion;
            AnimationType = NPCID.WalkingAntlion;
            base.SetDefaults();
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if(MyWorld.SandDiablos && spawnInfo.Player.ZoneDesert) return SpawnCondition.DesertCave.Chance;
            else return SpawnCondition.DesertCave.Chance / 2;
        }
    }
}