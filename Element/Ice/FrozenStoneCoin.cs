using Terraria;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementMachine.Element.Ice
{
    public class FrozenStoneCoin : ElementItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("FrozenStoneCoin");
			DisplayName.AddTranslation(GameCulture.Chinese, "霜寒石币");
			Tooltip.SetDefault("seem like it can be used to change the element of altar\nhold this item and right click the Altar to open Ice Altar");
			Tooltip.AddTranslation(GameCulture.Chinese, "看起来它可以改变祭坛的元素\n手持此物品右键祭坛激活冰属性祭坛");
		}
        public override void SetDefaults()
		{
            
			AddElement(ElementsType.Ice);
			item.width = 38;
			item.height = 28;
            item.maxStack = 1;
			item.value = 100;
			item.rare = ItemRarityID.Green;
		}
    }
}