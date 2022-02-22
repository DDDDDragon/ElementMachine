using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace ElementMachine.Effect.CameraModifiers
{
	public class PunchCameraModifier : ICameraModifier
	{
		public string UniqueIdentity { get; private set; }
		public bool Finished { get; private set; }
		public PunchCameraModifier(Vector2 startPosition, Vector2 direction, float strength, float vibrationCyclesPerSecond, int frames, float distanceFalloff = -1f, string uniqueIdentity = null)
		{
			this._startPosition = startPosition;
			this._direction = direction;
			this._strength = strength;
			this._vibrationCyclesPerSecond = vibrationCyclesPerSecond;
			this._framesToLast = frames;
			this._distanceFalloff = distanceFalloff;
			this.UniqueIdentity = uniqueIdentity;
		}
        public float Remap(float fromValue, float fromMin, float fromMax, float toMin, float toMax, bool clamped = true)
		{
			return MathHelper.Lerp(toMin, toMax, GetLerpValue(fromMin, fromMax, fromValue, clamped));
		}
        public float GetLerpValue(float from, float to, float t, bool clamped = false)
		{
			if (clamped)
			{
				if (from < to)
				{
					if (t < from)
					{
						return 0f;
					}
					if (t > to)
					{
						return 1f;
					}
				}
				else
				{
					if (t < to)
					{
						return 1f;
					}
					if (t > from)
					{
						return 0f;
					}
				}
			}
			return (t - from) / (to - from);
		}
		public void Update(ref CameraInfo cameraInfo)
		{
			float scaleFactor = (float)Math.Cos((double)((float)this._framesLasted / 60f * this._vibrationCyclesPerSecond * 6.2831855f));
			float scaleFactor2 = Remap((float)this._framesLasted, 0f, (float)this._framesToLast, 1f, 0f, true);
			float scaleFactor3 = Remap(Vector2.Distance(this._startPosition, cameraInfo.OriginalCameraCenter), 0f, this._distanceFalloff, 1f, 0f, true);
			if (this._distanceFalloff == -1f)
			{
				scaleFactor3 = 1f;
			}
			cameraInfo.CameraPosition += this._direction * scaleFactor * this._strength * scaleFactor2 * scaleFactor3;
			this._framesLasted++;
			if (this._framesLasted >= this._framesToLast)
			{
				this.Finished = true;
			}
		}
		private int _framesToLast;
		private Vector2 _startPosition;
		private Vector2 _direction;
		private float _distanceFalloff;
		private float _strength;
		private float _vibrationCyclesPerSecond;
		private int _framesLasted;
	}
}
