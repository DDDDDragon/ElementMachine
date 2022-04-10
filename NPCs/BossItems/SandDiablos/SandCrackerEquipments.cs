using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using ElementMachine.Tiles;
using ElementMachine.Bases;
using ElementMachine.Element;
using ElementMachine.Buffs;
using ElementMachine.NPCs.Boss.SandDiablos;
using ElementMachine.Element.Flame;
using System;
using Microsoft.Xna.Framework;

namespace ElementMachine.NPCs.BossItems.SandDiablos
{
	public class SandCrackerBow : ElementItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerBow");
			DisplayName.AddTranslation(GameCulture.Chinese, "荒砂角弓");
			Tooltip.SetDefault("For Wildspire Waste!");
			Tooltip.AddTranslation(GameCulture.Chinese, "为了大蚁冢荒地!\n消耗箭矢发射荒砂之矢,荒砂之矢在与方块接触时有几率释放沙尘暴");
		}
		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 72;
			item.ranged = true;
			item.damage = 14;
			item.useAnimation = 20;
			item.useTime = 20;
            item.maxStack = 1;
			item.value = 10000;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.rare = ItemRarityID.Green;
			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.useAmmo = AmmoID.Arrow;
			item.noMelee = true;
			item.shootSpeed = 7f;
			item.scale = 0.7f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<SandCrackerArrow>(), 12, 1f);
			return false;
        }
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, -5);
		}
	}
	public class SandCrackerArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerArrow");
			DisplayName.AddTranslation(GameCulture.Chinese, "荒砂之矢");
		}
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
			projectile.damage = 12;
			projectile.friendly = true;
			aiType = ProjectileID.WoodenArrowFriendly;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if(Main.rand.Next(0, 11) == 1)
			{
				Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.SandnadoFriendly, 5, 1f);
			}
		}
	}
	public class SandHorn : ElementItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandHorn");
			DisplayName.AddTranslation(GameCulture.Chinese, "荒砂号角");
			Tooltip.SetDefault("Wind, Sand and the world's end\nSummon Sand Diablos");
			Tooltip.AddTranslation(GameCulture.Chinese, "风,砂与世界的终结\n召唤砂角魔灵");
		}
        public override void SetDefaults()
		{
			item.width = 18;
			item.height = 20;
			item.useAnimation = 30;
			item.useTime = 30;
            item.maxStack = 30;
			item.value = 3500;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.rare = ItemRarityID.Green;
			item.consumable = true;
			item.useTurn = true;
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Boss.SandDiablos.SandDiablos>());
			Main.PlaySound(SoundID.Roar);
			return true;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SandBlock, 15);
			recipe.AddIngredient(ItemID.AntlionMandible, 3);
			recipe.AddIngredient(ItemID.Cactus, 20);
			recipe.AddIngredient(ModContent.ItemType<AntlionCarapace>(), 5);
			recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
    public class SandCrackerChainSickle : BaseSickleItem
    {
        public override void SetSickle()
        {
            damage = 15;
            defaultName = "SandCrackerShackles";
            transName = "荒砂束缚";
            Effect = "减速被攻击的敌人并有概率破甲\n";
			press = false;
            shootType = ModContent.ProjectileType<SandCrackerChainSickleProj>();
            shootSpeed = 18.5f;
            item.rare = ItemRarityID.Blue;
            base.SetSickle();
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<SandDiablosHorn>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 5);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
    public class SandCrackerChainSickleProj : BaseSickle
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SCCSP");
		}
		public override void SetDefaults()
		{
			projectile.width = 46;
			projectile.height = 28;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
            projectile.knockBack = 0;
		}
        public override void SetSprite(string a)
        {
            base.SetSprite("ElementMachine/NPCs/BossItems/SandDiablos/SandCrackerChainSickleChain");
        }
        public override void Effect()
        {
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(ModContent.BuffType<lowerSpeed>(), 60);
			if(Main.rand.Next(0, 4) == 0) target.AddBuff(ModContent.BuffType<ArmorBroken>(), 60);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
	public class SandCrackerShieldSpear : BaseShieldSpear
	{
		public override void SetSpear()
        {
			damage = 16;
            defaultName = "SandCrackerCrusher";
            transName = "荒砂碾碎机";
            Effect = "击中敌人有几率造成破甲效果";
			EffectEn = "probably make enemys' armor penetrated when hitting them";
			tooltip = "为了大蚁冢荒地!";
			tooltipEn = "For Wildspire Waste!";
            SpearProjType = ModContent.ProjectileType<SandCrackerShieldSpearProjS>();
			ThrownProjType = ModContent.ProjectileType<SandCrackerShieldSpearProjT>();
            shootSpeed = 10f;
            base.SetSpear();
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
    public class SandCrackerShieldSpearProjT : BaseShieldSpearProjT
    {
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if(Main.rand.Next(0, 4) == 0) target.AddBuff(ModContent.BuffType<ArmorBroken>(), 60);
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }

	public class SandCrackerShieldSpearProjS : BaseShieldSpearProjS
	{
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if(Main.rand.Next(0, 4) == 0) target.AddBuff(ModContent.BuffType<ArmorBroken>(), 60);
			base.OnHitNPC(target, damage, knockback, crit);
		}
	}
    public class SandDiablosHorn : ElementItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandDiablosHorn");
			DisplayName.AddTranslation(GameCulture.Chinese, "荒砂之角");
			Tooltip.SetDefault("For Wildspire Waste!");
			Tooltip.AddTranslation(GameCulture.Chinese, "为了大蚁冢荒地!");
		}
        public override void SetDefaults()
		{
			item.width = 18;
			item.height = 20;
            item.maxStack = 999;
			item.value = 3500;
			item.rare = ItemRarityID.Green;
		}
    }
    public class SandDiablosCarapace : ElementItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandDiablosCarapace");
			DisplayName.AddTranslation(GameCulture.Chinese, "荒砂甲壳");
			Tooltip.SetDefault("For Wildspire Waste!");
			Tooltip.AddTranslation(GameCulture.Chinese, "为了大蚁冢荒地!");
		}
        public override void SetDefaults()
		{
			item.width = 18;
			item.height = 20;
            item.maxStack = 999;
			item.value = 100;
			item.rare = ItemRarityID.Blue;
		}
    }
	[AutoloadEquip(EquipType.Head)]
	public class SandCrackerHeadgear : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerHeadgear");
			DisplayName.AddTranslation(GameCulture.Chinese, "砂角崩裂者头饰");
			Tooltip.SetDefault("It's like a sound says that you can kill 100 Barroths, but what's Battoth?\nincrease 5% minion damage");
			Tooltip.AddTranslation(GameCulture.Chinese, "好像有一个声音在说你能杀100只土砂龙,但是啥是土砂龙?\n提高5%召唤伤害");
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SandCrackerArmor>() && legs.type == ModContent.ItemType<SandCrackerCuisse>();
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "提高5%召唤伤害\n提高15%盾矛防御加成和2点盾矛额外防御";
            player.minionDamage += 0.05f;
            EquipmentPlayer.SandCracker = true;
            player.GetModPlayer<BasePlayer>().ShieldSpearDefensePer += 0.15f;
            player.GetModPlayer<BasePlayer>().ShieldSpearExtraDefense += 2;
		}
		public override void UpdateEquip(Player player)
        {
            player.statDefense += 1;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 30;
			item.value = 1000;
			item.rare = ItemRarityID.Green;
			item.defense = 4;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 3);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
    [AutoloadEquip(EquipType.Head)]
	public class SandCrackerHelmet : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerHelmet");
			DisplayName.AddTranslation(GameCulture.Chinese, "砂角崩裂者角盔");
			Tooltip.SetDefault("It's like a sound says that you can kill 100 Barroths, but what's Battoth?\nincrease 5% melee damage");
			Tooltip.AddTranslation(GameCulture.Chinese, "好像有一个声音在说你能杀100只土砂龙,但是啥是土砂龙?\n提高5%近战伤害");
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SandCrackerArmor>() && legs.type == ModContent.ItemType<SandCrackerCuisse>();
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "提高5%近战伤害\n提高2点防御和2点生命恢复";
			player.lifeRegen += 2;
            player.meleeDamage += 0.05f;
			player.statDefense += 2;
            EquipmentPlayer.SandCracker = true;
			//player.GetModPlayer<BasePlayer>().SickleDamagePer += 0.1f;
		}
		public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.05f;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			item.width = 26;
			item.height = 30;
			item.value = 1000;
			item.rare = ItemRarityID.Green;
			item.defense = 4;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 3);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
	[AutoloadEquip(EquipType.Body)]
	public class SandCrackerArmor : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerArmor");
			DisplayName.AddTranslation(GameCulture.Chinese, "砂角崩裂者护甲");
			Tooltip.SetDefault("It's like a sound says that you can kill 100 Barroths, but what's Battoth?\nincrease 8% endurance and 8% crit");
			Tooltip.AddTranslation(GameCulture.Chinese, "好像有一个声音在说你能杀100只土砂龙,但是啥是土砂龙?\n提高5%伤害减免和5%暴击率");
		}
		public override void UpdateEquip(Player player)
        {
            player.endurance += 0.08f;
            player.magicCrit += 8;
            player.meleeCrit += 8;
            player.rangedCrit += 8;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			item.width = 40;
			item.height = 24;
			item.value = 1000;
			item.rare = ItemRarityID.Green;
			item.defense = 5;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SandBlock, 20);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
    [AutoloadEquip(EquipType.Legs)]
	public class SandCrackerCuisse : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerCuisse");
			DisplayName.AddTranslation(GameCulture.Chinese, "砂角崩裂者腿甲");
			Tooltip.SetDefault("It's like a sound says that you can kill 100 Barroths, but what's Battoth?\nincrease 10% move speed");
			Tooltip.AddTranslation(GameCulture.Chinese, "好像有一个声音在说你能杀100只土砂龙,但是啥是土砂龙?\n提高10%移动速度");
		}
        public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 1000;
			item.rare = ItemRarityID.Green;
			item.defense = 2;
		}		
		public override void UpdateEquip(Player player)
        {
            player.maxRunSpeed += 0.1f;
            base.UpdateEquip(player);
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SandBlock, 15);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
