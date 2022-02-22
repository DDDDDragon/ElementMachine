using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ElementMachine.Effect.CameraModifiers
{
	public class CameraModifierStack
	{
		public void Add(ICameraModifier modifier)
		{
			this.RemoveIdenticalModifiers(modifier);
			this._modifiers.Add(modifier);
		}
		private void RemoveIdenticalModifiers(ICameraModifier modifier)
		{
			string uniqueIdentity = modifier.UniqueIdentity;
			if (uniqueIdentity == null)
			{
				return;
			}
			for (int i = this._modifiers.Count - 1; i >= 0; i--)
			{
				if (this._modifiers[i].UniqueIdentity == uniqueIdentity)
				{
					this._modifiers.RemoveAt(i);
				}
			}
		}
		public void ApplyTo(ref Vector2 cameraPosition)
		{
			CameraInfo cameraInfo = new CameraInfo(cameraPosition);
			this.ClearFinishedModifiers();
			for (int i = 0; i < this._modifiers.Count; i++)
			{
				this._modifiers[i].Update(ref cameraInfo);
			}
			cameraPosition = cameraInfo.CameraPosition;
		}
		private void ClearFinishedModifiers()
		{
			for (int i = this._modifiers.Count - 1; i >= 0; i--)
			{
				if (this._modifiers[i].Finished)
				{
					if(this._modifiers[i] is RestrictCameraModifier) (this._modifiers[i] as RestrictCameraModifier).Reset();
					this._modifiers.RemoveAt(i);
				}
			}
		}
		public void Clear()
		{
			_modifiers.Clear();
		}
		private List<ICameraModifier> _modifiers = new List<ICameraModifier>();
	}
}
