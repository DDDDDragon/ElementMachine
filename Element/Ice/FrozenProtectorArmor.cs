using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Tiles;

namespace ElementMachine.Element.Ice
{
    [AutoloadEquip(EquipType.Body)]
    public class FrozenProtectorArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrozenProtectorArmor");
            DisplayName.AddTranslation(GameCulture.Chinese, "霜寒守卫者胸甲");
            Tooltip.SetDefault("increase 5% melee damage");
            Tooltip.AddTranslation(GameCulture.Chinese, "增加5%的近战伤害");
            base.SetStaticDefaults();
        }
        public override string Texture => base.Texture;
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 18;
            item.defense = 2;
            item.rare = ItemRarityID.Blue;
            item.value = 2000;
            base.SetDefaults();
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.05f;
            base.UpdateEquip(player);
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<FrozenProtectorHelmet>() && legs.type == ModContent.ItemType<FrozenProtectorCuisse>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "增加3%的近战伤害和3%的近战暴击率\n增加1点防御力\n在雪地中时再增加1点防御力\n被击中时减速周围敌人";
            if(player.ZoneSnow)
            {
                player.statDefense += 1;
            }
            player.meleeDamage += 0.03f;
            player.meleeCrit += 3;
            player.statDefense += 1;
            EquipmentPlayer.FrozenProtector = true;
            base.UpdateArmorSet(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceBlock, 30);
            recipe.AddIngredient(ItemID.SnowBlock, 30);
            recipe.AddIngredient(ItemID.SlushBlock, 30);
            recipe.AddIngredient(ModContent.ItemType<FrozenStone>(), 10);
            recipe.AddTile(ModContent.TileType<ElementHoroScoper>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
