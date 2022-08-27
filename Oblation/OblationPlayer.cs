using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;

namespace ElementMachine.Oblation
{
    public class OblationPlayer : ModPlayer
    {
    }
    public class OblationGlobalItem : GlobalItem
    {
        public static bool IceShoot = false;
        public static int IceMaxDamage = 20;
        public static int IceBaseDamage = 1;
        public override bool? UseItem(Item item, Player player)
        {
            return base.UseItem(item, player);
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            tag.Add("IceShoot", IceShoot);
            base.SaveData(item, tag);
        }
        public override void LoadData(Item item, TagCompound tag)
        {
            IceShoot = tag.GetBool("IceShoot");
            base.LoadData(item, tag);
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (item.ModItem == null) return;
            ElementItem EItem = item.ModItem as ElementItem;
            if (IceShoot && ElementMachine.GetElementName(EItem.Element) == "Ice") 
            {
                //if(IceSpear) Projectile.NewProjectile(player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 10, ModContent.ProjectileType<IceSpearProj>(), Math.Min(Math.Max(IceBaseDamage, (int)(Item.damage * 0.3) + player.statDefense / 10 + IceBaseDamage), IceMaxDamage), 1, Main.myPlayer);
                Projectile.NewProjectile(null, player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 10, ProjectileID.IceBolt, Math.Min(Math.Max(IceBaseDamage, (int)(item.damage * 0.3) + player.statDefense / 10 + IceBaseDamage), IceMaxDamage), 1, Main.myPlayer);
            }
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

    }
}