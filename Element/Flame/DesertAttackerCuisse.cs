using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using ElementMachine.Tiles;

namespace ElementMachine.Element.Flame
{
	[AutoloadEquip(EquipType.Legs)]
	public class DesertAttackerCuisse : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DesertAttackerCuisse");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "荒漠突袭者腿甲");
			Tooltip.SetDefault("As hard as Antlion\nincrease move speed");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "和蚁狮一样硬\n提高移速");
		}
        public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 1000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 2;
		}		
		public override void UpdateEquip(Player player)
        {
            player.maxRunSpeed *= 1.1f;
            base.UpdateEquip(player);
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AntlionCarapace>(), 15);
            recipe.AddIngredient(ItemID.SandBlock, 15);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
	}
}
