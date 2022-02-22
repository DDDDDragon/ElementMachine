using Terraria;
using Terraria.ModLoader;
using ElementMachine.Effect.CameraModifiers;

namespace ElementMachine.Effect
{
    public class EffectGlobalItem : GlobalItem
    {
        public static bool CanUseItems = true;
        public override bool CanUseItem(Item item, Player player)
        {
            return base.CanUseItem(item, player) && CanUseItems;
        }
        public override void PreUpdateVanitySet(Player player, string set)
        {
            CanUseItems = true;
            base.PreUpdateVanitySet(player, set);
        }
    }
}