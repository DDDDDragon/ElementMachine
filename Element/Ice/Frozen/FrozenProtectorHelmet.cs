using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Tiles;

namespace ElementMachine.Element.Ice.Frozen
{
    [AutoloadEquip(EquipType.Head)]
    public class FrozenProtectorHelmet : ElementItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrozenProtectorHelmet");
            DisplayName.AddTranslation(GameCulture.Chinese, "霜寒守卫者头盔");
            Tooltip.SetDefault("increase 5% melee speed");
            Tooltip.AddTranslation(GameCulture.Chinese, "提高5%的近战速度");
            base.SetStaticDefaults();
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed += 0.05f;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
        {
            
            AddElement(ElementsType.Ice);
            item.width = 22;
            item.height = 20;
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
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
