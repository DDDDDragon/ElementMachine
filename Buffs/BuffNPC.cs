using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace ElementMachine.Buffs
{
    public class BuffNPC : GlobalNPC
    {
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if(npc.HasBuff(ModContent.BuffType<ArmorBroken>()))
            {
                damage = (int)(damage * 1.1f);
            }
            base.ModifyHitByItem(npc, player, item, ref damage, ref knockback, ref crit);
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(npc.HasBuff(ModContent.BuffType<ArmorBroken>()))
            {
                damage = (int)(damage * 1.1f);
            }
            base.ModifyHitByProjectile(npc, projectile, ref damage, ref knockback, ref crit, ref hitDirection);
        }
    }
}