using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace ElementMachine.Effect.CameraModifiers
{
	public struct CameraInfo
	{
		public CameraInfo(Vector2 position)
		{
			this.OriginalCameraPosition = position;
            Vector2 screenSize = new Vector2(Main.screenWidth, Main.screenHeight);
			this.OriginalCameraCenter = position + screenSize / 2f;
			this.CameraPosition = this.OriginalCameraPosition;
		}
		public Vector2 CameraPosition;
		public Vector2 OriginalCameraCenter;
		public Vector2 OriginalCameraPosition;
	}
}