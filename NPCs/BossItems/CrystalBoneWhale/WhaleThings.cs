using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace ElementMachine.NPCs.BossItems.CrystalBoneWhale
{
    public class WhaleOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WhaleOre");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese),"晶骨甲矿");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.value = 10;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
        }
    }
        public class WhaleBonePiece : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WhaleBonePiece");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese),"晶骨甲片");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 28;
            Item.value = 10;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
        }
    }
        public class WhaleSkin : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WhaleSkin");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese),"晶骨甲鲸皮");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
            Item.value = 10;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
        }
    }
}
