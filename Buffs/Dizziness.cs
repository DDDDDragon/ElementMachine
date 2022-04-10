using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;

namespace ElementMachine.Buffs
{
    public class Dizziness : ModBuff 
    {
        public override void SetDefaults() 
        {
            DisplayName.SetDefault("Dizziness");
            DisplayName.AddTranslation(GameCulture.Chinese, "眩晕");
            Description.SetDefault("⭐");
        }
        public override void Update(Player player, ref int buffIndex) 
        {
            player.moveSpeed = 0;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity *= 0f;
            base.Update(npc, ref buffIndex);
        }
    }
}
