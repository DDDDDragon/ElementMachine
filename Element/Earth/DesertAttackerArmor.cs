using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using ElementMachine.Tiles;

namespace ElementMachine.Element.Earth
{
	[AutoloadEquip(EquipType.Body)]
	public class DesertAttackerArmor : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DesertAttackerArmor");
			DisplayName.AddTranslation(GameCulture.Chinese, "荒漠突袭者护甲");
			Tooltip.SetDefault("As hard as Antlion\nincrease 5% melee damage");
			Tooltip.AddTranslation(GameCulture.Chinese, "和蚁狮一样硬\n提高5%近战伤害");
		}
		public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.05f;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			item.width = 40;
			item.height = 24;
			item.value = 10;
			item.rare = ItemRarityID.Blue;
			item.defense = 2;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<AntlionCarapace>(), 20);
			recipe.AddIngredient(ItemID.SandBlock, 20);
            recipe.AddTile(ModContent.TileType<ElementHoroScpoer>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
