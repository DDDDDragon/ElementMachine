using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ElementMachine.Buffs
{
    public class Dizziness : ModBuff 
    {
        public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("Dizziness");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "眩晕");
            Description.SetDefault("⭐");
        }
        public override void Update(Player player, ref int buffIndex) 
        {
            player.velocity.X = 0;
            player.immune = true;
            player.immuneNoBlink = true;
            player.controlJump = false;
            player.moveSpeed = 0;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity *= 0f;
            base.Update(npc, ref buffIndex);
        }
    }
}
