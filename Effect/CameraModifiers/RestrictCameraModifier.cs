using System;
using Microsoft.Xna.Framework;
using Terraria;
using ElementMachine.Effect;

namespace ElementMachine.Effect.CameraModifiers
{
	public class RestrictCameraModifier : ICameraModifier
	{
		public string UniqueIdentity { get; private set; }
        public bool Finished { get; private set; }
        public RestrictCameraModifier(Vector2 fromCenter, Vector2 toCenter, float amount, Func<bool> Finish, string uniqueIdentity = null, bool reset = true)
        {
            this.UniqueIdentity = uniqueIdentity;
            this._fromCenter = fromCenter;
            this._toCenter = toCenter;
            this._amount2 = amount;
            this.reset = reset;
            this.canFinish = Finish;
        }
        public Vector2 Remap(Vector2 from, Vector2 to, float amount)
        { 
            return new Vector2(MathHelper.Lerp(from.X, to.X, amount), MathHelper.Lerp(from.Y, to.Y, amount));
        }
        public Vector2 cenToScreenPos(Vector2 center)
        {
            return center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
        }
        public void Update(ref CameraInfo cameraInfo)
        {
            cameraInfo.CameraPosition = cenToScreenPos(_fromCenter) + (cenToScreenPos(_toCenter) - cenToScreenPos(_fromCenter)) *  _nowT;
            if(_nowT < _amount) _nowT += 1f / _amount2;
            if((_amount - _nowT <= 0.01 || _amount <= _nowT) && canFinish()) this.Finished = true;
        }
        private float _nowT = 0f;
        private float _amount = 1f;
        private float _amount2 = 1f;//总份数
        private Vector2 _fromCenter;
        private Vector2 _toCenter;
        private bool reset = true;
        private Func<bool> canFinish;
        public void Reset()
        {
            if(reset) 
            {
                RestrictCameraModifier RCM = new RestrictCameraModifier(_toCenter, Main.LocalPlayer.Center, _amount,() => true, "Rock_RCM_Reset", false);
                EffectPlayer.CMS.Add(RCM);
            }
        }
    }
}