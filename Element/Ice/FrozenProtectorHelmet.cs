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
    [AutoloadEquip(EquipType.Head)]
    public class FrozenProtectorHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrozenProtectorHelmet");
            DisplayName.AddTranslation(GameCulture.Chinese, "霜寒守卫者头盔");
            Tooltip.SetDefault("提高5%的近战速度");
            base.SetStaticDefaults();
        }
        public override void UpdateEquip(Player player)
        {
            player.meleeSpeed += 0.05f;
            base.UpdateEquip(player);
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.defense = 1;
            item.rare = ItemRarityID.Blue;
            item.value = 2000;
            base.SetDefaults();
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
