using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using ElementMachine.Element.Ice.Frozen;
using Terraria.ID;
using ElementMachine.Tiles;

namespace ElementMachine.Oblation
{
    public class SandDiablosCore : BossCore
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SandDiablosCore");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "砂角魔灵核心");
            Tooltip.SetDefault("sacrifice this on Earth Altar to get Treasures");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "在大地祭坛上献祭它以获得宝藏");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 62;
            Item.maxStack = 1;
            Item.value = 1000;
            base.SetDefaults();
        }
        public override void OnSacrifice(Player player)
        {

            base.OnSacrifice(player);
        }
        public override MyPlayer.AltarType RequestSacrifice()
        {
            return MyPlayer.AltarType.Earth;
        }
    }
    public class DiablosandStatue : ElementItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DiablosandStatue");
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "荒砂塑像");
            Tooltip.SetDefault("seem like it can be used to change the element of altar\nhold this item and right click the Altar to open Earth Altar");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "看起来它可以改变祭坛的元素\n手持此物品右键祭坛激活地属性祭坛");
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 28;
            Item.maxStack = 1;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Element = 2;
            ElementLevel = 0.7f;
        }
    }
}