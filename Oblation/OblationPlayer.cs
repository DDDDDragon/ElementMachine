using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

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
        public override bool UseItem(Item item, Player player)
        {
            return base.UseItem(item, player);
        }
        public override bool NeedsSaving(Item item)
        {
            return true;
        }
        public override TagCompound Save(Item item)
        {
            TagCompound tag = new TagCompound();
            tag.Add("IceShoot", IceShoot);
            return tag;
        }
        public override void Load(Item item, TagCompound tag)
        {
            IceShoot = tag.GetBool("IceShoot");
            base.Load(item, tag);
        }
        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if(item.modItem == null) return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
            if(IceShoot && ElementMachine.GetElementName(ElementMachine.GetElement(item.modItem.Name)) == "Ice") 
            {
                //if(IceSpear) Projectile.NewProjectile(player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 10, ModContent.ProjectileType<IceSpearProj>(), Math.Min(Math.Max(IceBaseDamage, (int)(item.damage * 0.3) + player.statDefense / 10 + IceBaseDamage), IceMaxDamage), 1, Main.myPlayer);
                Projectile.NewProjectile(player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 10, ProjectileID.IceBolt, Math.Min(Math.Max(IceBaseDamage, (int)(item.damage * 0.3) + player.statDefense / 10 + IceBaseDamage), IceMaxDamage), 1, Main.myPlayer);
            }
            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}