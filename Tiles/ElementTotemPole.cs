using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.Localization;
using ElementMachine.Machine;
using Terraria.DataStructures;

namespace ElementMachine.Tiles
{
    public class ElementTotemPole : ModTile
    {
        public override void SetStaticDefaults()
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
            TileID.Sets.DisableSmartCursor[Type] = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 16, ModContent.ItemType<ElementTotemPoleItem>());
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Vector2 vec = TileUtils.GetTileOrigin(i, j);
            if(Main.tile[(int)vec.X + 2, (int)vec.Y].TileType == ModContent.TileType<ElementAltar>())
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "元素图腾柱");
            Tooltip.SetDefault("for building Altar");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "建造祭坛用");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
			Item.height = 18;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<ElementTotemPole>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 5);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 15);
            recipe.AddTile(TileID.Anvils);
            
            recipe.Register();
            Recipe recipe1 = CreateRecipe(2);
            recipe1.AddIngredient(ItemID.FallenStar, 1);
            recipe1.AddRecipeGroup(RecipeGroupID.Wood, 30);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }

    }
}