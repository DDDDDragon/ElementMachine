using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ElementMachine.Tiles;

namespace ElementMachine.Element.Ice.Frozen
{
    [AutoloadEquip(EquipType.Legs)]
    public class FrozenProtectorCuisse : ElementItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrozenProtectorCuisse");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "霜寒守卫者护腿");
            Tooltip.SetDefault("increase 5% melee Crit");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "提高5%的近战暴击率");
            base.SetStaticDefaults();
        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += 5;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 12;
            Item.defense = 1;
            Item.rare = ItemRarityID.Blue;
            Item.value = 2000;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.SnowBlock, 20);
            recipe.AddIngredient(ItemID.SlushBlock, 20);
            recipe.AddIngredient(ModContent.ItemType<FrozenStone>(), 7);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
            
            recipe.Register();
        }
    }
}
