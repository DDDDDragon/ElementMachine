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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "初级合金头盔");
            Tooltip.SetDefault("just ju-nior √\nincrease 10 max life");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "确实初级\n增加10点最大生命值");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 24;
			Item.value = 10;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 2;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 25);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 25);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
    }
    [AutoloadEquip(EquipType.Body)]
    public class JuniorAlloyArmor : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloyArmor");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "初级合金战装");
            Tooltip.SetDefault("just ju-nior √\nincrease 10 max life");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "确实初级\n增加10点最大生命值");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 24;
			Item.value = 10;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 3;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 30);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 30);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<JuniorAlloyHelmet>() && legs.type == ModContent.ItemType<JuniorAlloyCuisse>();
        }
        public override void UpdateArmorSet(Player player)
        {
            if(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive) player.setBonus = "增加7%全部伤害, 增加1点防御\n使用机械武器攻击敌人时为玩家添加1层'液压引擎'buff\n每层为玩家提供3%全攻击力提升,至多5层,停止攻击后层数迅速减少";
            else player.setBonus = "increase 7% all damage\nwhen using Machine weapon, if you hit NPC, you will get 1 layer of HydraulicEngine buff\neach layer will increase 3% all damage, at most 5 layers, if stop hitting NPC, layer will decrease quickly";
            player.GetDamage(DamageClass.Generic) += 0.07f;
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "初级合金腿甲");
            Tooltip.SetDefault("just ju-nior √\nincrease 10 max life");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "确实初级\n增加10点最大生命值");
        }
        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 24;
			Item.value = 10;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 2;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 20);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 20);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
    }
    public class JuniorAlloySword : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloySword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "初级合金制式长剑");
            Tooltip.SetDefault("just ju-nior √");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "确实初级");
        }
        public override void SetDefaults()
        {
            Item.channel = true;
            Item.width = 54;
            Item.height = 56;
            Item.maxStack = 1;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Blue;
            Item.damage = 15;
            Item.knockBack = 3.75f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.scale = 0.85f;
            Item.autoReuse = true;
            Item.useTime = 20;
            Item.useAnimation = 20;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 25);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 25);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
    }
    public class JuniorAlloyPistol : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloySword");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "初级合金制式手枪");
            Tooltip.SetDefault("just ju-nior √");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "确实初级");
        }
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 22;
            Item.maxStack = 1;
            Item.scale = 0.7f;
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ItemRarityID.Blue;
            Item.noMelee = true;
            Item.damage = 11;
            Item.knockBack = 0.10f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.autoReuse = true;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 7f;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 25);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 25);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
    }
    public class JuniorAlloyPickaxe : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloyPickaxe");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "初级合金镐");
            Tooltip.SetDefault("just ju-nior √");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "确实初级");
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 1;
            Item.scale = 0.7f;
            Item.rare = ItemRarityID.Blue;
            Item.damage = 11;
            Item.knockBack = 0.10f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.pick = 45;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.autoReuse = true;
            Item.useTime = 25;
            Item.useAnimation = 25;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 15);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 5);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
    }
    public class JuniorAlloyAxe : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloyAxe");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "初级合金斧");
            Tooltip.SetDefault("just ju-nior √");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "确实初级");
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 1;
            Item.scale = 0.7f;
            Item.rare = ItemRarityID.Blue;
            Item.damage = 11;
            Item.knockBack = 0.10f;
            Item.useStyle = 1;
            Item.axe = 13;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.autoReuse = true;
            Item.useTime = 25;
            Item.useAnimation = 25;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 15);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 5);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
    }
    public class JuniorAlloyHammer : MachineItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("JuniorAlloyHammer");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "初级合金锤");
            Tooltip.SetDefault("just ju-nior √");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "确实初级");
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 1;
            Item.scale = 0.7f;
            Item.rare = ItemRarityID.Blue;
            Item.damage = 11;
            Item.knockBack = 0.10f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.hammer = 50;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.autoReuse = true;
            Item.useTime = 25;
            Item.useAnimation = 25;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<JuniorAlloy>(), 15);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 5);
            recipe.AddTile(ModContent.TileType<AlloyWorkBench>());
			
			recipe.Register();
		}
    }
}