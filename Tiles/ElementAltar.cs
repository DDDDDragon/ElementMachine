using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria.Localization;
using ElementMachine.Machine;
using Terraria.DataStructures;
using ElementMachine.Oblation;
using Microsoft.Xna.Framework.Graphics;
using ElementMachine.Element.Ice;

namespace ElementMachine.Tiles
{
    public class ElementAltar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.CoordinateHeights = new int[]{ 16, 16, 16, 16 };
            TileObjectData.newTile.Origin = new Point16(1, 2);
            TileObjectData.addTile(Type); 
			AddMapEntry(new Color(126, 67, 0));
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Element Altar");
            disableSmartCursor = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<ElementAltarItem>());
        }
        int ticks = 0;
        int timer = 0;
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            base.PostDraw(i, j, spriteBatch);
            ticks++;
            if(ticks == 20)
            {
                ticks = 0;
                timer++;
            }
            if(timer == 6) timer = 0;
            Vector2 vec = TileUtils.GetTileOrigin(i, j);
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen) zero = Vector2.Zero;
            if(i != vec.X || j != vec.Y) return;
            if(MyPlayer.AltarTypes.ContainsKey(vec))
            {
                Vector2 vec2 = vec * 16 - Main.screenPosition + zero - new Vector2(32, 0);
                if(MyPlayer.AltarTypes[vec] == MyPlayer.AltarType.Flame) Main.spriteBatch.Draw(ModContent.GetTexture("ElementMachine/Tiles/ElementAltar_FlameGlow"), vec2, Color.White);
                if(MyPlayer.AltarTypes[vec] == MyPlayer.AltarType.Ice) Main.spriteBatch.Draw(ModContent.GetTexture("ElementMachine/Tiles/ElementAltar_IceGlow"), vec2, new Rectangle(112 * timer, 0, 112, 64), Color.White);
            }
        }

        public override bool NewRightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Main.mouseRightRelease = false;
            Vector2 vec = TileUtils.GetTileOrigin(i, j);
            if(!MyPlayer.AltarTypes.ContainsKey(vec)) MyPlayer.AltarTypes.Add(vec, MyPlayer.AltarType.None);
            if(Main.tile[(int)vec.X - 1, (int)vec.Y + 1].type == ModContent.TileType<ElementTotemPole>() && Main.tile[(int)vec.X + 3, (int)vec.Y + 1].type == ModContent.TileType<ElementTotemPole>())
            {
                if(player.HeldItem.type == ModContent.ItemType<IceShootCore>()) 
                {
                    if(MyPlayer.AltarTypes[vec] == MyPlayer.AltarType.Ice)
                    {
                        player.HeldItem.stack--;
                        OblationGlobalItem.IceShoot = true;
                        if(GameCulture.Chinese.IsActive) Main.NewText("成功献祭" + player.HeldItem.Name);
                        else Main.NewText("successfully sacrifice" + player.HeldItem.Name);
                    }
                    else if(GameCulture.Chinese.IsActive) Main.NewText("你需要在冰霜祭坛上献祭它!");
                    else Main.NewText("You need to sacrifice it on Ice Altar!");
                }
                if(player.HeldItem.type == ModContent.ItemType<FrozenStoneCoin>()) MyPlayer.AltarTypes[vec] = MyPlayer.AltarType.Ice;
            }
            else if(GameCulture.Chinese.IsActive)
            {
                Main.NewText("需要两个图腾柱在左右两边!");
            }
            else Main.NewText("need two totem poles on the right and left!");
            return base.NewRightClick(i, j);
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter >= 5) //replace with duration of frame in ticks
            {
                frameCounter = 0;
                frame++;
                frame %= 5;
            }
        }
    }
    public class ElementAltarItem : ElementItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ElementAltar");
            DisplayName.AddTranslation(GameCulture.Chinese, "元素祭坛");
            Tooltip.SetDefault("offer element things to get stronger power!\nfor building altar, you should set two Totem Pole and a Altar on the middle");
            Tooltip.AddTranslation(GameCulture.Chinese, "献祭元素物品来获得更强大的力量!\n建造祭坛需要两根图腾柱中间摆放一个祭坛");
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
			item.createTile = ModContent.TileType<ElementAltar>();
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 15);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 30);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
            ModRecipe recipe1 = new ModRecipe(mod);
            recipe1.AddIngredient(ItemID.FallenStar, 3);
            recipe1.AddRecipeGroup(RecipeGroupID.Wood, 60);
            recipe1.AddTile(TileID.Anvils);
            recipe1.SetResult(this, 2);
            recipe1.AddRecipe();
        }

    }
}