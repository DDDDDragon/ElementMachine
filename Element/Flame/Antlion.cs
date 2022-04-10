using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
namespace ElementMachine.Element.Flame
{
	public class AntlionCarapace : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("AntlionCarapace");
			DisplayName.AddTranslation(GameCulture.Chinese, "蚁狮甲壳");
			Tooltip.SetDefault("There's no Antlion Carapace as a free lunch");
			Tooltip.AddTranslation(GameCulture.Chinese, "蚁狮壳出在蚁狮身上");
		}
        public override void SetDefaults()
		{
			item.width = 18;
			item.height = 20;
            item.maxStack = 999;
			item.value = 10;
			item.rare = ItemRarityID.Blue;
		}
	}
    public class AntlionBones : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("AntlionBones");
			DisplayName.AddTranslation(GameCulture.Chinese, "蚁狮骨");
			Tooltip.SetDefault("Why a insect has bones ????");
			Tooltip.AddTranslation(GameCulture.Chinese, "为啥虫子会长大骨棒????");
		}
        public override void SetDefaults()
		{
			item.width = 34;
			item.height = 26;
			item.value = 10;
			item.rare = ItemRarityID.Orange;
		}
	}
}
