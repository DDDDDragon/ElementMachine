using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;
using ElementMachine.Machine;
using ElementMachine.Buffs;

namespace ElementMachine
{
    public abstract class MachineItem : ModItem
    {
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if(player.GetModPlayer<MachinePlayer>().MachineArmorSet)
            {
                player.AddBuff(ModContent.BuffType<HydraulicEngine>(), 1);
                if(player.GetModPlayer<BuffPlayer>().JuniorAlloyNum < 5)player.GetModPlayer<BuffPlayer>().JuniorAlloyNum++;
                player.GetModPlayer<BuffPlayer>().JuniorAlloyTimer = 240;
            }
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
        public enum Energy : int
        {
            None,
            Flame,
            Ice
        }
        /// <summary>
        /// 物品的能量属性,为一个list
        /// </summary>
        public List<Energy> energyTypes = new List<Energy>(){Energy.None};
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (!line.oneDropLogo) 
			{
				if (line.Name == "Damage" && line.mod == "Terraria")
				{
					//float drawX = line.X + line.font.MeasureString(sepText).X - 150;
					float drawY = line.Y;
				}
				else 
				{
					yOffset = 0;
				}
			}
			return true;
        }
    }
}