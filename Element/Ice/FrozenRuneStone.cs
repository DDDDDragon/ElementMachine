using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.UI;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace ElementMachine.Element.Ice
{
    public class FrozenRuneStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrozenRuneStone");
            DisplayName.AddTranslation(GameCulture.Chinese, "霜寒符石");
            Tooltip.SetDefault("Don't ask me why ice and snow can be made into stone\nafter all it's so cold!!!\n1 more defnese and 2 in snow\nadd 3% melee damage and 2 armor penetration");
            Tooltip.AddTranslation(GameCulture.Chinese, "别问我冰和雪是怎么做成石头的\n反正它很冷就对了\n增加一点防御力，在雪地时则增加两点\n增加3%的近战伤害与2点穿甲");
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            item.height = 26;
            item.width = 18;
            item.accessory = true;
            item.value = 2000;
            item.rare = ItemRarityID.Blue;
            base.SetDefaults();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 1;
            if (player.ZoneSnow) player.statDefense += 1;
            player.meleeDamage += 0.03f;
            player.armorPenetration += 2;
            base.UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceBlock, 20);
			recipe.AddIngredient(ItemID.SnowBlock, 20);
			recipe.AddIngredient(ItemID.SlushBlock, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
