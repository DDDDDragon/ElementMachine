using Terraria;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ElementMachine.World
{
    public class RandomCreate
    {
        /// <summary>
        /// height=>地形最大高度, width=>地形最大宽度, space=>内部空间大小(格数)
        /// </summary>
        /// <param name="height">地形最大高度</param>
        /// <param name="width">地形最大宽度</param>
        /// <param name="space">内部空间大小(格数)</param>
        public RandomCreate(int height, int width, int space)
        {
            this.MaxHeight = height;
            this.MaxWidth = width;
            this.step = space;
        }
        public void Create()
        {
            CreateSpace();
            CreateBlock();
            CutMap();
        }
        /// <summary>
        /// 这个方法必须在Create()方法后调用!
        /// </summary>
        public void setToWorld()
        {
            for(int i = 0; i < MaxWidth; i++)
            {
                string MapLine = "";
                for(int j = 0; j < MaxHeight; j++)
                {
                    MapLine += Map[i, j];
                }
                Main.NewText(MapLine);
            }
        }
        void CreateSpace()
        {
            Main.NewText("正在执行CreateSpace");
            Vector2 nowPoint = new Vector2((int)MaxWidth / 2,(int)MaxHeight / 2);//起始位置
            Wall.Clear();
            Road.Clear();
            Map = new int[MaxWidth, MaxHeight];
            Main.NewText("初始化");
            for(int i = 0; i < MaxWidth; i++)
            {
                for(int j = 0; j < MaxHeight; j++)
                {
                    Map[i, j] = 0;
                }
            }
            setBlock(nowPoint, 2);
            Main.NewText("生成通路");
            for(; Road.Count < step;)//生成通路
            {
                if(!Road.ContainsKey(nowPoint))
                {
                    Road.Add(nowPoint, 2);
                    setBlock(nowPoint, 2);
                }
                nowPoint = RandomDirection(nowPoint);
            }
        }
        void CreateBlock()
        {
            Main.NewText("正在执行CreateBlock");
            Vector2 up, down, left, right;
            foreach(Vector2 item in Road.Keys)
            {
                right = item + new Vector2(1, 0);
                left = item + new Vector2(-1, 0);
                up = item + new Vector2(0, 1);
                down = item + new Vector2(0, -1);

                if(!Wall.Contains(up) && !Road.ContainsKey(up) && up.X < MaxWidth && up.Y < MaxHeight && up.X >= 0 && up.Y >= 0) Wall.Add(up);
                if(!Wall.Contains(down) && !Road.ContainsKey(down) && down.X < MaxWidth && down.Y < MaxHeight && down.X >= 0 && down.Y >= 0) Wall.Add(down);
                if(!Wall.Contains(right) && !Road.ContainsKey(right) && right.X < MaxWidth && right.Y < MaxHeight && right.X >= 0 && right.Y >= 0) Wall.Add(right);
                if(!Wall.Contains(left) && !Road.ContainsKey(left) && left.X < MaxWidth && left.Y < MaxHeight && left.X >= 0 && left.Y >= 0) Wall.Add(left);
            }
            Main.NewText("设置墙壁");
            foreach(Vector2 item in Wall) setBlock(item, 1);
        }
        void CutMap()
        {
            Main.NewText("正在执行CutMap");
            List<Vector2> tempWall = new List<Vector2>(Wall);
            int countRoad = 0;
            Vector2 up, down, left, right;
            foreach(Vector2 item in Wall)
            {
                right = item + new Vector2(1, 0);
                left = item + new Vector2(-1, 0);
                up = item + new Vector2(0, 1);
                down = item + new Vector2(0, -1);

                if(Road.ContainsKey(right)) countRoad++;
                if(Road.ContainsKey(left)) countRoad++;
                if(Road.ContainsKey(up)) countRoad++;
                if(Road.ContainsKey(down)) countRoad++;

                if(countRoad >= 3)
                {
                    setBlock(item, 2);
                    Road.Add(item, 2);
                    Wall.Remove(item);
                }

                countRoad = 0;
            }
        }
        void setBlock(Vector2 pos, int type)
        {
            Map[(int)pos.X, (int)pos.Y] = type;
        }
        Vector2 RandomDirection(Vector2 nowPoint)//随机下一步
        {
            int random = Main.rand.Next(0, 5);
            Vector2 up, down, left, right;
            right = nowPoint + new Vector2(1, 0);
            left = nowPoint + new Vector2(-1, 0);
            up = nowPoint + new Vector2(0, 1);
            down = nowPoint + new Vector2(0, -1);
            switch(random)
            {
                case 0 :
                    if(right.X < MaxWidth && right.Y < MaxHeight && right.X >= 0 && right.Y >= 0) return right;
                    break;
                case 1 :
                    if(left.X < MaxWidth && left.Y < MaxHeight && left.X >= 0 && left.Y >= 0) return left;
                    break;
                case 2 :
                    if(up.X < MaxWidth && up.Y < MaxHeight && up.X >= 0 && up.Y >= 0) return up;
                    break;
                case 3 :
                    if(down.X < MaxWidth && down.Y < MaxHeight && down.X >= 0 && down.Y >= 0) return down;
                    break;    
            }
            RandomDirection(nowPoint);
            return Vector2.Zero;
        }
        public int step;
        public int MaxHeight;
        public int MaxWidth;
        public int[,] Map;
        Dictionary<Vector2, int> Road = new Dictionary<Vector2, int>();
        List<Vector2> Wall = new List<Vector2>();
        public static void XXX<T>(params T[] t)
        {
            
        }
    }
}