using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using Terraria;
using System.Reflection;
using Terraria.Localization;
using ElementMachine.Tiles;
using ElementMachine.World;
using Microsoft.Xna.Framework;

namespace ElementMachine
{
    public class MyPlayer : ModPlayer 
    {
        public static List<string> AnalyzedItemsName = new List<string>();
        public static List<int> AnalyzedItemsValue = new List<int>();
        public static List<string> Oblations = new List<string>();
        public static RandomCreate randomCreate = new RandomCreate(10, 10, 20);
        public enum AltarType
        {
            None,
            Flame,
            Ice
        }
        public static Dictionary<Vector2, AltarType> AltarTypes = new Dictionary<Vector2, AltarType>();
        
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
            tag.Add("AnalyzedItemsName", AnalyzedItemsName);
            tag.Add("AnalyzedItemsValue", AnalyzedItemsValue);
            tag.Add("Oblations", Oblations);
            //tag.Add("AltarTypes", AltarTypes);
            return tag;
        }
        public override void Load(TagCompound tag)
        {
            AnalyzedItemsName = tag.Get<List<string>>("AnalyzedItemsName");
            AnalyzedItemsValue = tag.Get<List<int>>("AnalyzedItemsValue");
            Oblations = tag.Get<List<string>>("Oblations");
            //AltarTypes = tag.Get<Dictionary<Vector2, AltarType>>("AltarTypes");
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