using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using Terraria;
using System.Reflection;
using Terraria.Localization;
using ElementMachine.Tiles;

namespace ElementMachine
{
    public class MyPlayer : ModPlayer 
    {
        public static List<string> AnalyzedItemsName = new List<string>();
        public static List<int> AnalyzedItemsValue = new List<int>();
        
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            tag.Add("AnalyzedItemsName", AnalyzedItemsName);
            tag.Add("AnalyzedItemsValue", AnalyzedItemsValue);
            return tag;
        }
        public override void Load(TagCompound tag)
        {
            AnalyzedItemsName = tag.Get<List<string>>("AnalyzedItemsName");
            AnalyzedItemsValue = tag.Get<List<int>>("AnalyzedItemsValue");
            base.Load(tag);
        }
        public override void PreUpdate()
        {
            Dictionary<string, LocalizedText> DSL = LanguageManager.Instance.GetType().GetField("_localizedTexts", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(LanguageManager.Instance) as Dictionary<string, LocalizedText>;
            if(Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredTile[0] == ModContent.TileType<AlloyAnalyzer>())
            {
                if(GameCulture.Chinese.IsActive) typeof(LocalizedText).GetProperty("Value").GetSetMethod(true).Invoke(DSL["LegacyInterface.25"], new object[]{ "分析" });
                else typeof(LocalizedText).GetProperty("Value").GetSetMethod(true).Invoke(DSL["LegacyInterface.25"], new object[]{ "Analyze" });
            } 
            else if(GameCulture.Chinese.IsActive) typeof(LocalizedText).GetProperty("Value").GetSetMethod(true).Invoke(DSL["LegacyInterface.25"], new object[]{ "制作" });
            else typeof(LocalizedText).GetProperty("Value").GetSetMethod(true).Invoke(DSL["LegacyInterface.25"], new object[]{ "Crafting" });
            base.PreUpdate();
        }
        public override void ResetEffects()
        {
            base.ResetEffects();
        }
    }
}