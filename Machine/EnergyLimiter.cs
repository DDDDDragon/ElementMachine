using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using ElementMachine.Tiles;

namespace ElementMachine.Machine
{
	public class EnergyLimiter : ModItem
	{
		public override void SetDefaults()
		{
		    item.maxStack = 1;
	        item.rare = ItemRarityID.Blue;
		    item.height = 32;
            item.width = 28;
		    item.accessory = true;
			item.value = 2000;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.meleeDamage += 0.1f;
			player.lifeRegen += 1;
			if(player.statManaMax2 >= 40) player.statManaMax2 -= 40;
			else player.statManaMax2 = 0;
			player.magicDamage -= 0.05f;
			player.minionDamage -= 0.05f;
		}
        public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("EnergyLimiter");
			DisplayName.AddTranslation(GameCulture.Chinese, "能量限制器");
			Tooltip.SetDefault("decrease 40 max mana, 2 defense and 5% magic/minion damage, but increase 10% melee damage and 1 life regen");
		    Tooltip.AddTranslation(GameCulture.Chinese, "减少你40点最大魔力, 2点防御与5%的魔法和召唤伤害, 但可以增加你15%的近战伤害与1点回血速度");
	    }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<EnergyCore>(), 5);
			recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 5);
			recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 10);
			recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}