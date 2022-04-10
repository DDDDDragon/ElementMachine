using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using ElementMachine.Element.Ice.Frozen;
using Terraria.ID;
using ElementMachine.Tiles;

namespace ElementMachine.Oblation
{
    public class IceShootCore : OblationCore
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("IceShootCore");
            DisplayName.AddTranslation(GameCulture.Chinese, "冰霜弹祭品核心");
            Tooltip.SetDefault("sacrifice this on Ice Altar to shoot ice when using Ice-Element Weapon");
            Tooltip.AddTranslation(GameCulture.Chinese, "在冰霜祭坛上献祭它以获得使用冰属性武器时发射冰霜弹的能力\n使用武器时额外射出冰霜弹\n伤害为当前人物防御*0.1 + 手持物品伤害*0.3 + 基础伤害(1点),最高为20点\n注:长枪蓄力时可以持续发射");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            item.width = 58;
            item.height = 58;
            item.maxStack = 1;
            item.value = 1000;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            OblationRecipe recipe = new OblationRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<FrozenStone>(), 20);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 20);
            recipe.Single = true;
            recipe.AddTile(ModContent.TileType<ElementHoroscoper>());
            recipe.SetResult(this);
            recipe.AddRecipe();
            base.AddRecipes();
        }
    }
}