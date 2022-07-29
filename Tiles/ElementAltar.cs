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
using ElementMachine.Element.Ice.Frozen;
using ElementMachine.Effect.CameraModifiers;
using ElementMachine.Effect;
using System;
using Terraria.GameContent;

namespace ElementMachine.Tiles
{
    public class ElementAltar : ModTile
    {
        public override void SetStaticDefaults()
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
            TileID.Sets.DisableSmartCursor[Type] = true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 16, ModContent.ItemType<ElementAltarItem>());
        }
        int ticks = 0;
        int timer = 0;
        int saTimer = 0;
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {            
            base.PostDraw(i, j, spriteBatch);
            Player player = Main.LocalPlayer;
            Vector2 vec = TileUtils.GetTileOrigin(i, j);
            if(!MyPlayer.AltarS.ContainsKey(vec)) MyPlayer.AltarS.Add(vec, false);
            if(!MyPlayer.AltarItem.ContainsKey(vec)) MyPlayer.AltarItem.Add(vec, -1);
            if(i != vec.X + 2 || j != vec.Y + 3) return;
            ticks++;
            if(ticks == 5)
            {
                ticks = 0;
                timer++;
            }
            if(timer == 6) timer = 0;
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen) zero = Vector2.Zero;
            if(Main.tile[(int)vec.X - 1, (int)vec.Y + 1].TileType == ModContent.TileType<ElementTotemPole>() && Main.tile[(int)vec.X + 3, (int)vec.Y + 1].TileType == ModContent.TileType<ElementTotemPole>())
            {
                Vector2 vec2 = vec * 16 - Main.screenPosition + zero - new Vector2(32, 0);
                if(MyPlayer.AltarTypes.ContainsKey(vec))
                {
                    Rectangle rec = new Rectangle(112 * timer, 0, 112, 64);
                    if(MyPlayer.AltarS[vec] && !MyPlayer.IsSacrifice) rec = new Rectangle(672, 0, 112, 64);
                    if(MyPlayer.AltarTypes[vec] == MyPlayer.AltarType.Flame) spriteBatch.Draw(ModContent.Request<Texture2D>("ElementMachine/Tiles/ElementAltar_FlameGlow").Value, vec2, Color.White);
                    if(MyPlayer.AltarTypes[vec] == MyPlayer.AltarType.Ice) spriteBatch.Draw(ModContent.Request<Texture2D>("ElementMachine/Tiles/ElementAltar_IceGlow").Value, vec2, rec, Color.White);
                }
                if(MyPlayer.AltarS[vec] && !MyPlayer.IsSacrifice) 
                {
                    saTimer++;
                    Vector2 vec3 = vec2 + new Vector2(56, 0);
                    Texture2D tex = TextureAssets.Item[MyPlayer.AltarItem[vec]].Value;
                    spriteBatch.Draw(tex, vec3, new Rectangle(0, 0, tex.Width, tex.Height), Color.White, 0, tex.Size() / 2, 0.75f, SpriteEffects.None, 0);
                }
                if (saTimer == 240)
                {
                    MyPlayer.AltarS[vec] = false;
                    saTimer = 0;
                }

            }
            if(MyPlayer.AltarS[vec] && MyPlayer.AltarItem[vec] != -1 && MyPlayer.IsSacrifice) 
            {
                (player.HeldItem.ModItem as OblationCore).OnSacrifice(player);
            }
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Main.mouseRightRelease = false;
            Vector2 vec = TileUtils.GetTileOrigin(i, j);
            if(!MyPlayer.AltarTypes.ContainsKey(vec)) MyPlayer.AltarTypes.Add(vec, MyPlayer.AltarType.None);
            if(Main.tile[(int)vec.X - 1, (int)vec.Y + 1].TileType == ModContent.TileType<ElementTotemPole>() && Main.tile[(int)vec.X + 3, (int)vec.Y + 1].TileType == ModContent.TileType<ElementTotemPole>())
            {
                if(player.HeldItem.ModItem is OblationCore)
                {
                    if((player.HeldItem.ModItem as OblationCore).RequestSacrifice() == MyPlayer.AltarTypes[vec]) 
                    {
                        player.HeldItem.stack--;
                        if(player.HeldItem.ModItem is BossCore)
                        {
                            RestrictCameraModifier RCM = new RestrictCameraModifier(player.Center, TileUtils.GetTileOrigin(i, j) * 16 + new Vector2(24, 100), 20, new Func<bool>(() => !MyPlayer.AltarS[vec]));
                            EffectPlayer.CMS.Add(RCM);
                            MyPlayer.AltarItem[vec] = player.HeldItem.type;
                            MyPlayer.AltarS[vec] = true;
                        }
                        else (player.HeldItem.ModItem as OblationCore).OnSacrifice(player);
                    }
                    else if(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive) Main.NewText("你需要在对应元素的祭坛上献祭它!");
                    else Main.NewText("You need to sacrifice it on the correct Element Altar!");
                }
                #region 冰霜祭品献祭
                if(player.HeldItem.type == ModContent.ItemType<FrozenStoneCoin>()) MyPlayer.AltarTypes[vec] = MyPlayer.AltarType.Ice;
                #endregion
            }
            else if(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive)
            {
                Main.NewText("需要两个图腾柱在左右两边!");
            }
            else Main.NewText("need two totem poles on the right and left!");
            return base.RightClick(i, j);
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
            DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "元素祭坛");
            Tooltip.SetDefault("offer element things to get stronger power!\nfor building altar, you should set two Totem Pole and a Altar on the middle");
            Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "献祭元素物品来获得更强大的力量!\n建造祭坛需要两根图腾柱中间摆放一个祭坛");
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
			Item.createTile = ModContent.TileType<ElementAltar>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MagicLoop>(), 15);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 30);
            recipe.AddTile(TileID.Anvils);
            
            recipe.Register();
            Recipe recipe1 = CreateRecipe(2);
            recipe1.AddIngredient(ItemID.FallenStar, 3);
            recipe1.AddRecipeGroup(RecipeGroupID.Wood, 60);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }
    }
}