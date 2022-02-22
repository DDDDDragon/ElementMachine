using System;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;
namespace ElementMachine.Bases
{
    public class BaseGlobalItem : GlobalItem
    {
        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            base.ModifyWeaponDamage(item, player, ref add, ref mult, ref flat);
            if(item.modItem != null && item.modItem is BaseSickleItem) add += Main.LocalPlayer.GetModPlayer<BasePlayer>().SickleDamagePer;  
        }
    }
}