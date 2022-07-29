using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Tiles;
using ElementMachine.Bases;

namespace ElementMachine.Element.Ice.Frozen
{
    [AutoloadEquip(EquipType.Body)]
    public class FrozenProtectorArmor : ElementItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrozenProtectorArmor");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "霜寒守卫者胸甲");
            Tooltip.SetDefault("increase 5% melee damage");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "增加5%的近战伤害");
            base.SetStaticDefaults();
        }
        public override string Texture => base.Texture;
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 18;
            Item.defense = 2;
            Item.rare = ItemRarityID.Blue;
            Item.value = 2000;
            base.SetDefaults();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.05f;
            base.UpdateEquip(player);
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<FrozenProtectorHelmet>() && legs.type == ModContent.ItemType<FrozenProtectorCuisse>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "增加3%的近战伤害和3%的近战暴击率\n增加1点防御力\n在雪地中时再增加1点防御力\n被击中时减速周围敌人\n提高10%盾矛防御加成和1点盾矛额外防御";
            if(player.ZoneSnow)
            {
                player.statDefense += 1;
            }
            player.GetModPlayer<BasePlayer>().ShieldSpearDefensePer += 0.1f;
            player.GetModPlayer<BasePlayer>().ShieldSpearExtraDefense += 1;
            player.GetDamage(DamageClass.Melee) += 0.03f;
            player.GetCritChance(DamageClass.Melee) += 3;
            player.statDefense += 1;
            EquipmentPlayer.FrozenProtector = true;
            base.UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IceBlock, 30);
            recipe.AddIngredient(ItemID.SnowBlock, 30);
            recipe.AddIngredient(ItemID.SlushBlock, 30);
            recipe.AddIngredient(ModContent.ItemType<FrozenStone>(), 10);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
            
            recipe.Register();
        }
    }
}
