using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Tiles;

namespace ElementMachine.Machine
{
	public class EnergyProvider : MachineItem
	{
		public override void SetDefaults()
		{
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Blue;
			Item.height = 32;
			Item.width = 32;
			Item.accessory = true;
			Item.defense = 1;
			Item.value = 2000;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statManaMax2 += 20;
			player.GetDamage(DamageClass.Magic) += 0.05f;
			player.GetDamage(DamageClass.Summon) += 0.05f;
		}
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("EnergyProvider");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "能量供应器");
			Tooltip.SetDefault("increase 20 max mana and 5% magic/minion damage and 1 defense");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "安装了能量核心的转化器\n增加你20点最大魔力与5%的魔法和召唤伤害以及1点防御");
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<EnergyCore>(), 3);
			recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 5);
			recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 10);
			recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
	}
}