using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementMachine.Element.Ice
{
    public class FrozenStone : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("FrozenStone");
			DisplayName.AddTranslation(GameCulture.Chinese, "霜寒石");
			Tooltip.SetDefault("it's so cold");
			Tooltip.AddTranslation(GameCulture.Chinese, "冰冷刺骨");
		}
        public override void SetDefaults()
		{
			item.width = 16;
			item.height = 18;
            item.maxStack = 999;
			item.value = 10;
			item.rare = ItemRarityID.Blue;
		}
    }
}