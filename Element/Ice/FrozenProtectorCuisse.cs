using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Tiles;

namespace ElementMachine.Element.Ice
{
    [AutoloadEquip(EquipType.Legs)]
    public class FrozenProtectorCuisse : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrozenProtectorCuisse");
            DisplayName.AddTranslation(GameCulture.Chinese, "霜寒守卫者护腿");
            Tooltip.SetDefault("提高5%的近战暴击率");
            base.SetStaticDefaults();
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeCrit += 5;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 12;
            item.defense = 1;
            item.rare = ItemRarityID.Blue;
            item.value = 2000;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.SnowBlock, 20);
            recipe.AddIngredient(ItemID.SlushBlock, 20);
            recipe.AddIngredient(ModContent.ItemType<FrozenStone>(), 7);
            recipe.AddTile(ModContent.TileType<ElementHoroScoper>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
