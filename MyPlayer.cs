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
            Ice,
            Earth
        }
        public static Dictionary<Vector2, AltarType> AltarTypes = new Dictionary<Vector2, AltarType>();
        public static Dictionary<Vector2, bool> AltarS = new Dictionary<Vector2, bool>();
        public static Dictionary<Vector2, int> AltarItem = new Dictionary<Vector2, int>();
        public static bool IsSacrifice = false;
        public override void SaveData(TagCompound tag)
        {
            tag.Add("AnalyzedItemsName", AnalyzedItemsName);
            tag.Add("AnalyzedItemsValue", AnalyzedItemsValue);
            tag.Add("Oblations", Oblations);
            tag.Add("FirstTalk", FirstTalk);
            base.SaveData(tag);
        }
        public override void LoadData(TagCompound tag)
        {
            AnalyzedItemsName = tag.Get<List<string>>("AnalyzedItemsName");
            AnalyzedItemsValue = tag.Get<List<int>>("AnalyzedItemsValue");
            Oblations = tag.Get<List<string>>("Oblations");
            FirstTalk = tag.GetBool("FirstTalk");
            //AltarTypes = tag.Get<Dictionary<Vector2, AltarType>>("AltarTypes");
            base.LoadData(tag);
        }
        public override void PreUpdate()
        {
            Dictionary<string, LocalizedText> DSL = LanguageManager.Instance.GetType().GetField("_localizedTexts", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(LanguageManager.Instance) as Dictionary<string, LocalizedText>;
            if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredTile.Count == 0)
            {
                base.PreUpdate();
                return;
            }
                if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredTile[0] == ModContent.TileType<AlloyAnalyzer>())
            {
                if(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive) typeof(LocalizedText).GetProperty("Value").GetSetMethod(true).Invoke(DSL["LegacyInterface.25"], new object[]{ "分析" });
                else typeof(LocalizedText).GetProperty("Value").GetSetMethod(true).Invoke(DSL["LegacyInterface.25"], new object[]{ "Analyze" });
            } 
            else if(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive) typeof(LocalizedText).GetProperty("Value").GetSetMethod(true).Invoke(DSL["LegacyInterface.25"], new object[]{ "制作" });
            else typeof(LocalizedText).GetProperty("Value").GetSetMethod(true).Invoke(DSL["LegacyInterface.25"], new object[]{ "Crafting" });
            if(!talkToGuide)
            {
                
            }
            base.PreUpdate();
        }
        public override void ResetEffects()
        {
            base.ResetEffects();
        }
        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
        {
            itemsByMod["Terraria"].Clear();
            base.ModifyStartingInventory(itemsByMod, mediumCoreDeath);
            
        }
        public override void OnEnterWorld(Player player)
        {
            if (!FirstTalk) Main.NewText(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "向导貌似找你有些事情" : "The Guide may have something to talk with you");
            base.OnEnterWorld(player);
        }
        public bool FirstTalk = false;
        public int TalkIndex = 0;
        public bool talkToGuide = false;
    }
}