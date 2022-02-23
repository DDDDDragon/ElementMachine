using ElementMachine.Recipe;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ElementMachine.Tiles;

namespace ElementMachine.Machine
{
    [AutoloadEquip(EquipType.Head)]
    public class JuniorAlloyHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloyHelmet");
            DisplayName.AddTranslation(GameCulture.Chinese, "初级合金头盔");
            Tooltip.SetDefault("just ju-nior √\nincrease 10 max life");
            Tooltip.AddTranslation(GameCulture.Chinese, "确实初级\n增加10点最大生命值");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;
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
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 25);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 25);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
    [AutoloadEquip(EquipType.Body)]
    public class JuniorAlloyArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloyArmor");
            DisplayName.AddTranslation(GameCulture.Chinese, "初级合金战装");
            Tooltip.SetDefault("just ju-nior √\nincrease 10 max life");
            Tooltip.AddTranslation(GameCulture.Chinese, "确实初级\n增加10点最大生命值");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			item.width = 40;
			item.height = 24;
			item.value = 10;
			item.rare = ItemRarityID.Blue;
			item.defense = 3;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 30);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 30);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<JuniorAlloyHelmet>() && legs.type == ModContent.ItemType<JuniorAlloyCuisse>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "增加10%全部伤害, 增加1点防御";
            player.allDamage += 0.1f;
            player.statDefense += 1;
            base.UpdateArmorSet(player);
        }
    }
    [AutoloadEquip(EquipType.Legs)]
    public class JuniorAlloyCuisse : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloyCuisse");
            DisplayName.AddTranslation(GameCulture.Chinese, "初级合金腿甲");
            Tooltip.SetDefault("just ju-nior √\nincrease 10 max life");
            Tooltip.AddTranslation(GameCulture.Chinese, "确实初级\n增加10点最大生命值");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;
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
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 25);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 25);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}