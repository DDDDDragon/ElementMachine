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
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "蚁狮甲壳");
			Tooltip.SetDefault("There's no Antlion Carapace as a free lunch");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "蚁狮壳出在蚁狮身上");
		}
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 20;
            Item.maxStack = 999;
			Item.value = 10;
			Item.rare = ItemRarityID.Blue;
			Element = 1;
			ElementLevel = 0.7f;
		}
	}
    public class AntlionBones : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("AntlionBones");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "蚁狮骨");
			Tooltip.SetDefault("Why a insect has bones ????");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "为啥虫子会长大骨棒????");
		}
        public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 26;
			Item.value = 10;
			Item.rare = ItemRarityID.Orange;
			Element = 1;
			ElementLevel = 0.7f;
		}
	}
}
