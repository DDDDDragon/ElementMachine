using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;

namespace ElementMachine.Buffs
{
    public class IceIncreaseL1 : ModBuff 
    {
        public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("IceIncrease");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "冰霜");
            Description.SetDefault("Increase Ice-Element Weapon's damage");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "提高冰霜属性武器的伤害");
        }
    }
}
