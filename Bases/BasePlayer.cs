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
        public override void ResetEffects()
        {
            base.ResetEffects();
            SickleDamagePer = 0f;
        }
    }
}