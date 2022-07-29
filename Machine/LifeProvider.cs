using Terraria;
using ElementMachine.Tiles;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;


namespace ElementMachine.Machine
{
	public class LifeProvider : MachineItem
	{
		public override void SetDefaults()
		{
			Item.maxStack = 1;
			Item.rare = 5;
			Item.height = 32;
			Item.width = 28;
			Item.accessory = true;
			Item.value = 2000;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
			if(player.lifeRegen < 0)
			{ 
				player.lifeRegen = 0;
			}
			player.lifeRegen = 2;
			player.statLifeMax2 += 20;
        }
        public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("LifeProvider");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "生命供应器");
			Tooltip.SetDefault("increase 20 max life and a little life regen");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "科技无所不能 \n增加20点最大生命, 略微增加生命恢复速度");
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<LifeCore>(), 3);
			recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 5);
			recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 10);
			recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
	}
}