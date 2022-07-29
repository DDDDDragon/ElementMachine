using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Social;
using Terraria.UI;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace ElementMachine.World
{
    public class Tiles
    {
        public List<List<Tile>> tilesData = new List<List<Tile>>();
    }
    public class TilesInfo
    {
        public Tiles tiles = new Tiles();
        public int Height;
        public int Width;
        public void SetTiles(Vector2 pos, int height, int width)
        {
            int x = 0;
            int y = 0;
            List<Tile> list = new List<Tile>();
            while(x <= width && y <= height)
            {
                Tile tile = new Tile();
                tile.CopyFrom(Main.tile[(int)pos.X + x, (int)pos.Y + y]);
                list.Add(tile);
                Main.NewText(tile.TileType);
                x++;
                if(x == width)
                {
                    if(y != height)
                    {
                        tiles.tilesData.Add(list);
                        list.Clear();
                        y++;
                        x = 0;
                    } 
                    else 
                    {
                        tiles.tilesData.Add(list);
                        y++;
                        break;
                    }
                }
            }
            Height = height;
            Width = width;
        }
        public void Output()
        {
            int x = 0;
            int y = 0;
            string a = "";
            while(x <= Width && y <= Height)
            {
                a += tiles.tilesData[x][y].TileType.ToString();
                x++;
                if(x == Width)
                {
                    if(y != Height)
                    {
                        y++;
                        Main.NewText(a);
                        a = "";
                        x = 0;
                    } 
                    else break;
                }
            }
        }
    }
}