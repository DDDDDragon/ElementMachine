﻿using ElementMachine.Tiles;
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
			DisplayName.AddTranslation(GameCulture.Chinese, "机械红宝石法杖");
			Tooltip.SetDefault("it's just the mechanical edition of RubyStaff, just cooler and more awesome");
			Tooltip.AddTranslation(GameCulture.Chinese, "仿制红宝石法杖做出的机械化法杖, 更加强大");
		}
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.RubyStaff);
			item.damage = 27;
			item.mana = 10;
			item.knockBack = 0.25f;
			item.rare = ItemRarityID.Blue;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.ranged = true;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.width = 24;
			item.height = 24;
			item.scale = 0.85f;
			item.maxStack = 1;
			item.noMelee = true;
			item.shootSpeed = 6f;
			item.channel = true;
			item.autoReuse = false;
			item.useTime = 40;
			item.useAnimation = 40;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 10);
			recipe.AddIngredient(ItemID.Ruby, 5);
			recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 3);
			recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

	}
}
