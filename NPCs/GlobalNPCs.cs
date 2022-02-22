using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ElementMachine.NPCs
{
    public class GlobalNPCs : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            if(npc.type == NPCID.Guide)
            {
                WeightedRandom<string> random = new WeightedRandom<string>();
                if(GameCulture.Chinese.IsActive)
                {
                    random.Add("我不是那个泰拉大陆上的向导! 虽然长得有些像就是了");
                    random.Add("你问我这是哪? 这是械元域, 泰拉大陆之外的一个大型的自成一体的空岛");
                }
                else
                {
                    random.Add("I'm not that Guide in Terraria! Even if we are similar");
                    random.Add("You want to know where's here? Here is ElementMachineLand, an independent sky island away from the Terraria");
                }
                chat = random;
            }
            else base.GetChat(npc, ref chat);
        }
        public override void OnChatButtonClicked(NPC npc, bool firstButton)
        {
            base.OnChatButtonClicked(npc, firstButton);
        }
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            base.SetupShop(type, shop, ref nextSlot);
        }
    }
}