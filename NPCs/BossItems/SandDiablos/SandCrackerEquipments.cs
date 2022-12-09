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
using Terraria.Audio;
using Terraria.DataStructures;

namespace ElementMachine.NPCs.BossItems.SandDiablos
{
	public class SandDiablosBody_bone : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandDiablosChest");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "荒砂的胸壳");
			Tooltip.SetDefault("Sand Diablos material. Obtained by breaking its chest. Heavy, used to craft armor.");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵的素材。容易通过破坏砂角魔灵的胸壳部位获得。材质沉重，常用于防具上。");
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 22;
			Item.maxStack = 99;
			Item.value = 3500;
			Item.rare = ItemRarityID.Blue;
			Element = 3;
			ElementLevel = 1.3f;
		}
	}
	public class SandCrackerBow : ElementItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerBow");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "荒砂角弓");
			Tooltip.SetDefault("For Wildspire Waste! Consume Arrow to shoot SandArrow, it will release a Sandnado when collide block or hit enemy");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "为了大蚁冢荒地!\n消耗箭矢发射荒砂之矢,荒砂之矢在与方块接触或命中敌人时有几率释放沙尘暴");
		}
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 72;
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 14;
			Item.useAnimation = 20;
			Item.useTime = 20;
            Item.maxStack = 1;
			Item.value = 10000;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.rare = ItemRarityID.Green;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.useAmmo = AmmoID.Arrow;
			Item.noMelee = true;
			Item.shootSpeed = 7f;
			Item.scale = 0.7f;
			Element = 3;
			ElementLevel = 1.3f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SandCrackerArrow>(), 12, 1f, player.whoAmI);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, -5);
		}
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
	public class SandCrackerArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerArrow");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "荒砂之矢");
		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
			Projectile.damage = 12;
			Projectile.friendly = true;
			AIType = ProjectileID.WoodenArrowFriendly;
		}
		public override void AI()
        {
			Dust.NewDust(Projectile.Center, 1, 1, MyDustId.YellowGoldenFire, Projectile.velocity.X / 10, Projectile.velocity.Y / 10);
			Dust.NewDust(Projectile.Center, 1, 1, MyDustId.YellowFx1, Projectile.velocity.X / 10, Projectile.velocity.Y / 10);
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if(Main.rand.Next(0, 6) == 1)
			{
				Projectile.NewProjectile(null, Projectile.Center, Vector2.Zero, ProjectileID.SandnadoFriendly, 5, 1f, Projectile.owner);
			}
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			if (Main.rand.Next(0, 6) == 1)
			{
				Projectile.NewProjectile(null, Projectile.Center, Vector2.Zero, ProjectileID.SandnadoFriendly, 5, 1f, Projectile.owner);
			}
			return base.OnTileCollide(oldVelocity);
        }
    }
	public class SandHorn : ElementItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandHorn");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "荒砂号角");
			Tooltip.SetDefault("Wind, Sand and the world's end\nSummon Sand Diablos");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "风,砂与世界的终结\n召唤砂角魔灵");
		}
        public override void SetDefaults()
		{
			Element = 3;
			ElementLevel = 1f;
			Item.width = 18;
			Item.height = 20;
			Item.useAnimation = 30;
			Item.useTime = 30;
            Item.maxStack = 30;
			Item.value = 3500;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.rare = ItemRarityID.Green;
			Item.consumable = true;
			Item.useTurn = true;
			Element = 3;
			ElementLevel = 1.3f;
		}
		public override bool? UseItem(Player player)
		{
			Main.NewText(ElementLevel);
			if(player.ZoneDesert)
			{
				NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Boss.SandDiablos.SandDiablos>());
				SoundEngine.PlaySound(SoundID.Roar);
				return true;
			}
			return false;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SandBlock, 15);
			recipe.AddIngredient(ItemID.AntlionMandible, 3);
			recipe.AddIngredient(ItemID.Cactus, 20);
			recipe.AddIngredient(ModContent.ItemType<AntlionCarapace>(), 5);
			recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
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
            Item.rare = ItemRarityID.Blue;
			Element = 3;
			ElementLevel = 1.3f;
			base.SetSickle();
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<SandDiablosHorn>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 5);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
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
			Projectile.width = 46;
			Projectile.height = 28;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
            Projectile.knockBack = 0;
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
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<SandDiablosHorn>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 5);
			recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
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
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "荒砂之角");
			Tooltip.SetDefault("For Wildspire Waste!");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "为了大蚁冢荒地!");
		}
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 20;
            Item.maxStack = 999;
			Item.value = 3500;
			Item.rare = ItemRarityID.Green;
			Element = 3;
			ElementLevel = 1.2f;
		}
    }
    public class SandDiablosCarapace : ElementItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandDiablosCarapace");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "荒砂甲壳");
			Tooltip.SetDefault("For Wildspire Waste!");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "为了大蚁冢荒地!");
		}
        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 20;
            Item.maxStack = 999;
			Item.value = 100;
			Item.rare = ItemRarityID.Blue;
			Element = 3;
			ElementLevel = 1.2f;
        }
    }
	[AutoloadEquip(EquipType.Head)]
	public class SandCrackerVisage : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerVisage");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角崩裂者圣面");
			Tooltip.SetDefault("It's like a sound says that you can kill 100 Barroths, but what's Battoth?\nincrease 5% minion damage");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "好像有一个声音在说你能杀100只土砂龙,但是啥是土砂龙?\n提高5%魔法伤害");
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SandCrackerArmor>() && legs.type == ModContent.ItemType<SandCrackerCuisse>();
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "提高5%魔法伤害和暴击率\n降低10%魔力消耗" :
				"Increase 5% magic damage and crit\ndecrease 10% mana cost";
			player.GetDamage(DamageClass.Magic) += 0.05f;
			player.GetCritChance(DamageClass.Magic) += 5;
			player.manaCost *= 0.9f;
			EquipmentPlayer.SandCracker = true;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Magic) += 0.05f;
			base.UpdateEquip(player);
		}
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 30;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.defense = 2;
			Element = 3;
			ElementLevel = 1.3f;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 3);
			recipe.AddTile(ModContent.TileType<ElementHoroscoper>());

			recipe.Register();
		}
	}

    [AutoloadEquip(EquipType.Head)]
	public class SandCrackerMask : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerMask");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角崩裂者面具");
			Tooltip.SetDefault("It's like a sound says that you can kill 100 Barroths, but what's Battoth?\nincrease 5% minion damage");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "好像有一个声音在说你能杀100只土砂龙,但是啥是土砂龙?\n提高5%召唤伤害");
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SandCrackerArmor>() && legs.type == ModContent.ItemType<SandCrackerCuisse>();
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "提高5%远程伤害和暴击率\n当人物站在物块上方时，远程伤害再增加10%" :
				 "Increase 5% ranged damage and crit\nif player is over a tile, increase 10% more damage";
			player.GetDamage(DamageClass.Ranged) += 0.05f;
			player.GetCritChance(DamageClass.Ranged) += 5;
			for (int i = 0; i < (int)(player.width / 16f); i++)
			{
				int bx = (int)player.BottomLeft.X / 16 + i;
				int by = (int)player.BottomLeft.Y / 16;
				Tile tileBottom = Main.tile[bx, by];
				if (tileBottom.HasUnactuatedTile)
				{
					player.GetDamage(DamageClass.Ranged) += 0.1f;
				}
			}
			EquipmentPlayer.SandCracker = true;
		}
		public override void UpdateEquip(Player player)
        {
			player.GetDamage(DamageClass.Ranged) += 0.05f;
			base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 30;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.defense = 2;
			Element = 3;
			ElementLevel = 1.3f;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 3);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
	}
	[AutoloadEquip(EquipType.Head)]
	public class SandCrackerHeadgear : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerHeadgear");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角崩裂者头饰");
			Tooltip.SetDefault("It's like a sound says that you can kill 100 Barroths, but what's Battoth?\nincrease 5% minion damage");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "好像有一个声音在说你能杀100只土砂龙,但是啥是土砂龙?\n提高5%召唤伤害");
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SandCrackerArmor>() && legs.type == ModContent.ItemType<SandCrackerCuisse>();
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "提高5%召唤伤害和暴击率\n提高15%盾矛防御加成和2点盾矛额外防御" :
				  "Increase 5% summon damage and crit\nincrease 15% ShieldSpear defense addition and 2 ShieldSpear extra defense";
			player.GetDamage(DamageClass.Summon) += 0.05f;
			player.GetCritChance(DamageClass.Summon) += 5;
			EquipmentPlayer.SandCracker = true;
            player.GetModPlayer<BasePlayer>().ShieldSpearDefensePer += 0.15f;
            player.GetModPlayer<BasePlayer>().ShieldSpearExtraDefense += 2;
		}
		public override void UpdateEquip(Player player)
        {
			player.GetDamage(DamageClass.Summon) += 0.05f;
			base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 30;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.defense = 1;
			Element = 3;
			ElementLevel = 1.3f;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 3);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
	}
    [AutoloadEquip(EquipType.Head)]
	public class SandCrackerHelmet : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerHelmet");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角崩裂者角盔");
			Tooltip.SetDefault("It's like a sound says that you can kill 100 Barroths, but what's Battoth?\nincrease 5% melee damage");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "好像有一个声音在说你能杀100只土砂龙,但是啥是土砂龙?\n提高5%近战伤害");
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SandCrackerArmor>() && legs.type == ModContent.ItemType<SandCrackerCuisse>();
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "提高5%近战伤害和暴击率\n提高2点防御和2点生命恢复" :
				   "Increase 5% melee damage and crit\nincrease 2 defense and 2 life regen";
			player.lifeRegen += 2;
            player.GetDamage(DamageClass.Melee) += 0.05f;
			player.GetCritChance(DamageClass.Melee) += 5;
			player.statDefense += 2;
            EquipmentPlayer.SandCracker = true;
			//player.GetModPlayer<BasePlayer>().SickleDamagePer += 0.1f;
		}
		public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.05f;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 30;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.defense = 4;
			Element = 3;
			ElementLevel = 1.3f;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 3);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
	}
	[AutoloadEquip(EquipType.Body)]
	public class SandCrackerArmor : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerArmor");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角崩裂者护甲");
			Tooltip.SetDefault("It's like a sound says that you can kill 100 Barroths, but what's Battoth?\nincrease 8% endurance and 8% crit");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "好像有一个声音在说你能杀100只土砂龙,但是啥是土砂龙?\n提高5%伤害减免和5%暴击率");
		}
		public override void UpdateEquip(Player player)
        {
            player.endurance += 0.08f;
			player.GetCritChance(DamageClass.Generic) += 8;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 24;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.defense = 5;
			Element = 3;
			ElementLevel = 1.3f;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 5);
			recipe.AddIngredient(ModContent.ItemType<SandDiablosBody_bone>(), 1);
			recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
	}
    [AutoloadEquip(EquipType.Legs)]
	public class SandCrackerCuisse : ElementItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandCrackerCuisse");
			DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角崩裂者腿甲");
			Tooltip.SetDefault("It's like a sound says that you can kill 100 Barroths, but what's Battoth?\nincrease 10% move speed");
			Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "好像有一个声音在说你能杀100只土砂龙,但是啥是土砂龙?\n提高10%移动速度");
		}
        public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 18;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.defense = 2;
			Element = 3;
			ElementLevel = 1.3f;
		}		
		public override void UpdateEquip(Player player)
        {
            player.maxRunSpeed += 0.1f;
            base.UpdateEquip(player);
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SandDiablosCarapace>(), 3);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
			
			recipe.Register();
		}
	}
}
