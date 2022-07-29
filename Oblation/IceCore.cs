using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using ElementMachine.Element.Ice.Frozen;
using Terraria.ID;
using ElementMachine.Tiles;
using ElementMachine.Buffs;

namespace ElementMachine.Oblation
{
    public class IceCore : OblationCore
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("IceCore");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "冰霜祭品核心");
            Tooltip.SetDefault("sacrifice this on Ice Altar to gain damage increase when using Ice-Element Weapon");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "在冰霜祭坛上献祭它以获得冰属性武器伤害加成");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 58;
            Item.maxStack = 10;
            Item.value = 1000;
            single = false;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            Recipe recipe = OblationRecipe.CreateOblationRecipe(this, 1);
            recipe.AddIngredient(ModContent.ItemType<FrozenStone>(), 2);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 20);
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
            recipe.Register();
            base.AddRecipes();
        }
        public override void OnSacrifice(Player player)
        {
            player.AddBuff(ModContent.BuffType<IceIncreaseL1>(), 7200);
            base.OnSacrifice(player);
        }
        public override MyPlayer.AltarType RequestSacrifice()
        {
            return MyPlayer.AltarType.Ice;
        }
    }
}