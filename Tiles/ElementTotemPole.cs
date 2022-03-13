using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.Localization;
using ElementMachine.Machine;

namespace ElementMachine.Tiles
{
    public class ElementTotemPole : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 4; 
            TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 16, 16, 16 };
            TileObjectData.addTile(Type); 
			AddMapEntry(new Color(126, 67, 0));
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Element Totem Pole");
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<ElementTotemPoleItem>());
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Vector2 vec = TileUtils.GetTileOrigin(i, j);
            if(Main.tile[(int)vec.X + 2, (int)vec.Y].type == ModContent.TileType<ElementAltar>())
            {
                
            }
            base.NearbyEffects(i, j, closer);
        }
    }
    public class ElementTotemPoleItem : ElementItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ElementTotemPole");
            DisplayName.AddTranslation(GameCulture.Chinese, "元素图腾柱");
            Tooltip.SetDefault("for building Altar");
            Tooltip.AddTranslation(GameCulture.Chinese, "建造祭坛用");
        }
        public override void SetDefaults()
        {
            item.width = 32;
			item.height = 18;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = ModContent.TileType<ElementTotemPole>();
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 5);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemID.FallenStar, 1);
            recipe1.AddRecipeGroup(RecipeGroupID.Wood, 30);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(this, 2);
            recipe1.AddRecipe();
        }

    }
}