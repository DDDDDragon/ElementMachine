using Terraria;
using Terraria.ModLoader;
using ElementMachine.Effect.CameraModifiers;

namespace ElementMachine.Effect
{

    public class EffectPlayer : ModPlayer
    {
        public static CameraModifierStack CMS = new CameraModifierStack();
        public override void ModifyScreenPosition()
        {
            base.ModifyScreenPosition();
            CMS.ApplyTo(ref Main.screenPosition);
        }
        public override void OnEnterWorld(Player player)
        {
            base.OnEnterWorld(player);
            CMS.Clear();
        }
    }
}