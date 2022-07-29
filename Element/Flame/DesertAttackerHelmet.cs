using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using ElementMachine.Bases;
using ElementMachine.Tiles;

namespace ElementMachine.Element.Flame
{
	[AutoloadEquip(EquipType.Head)]
	public class DesertAttackerHelmet : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DesertAttackerHelmet");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "荒漠突袭者角盔");
			Tooltip.SetDefault("As hard as Antlion\nincrease 1 defense");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "和蚁狮一样硬\n提高1点防御力");
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DesertAttackerArmor>() && legs.type == ModContent.ItemType<DesertAttackerCuisse>();
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "允许冲刺,撞到敌人时造成少量伤害\n提高10%钩镰伤害\n在沙漠时提高5%近战伤害";
            Main.LocalPlayer.GetModPlayer<EquipmentPlayer>().AntlionDash = true;
			Main.LocalPlayer.GetDamage(DamageClass.Melee) += 0.05f;
			Main.LocalPlayer.GetModPlayer<BasePlayer>().SickleDamagePer += 0.1f;
		}
		public override void UpdateEquip(Player player)
        {
            player.statDefense += 1;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 30;
			Item.value = 1000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 1;
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
