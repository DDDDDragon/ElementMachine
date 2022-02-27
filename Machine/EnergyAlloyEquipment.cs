using ElementMachine.Recipe;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ElementMachine.Tiles;

namespace ElementMachine.Machine
{
    [AutoloadEquip(EquipType.Head)]
    public class EnergyAlloyHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EnergyAlloyHelmet");
            DisplayName.AddTranslation(GameCulture.Chinese, "能量合金头盔");
            Tooltip.SetDefault("it can even emit Green light! fucking cool!\nincrease 15 max life\nthis suit's code is not complete");
            Tooltip.AddTranslation(GameCulture.Chinese, "这不高级多了,还tm会发蓝光!\n增加15点最大生命值\n这套套装还没有开发完毕");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 15;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			item.width = 40;
			item.height = 24;
			item.value = 10;
			item.rare = ItemRarityID.Green;
			item.defense = 5;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<EnergyAlloy>(), 25);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 25);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
    [AutoloadEquip(EquipType.Body)]
    public class EnergyAlloyArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EnergyAlloyArmor");
            DisplayName.AddTranslation(GameCulture.Chinese, "能量合金战装");
            Tooltip.SetDefault("it can even emit Green light! fucking cool!\nincrease 15 max life\nthis suit's code is not complete");
            Tooltip.AddTranslation(GameCulture.Chinese, "这不高级多了,还tm会发蓝光!\n增加15点最大生命值\n这套套装还没有开发完毕");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 15;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			item.width = 40;
			item.height = 24;
			item.value = 10;
			item.rare = ItemRarityID.Green;
			item.defense = 6;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<EnergyAlloy>(), 30);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 30);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<EnergyAlloyHelmet>() && legs.type == ModContent.ItemType<EnergyAlloyCuisse>();
        }
        public override void UpdateArmorSet(Player player)
        {
            if(GameCulture.Chinese.IsActive) player.setBonus = "这套套装还没有开发完毕";
            else player.setBonus = "this suit's code is not complete";
            player.allDamage += 0.08f;
            player.statDefense += 1;
            base.UpdateArmorSet(player);
        }
    }
    [AutoloadEquip(EquipType.Legs)]
    public class EnergyAlloyCuisse : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EnergyAlloyCuisse");
            DisplayName.AddTranslation(GameCulture.Chinese, "能量合金腿甲");
            Tooltip.SetDefault("it can even emit Green light! fucking cool!\nincrease 15 max life\nthis suit's code is not complete");
            Tooltip.AddTranslation(GameCulture.Chinese, "这不高级多了,还tm会发蓝光!\n增加15点最大生命值\n这套套装还没有开发完毕");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 15;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			item.width = 40;
			item.height = 24;
			item.value = 10;
			item.rare = ItemRarityID.Green;
			item.defense = 5;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<EnergyAlloy>(), 25);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 25);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}