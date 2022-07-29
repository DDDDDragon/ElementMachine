using Terraria;
using Terraria.ModLoader;

namespace ElementMachine.MyRecipe
{
    public class BlueprintRecipe
    {
        public static void OnCraft(Recipe recipe, Item item)
        {
            if(!MyPlayer.AnalyzedItemsName.Contains(item.ModItem.Name))
            {
                MyPlayer.AnalyzedItemsName.Add(item.ModItem.Name);
                MyPlayer.AnalyzedItemsValue.Add(item.value);
                Main.NewText($"[c/3100FF:{item.Name}] 已分析完毕, 可在向导商店处购买");
            }
        }
    }
}
    