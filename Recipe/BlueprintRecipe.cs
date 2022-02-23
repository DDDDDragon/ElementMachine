using Terraria;
using Terraria.ModLoader;

namespace ElementMachine.Recipe
{
    public class BlueprintRecipe : ModRecipe
    {
        public BlueprintRecipe(Mod mod) : base(mod)
        {

        }

        public override void OnCraft(Item item)
        {
            if(!MyPlayer.AnalyzedItemsName.Contains(item.modItem.Name))
            {
                MyPlayer.AnalyzedItemsName.Add(item.modItem.Name);
                MyPlayer.AnalyzedItemsValue.Add(item.value);
                Main.NewText($"[c/3100FF:{item.Name}] 已分析完毕, 可在向导商店处购买");
            }
            base.OnCraft(item);
        }
    }
}
    