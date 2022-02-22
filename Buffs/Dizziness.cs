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
        }
        public override void Update(Player player, ref int buffIndex) 
        {
            player.buffTime[buffIndex] = 0;
            player.moveSpeed = 0;
        }
    }
}
