using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.IO;

namespace ElementMachine
{
    public class MyWorld : ModWorld
    {
        public bool SandDiablos = false;
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            tag.Add("SandDiablos", SandDiablos);
            return tag;
        }
        public override void Load(TagCompound tag)
        {
            SandDiablos = tag.GetBool("SandDiablos");
            base.Load(tag);
        }
    }
}