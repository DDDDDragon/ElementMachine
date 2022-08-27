using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ElementMachine.Buffs
{
    public class lowerSpeed : ModBuff 
    {
        public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("减速");
            Description.SetDefault("冰冷刺骨");
            Main.buffNoTimeDisplay[Type] = false;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if(!npc.boss)
            {
                npc.velocity = new Vector2((float)(npc.velocity.X * 0.8), npc.velocity.Y);
                Dust.NewDust(npc.Center,npc.width / 2, npc.height / 2, MyDustId.IceTorch);
            }
            
            base.Update(npc, ref buffIndex);
        }
    }
}
