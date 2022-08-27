using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementMachine.Element.Ice.Frozen
{
    public class FrozenStoneCoin : ElementItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("FrozenStoneCoin");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "霜寒石币");
			Tooltip.SetDefault("seem like it can be used to change the element of altar\nhold this item and right click the Altar to open Ice Altar");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "看起来它可以改变祭坛的元素\n手持此物品右键祭坛激活冰属性祭坛");
		}
        public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 28;
            Item.maxStack = 1;
			Item.value = 100;
			Item.rare = ItemRarityID.Green;
			Element = 2;
			ElementLevel = 0.7f;
		}
    }
}