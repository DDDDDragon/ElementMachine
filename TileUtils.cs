using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ElementMachine
{
	public static class TileUtils
	{
		/// <summary>
		/// 获取多块物块的左上角坐标
		/// </summary>
		/// <param name="i">物块X坐标</param>
		/// <param name="j">物块Y坐标</param>
		public static Vector2 GetTileOrigin(int i, int j)
		{
            int type = Main.tile[i, j].TileType;
            while(Main.tile[i - 1, j].TileType == type) i--;
            while(Main.tile[i, j - 1].TileType == type) j--;
			return  new Vector2(i, j);
		}
	}
}