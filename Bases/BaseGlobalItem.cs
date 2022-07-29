using System;
using System.Collections.ObjectModel;
using Terraria;
using Terraria.ModLoader;
namespace ElementMachine.Bases
{
    public class BaseGlobalItem : GlobalItem
    {
        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            base.ModifyWeaponDamage(item, player, ref damage);
            if (item.ModItem != null && item.ModItem is BaseSickleItem) damage += Main.LocalPlayer.GetModPlayer<BasePlayer>().SickleDamagePer;
        }
    }
}