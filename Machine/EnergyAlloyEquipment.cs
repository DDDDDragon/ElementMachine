using Terraria.Localization;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ElementMachine.Tiles;

namespace ElementMachine.Machine
{
    [AutoloadEquip(EquipType.Head)]
    public class EnergyAlloyHelmet : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EnergyAlloyHelmet");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "能量合金头盔");
            Tooltip.SetDefault("it can even emit Green light! fucking cool!\nincrease 15 max life\nthis suit's code is not complete");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "这不高级多了,还tm会发蓝光!\n增加15点最大生命值\n这套套装还没有开发完毕");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 15;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 24;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.defense = 5;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnergyAlloy>(), 25);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 25);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
    }
    [AutoloadEquip(EquipType.Body)]
    public class EnergyAlloyArmor : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EnergyAlloyArmor");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "能量合金战装");
            Tooltip.SetDefault("it can even emit Green light! fucking cool!\nincrease 15 max life\nthis suit's code is not complete");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "这不高级多了,还tm会发蓝光!\n增加15点最大生命值\n这套套装还没有开发完毕");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 15;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 24;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.defense = 6;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnergyAlloy>(), 30);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 30);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<EnergyAlloyHelmet>() && legs.type == ModContent.ItemType<EnergyAlloyCuisse>();
        }
        public override void UpdateArmorSet(Player player)
        {
            if(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive) player.setBonus = "这套套装还没有开发完毕";
            else player.setBonus = "this suit's code is not complete";
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.statDefense += 1;
            base.UpdateArmorSet(player);
        }
    }
    [AutoloadEquip(EquipType.Legs)]
    public class EnergyAlloyCuisse : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EnergyAlloyCuisse");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "能量合金腿甲");
            Tooltip.SetDefault("it can even emit Green light! fucking cool!\nincrease 15 max life\nthis suit's code is not complete");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "这不高级多了,还tm会发蓝光!\n增加15点最大生命值\n这套套装还没有开发完毕");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 15;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 24;
			Item.value = 10;
			Item.rare = ItemRarityID.Green;
			Item.defense = 5;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EnergyAlloy>(), 25);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 25);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
    }
}