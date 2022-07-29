using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Graphics;
using Terraria.ModLoader;
using ElementMachine.UI;
namespace ElementMachine.Machine
{
    public class MachinePlayer : ModPlayer
    {
        public float EnergyMax, EnergyMax2, EnergyNow;
        public bool EnergyBar = false;
        public bool MachineArmorSet = false;
        public override void ResetEffects()
        {
            base.ResetEffects();
            MachineArmorSet = false;
        }
        //public BarBase EnergyBar = new BarBase(() => EnergyNow, () => EnergyMax, ModContent.Request<Texture2D>())
    }
}