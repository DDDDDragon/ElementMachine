using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.IO;

namespace ElementMachine
{
    public class MyWorld : ModSystem
    {
        public override void PostWorldGen()
        {
            base.PostWorldGen();
        }
        public static bool SandDiablos = false;
        public override void SaveWorldData(TagCompound tag)
        {
            tag.Add("SandDiablos", SandDiablos);
            base.SaveWorldData(tag);
        }
        public override void LoadWorldData(TagCompound tag)
        {
            SandDiablos = tag.GetBool("SandDiablos");
            base.LoadWorldData(tag);
        }
        
    }
}