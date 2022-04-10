using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;

namespace ElementMachine.Buffs
{
    public class ArmorBroken : ModBuff 
    {
        public override void SetDefaults() 
        {
            DisplayName.SetDefault("ArmorBroken");
            DisplayName.AddTranslation(GameCulture.Chinese, "破甲");
            Description.SetDefault("Lower enemy's defense");
        }
    }
}
