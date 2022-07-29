using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExampleMod.Tiles
{
	public abstract class ExampleBlock : ModTile
	{
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			//drop = ModContent.ItemType<Items.Placeable.ExampleBlock>();
			AddMapEntry(new Color(86, 64, 50));
		}
	}
}