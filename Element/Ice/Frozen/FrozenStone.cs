using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementMachine.Element.Ice.Frozen
{
    public class FrozenStone : ElementItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("FrozenStone");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "霜寒石");
			Tooltip.SetDefault("it's so cold");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "冰冷刺骨");
		}
        public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 18;
            Item.maxStack = 999;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Element = 2;
			ElementLevel = 0.7f;
		}
    }
}