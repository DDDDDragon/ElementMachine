using Terraria;
using Terraria.ModLoader;

namespace ElementMachine.NPCs.ElementCreatures
{
    public abstract class ElementCreaturesBase : ModNPC
    {
        public int Level;
        /// <summary>
        /// 0-Flame 1-Ice 
        /// </summary>
        public int Element;
        public virtual void OnCatch()
        {
            NPC.life = 0;
        }
        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            OnHitByIce(player, item, damage, knockback, crit);
            base.OnHitByItem(player, item, damage, knockback, crit);
        }
        public virtual void OnHitByIce(Player player, Item item, int damage, float knockback, bool crit)
        {

        }
    }
}
