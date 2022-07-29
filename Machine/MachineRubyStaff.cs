using ElementMachine.Tiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Localization;
namespace ElementMachine.Machine
{
	public class MachineRubyStaff : MachineItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("MachineRubyStaff");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "机械红宝石法杖");
			Tooltip.SetDefault("it's just the mechanical edition of RubyStaff, just cooler and more awesome");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "仿制红宝石法杖做出的机械化法杖, 更加强大");
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.RubyStaff);
			Item.damage = 27;
			Item.mana = 10;
			Item.knockBack = 0.25f;
			Item.rare = ItemRarityID.Blue;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.DamageType = DamageClass.Ranged;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.width = 24;
			Item.height = 24;
			Item.scale = 0.85f;
			Item.maxStack = 1;
			Item.noMelee = true;
			Item.shootSpeed = 6f;
			Item.channel = true;
			Item.autoReuse = false;
			Item.useTime = 40;
			Item.useAnimation = 40;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 10);
			recipe.AddIngredient(ItemID.Ruby, 5);
			recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 3);
			recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}

	}
}
