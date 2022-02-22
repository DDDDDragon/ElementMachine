using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace ElementMachine.Element.Earth
{
	[AutoloadEquip(EquipType.Legs)]
	public class DesertAttackerCuisse : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DesertAttackerCuisse");
			DisplayName.AddTranslation(GameCulture.Chinese, "荒漠突袭者腿甲");
			Tooltip.SetDefault("As hard as Antlion\nincrease move speed");
			Tooltip.AddTranslation(GameCulture.Chinese, "和蚁狮一样硬\n提高移速");
		}
        public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 10;
			item.rare = ItemRarityID.Blue;
			item.defense = 2;
		}		
		public override void UpdateEquip(Player player)
        {
            player.maxRunSpeed *= 1.1f;
            base.UpdateEquip(player);
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<AntlionCarapace>(), 15);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
