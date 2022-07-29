using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Input;
namespace ElementMachine.Bases
{
    public class BasePlayer : ModPlayer
    {
        public int SicklePoint = 0;
        public float SickleDamagePer = 0f;
        public float ShieldSpearDefensePer = 1.1f;
        public int ShieldSpearExtraDefense = 1;
        public override void ResetEffects()
        {
            base.ResetEffects();
            SickleDamagePer = 0f;
            ShieldSpearDefensePer = 1.1f;
            ShieldSpearExtraDefense = 1;
        }
        public override void PostUpdateEquips()
        {
            base.PostUpdateEquips();
            foreach(var i in Main.projectile)
            {
                if(i.ModProjectile is BaseShieldSpearProjS && i.active) Player.statDefense = (int)(Player.statDefense * ShieldSpearDefensePer) + ShieldSpearExtraDefense;
            }
            
        }
    }
}