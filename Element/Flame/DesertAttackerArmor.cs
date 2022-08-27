using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using ElementMachine.Tiles;

namespace ElementMachine.Element.Flame
{
	[AutoloadEquip(EquipType.Body)]
	public class DesertAttackerArmor : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DesertAttackerArmor");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "荒漠突袭者护甲");
			Tooltip.SetDefault("As hard as Antlion\nincrease 5% melee damage");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "和蚁狮一样硬\n提高5%近战伤害");
		}
		public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.05f;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 24;
			Item.value = 1000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 2;
			Element = 1;
			ElementLevel = 0.7f;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AntlionCarapace>(), 20);
			recipe.AddIngredient(ItemID.SandBlock, 20);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
	}
}
