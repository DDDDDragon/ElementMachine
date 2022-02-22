using System;

namespace ElementMachine.Effect.CameraModifiers
{
    public interface ICameraModifier
    {
        string UniqueIdentity {get; }
        void Update(ref CameraInfo cameraPosition);
        bool Finished { get; }
    }
}