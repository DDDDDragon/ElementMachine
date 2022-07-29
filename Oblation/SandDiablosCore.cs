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
            Main.NewText("dadadadadadad");
            base.OnSacrifice(player);
        }
        public override MyPlayer.AltarType RequestSacrifice()
        {
            return MyPlayer.AltarType.Earth;
        }
    }
}