using ElementMachine.Recipe;
using Terraria.Localization;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using ElementMachine.Tiles;
using ElementMachine.Buffs;

namespace ElementMachine.Machine
{
    [AutoloadEquip(EquipType.Head)]
    public class JuniorAlloyHelmet : MachineItem
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
    public class JuniorAlloyArmor : MachineItem
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
            if(GameCulture.Chinese.IsActive) player.setBonus = "增加7%全部伤害, 增加1点防御\n使用合金武器攻击敌人时为玩家添加1层'液压引擎'buff\n每层为玩家提供3%全攻击力提升,至多5层,停止攻击后层数迅速减少";
            else player.setBonus = "increase 7% all damage\nwhen using alloy weapon, if you hit NPC, you will get 1 layer of HydraulicEngine buff\neach layer will increase 3% all damage, at most 5 layers, if stop hitting NPC, layer will decrease quickly";
            player.allDamage += 0.07f;
            player.statDefense += 1;
            player.GetModPlayer<MachinePlayer>().MachineArmorSet = true;
            base.UpdateArmorSet(player);
        }
    }
    [AutoloadEquip(EquipType.Legs)]
    public class JuniorAlloyCuisse : MachineItem
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
    public class JuniorAlloySword : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloySword");
            DisplayName.AddTranslation(GameCulture.Chinese, "初级合金制式长剑");
            Tooltip.SetDefault("just ju-nior √");
            Tooltip.AddTranslation(GameCulture.Chinese, "确实初级");
        }
        public override void SetDefaults()
        {
            item.channel = true;
            item.width = 54;
            item.height = 56;
            item.maxStack = 1;
            item.melee = true;
            item.rare = ItemRarityID.Blue;
            item.damage = 15;
            item.knockBack = 0.25f;
            item.useStyle = 1;
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.scale = 0.85f;
            item.autoReuse = true;
            item.useTime = 20;
            item.useAnimation = 20;
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
    public class JuniorAlloyPistol : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloySword");
            DisplayName.AddTranslation(GameCulture.Chinese, "初级合金制式手枪");
            Tooltip.SetDefault("just ju-nior √");
            Tooltip.AddTranslation(GameCulture.Chinese, "确实初级");
        }
        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 22;
            item.maxStack = 1;
            item.scale = 0.7f;
            item.ranged = true;
            item.rare = ItemRarityID.Blue;
            item.noMelee = true;
            item.damage = 15;
            item.knockBack = 0.10f;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 0, 10, 0);
            item.autoReuse = true;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 7f;
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