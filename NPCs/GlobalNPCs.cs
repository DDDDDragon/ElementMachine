using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using ElementMachine.Tasks;
using System.Linq;

namespace ElementMachine.NPCs
{
    public class GlobalNPCs : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            if (Main.LocalPlayer.GetModPlayer<MyPlayer>().NowTasks.Where(t => t.NPC == npc.type).ToList().Count > 0)
            {
                chat = "";
                TaskBase task = Main.LocalPlayer.GetModPlayer<MyPlayer>().NowTasks.FirstOrDefault(t => t.NPC == npc.type);
                int index = Main.LocalPlayer.GetModPlayer<MyPlayer>().NowTasks.FindIndex(0, t => t.NPC == npc.type);
                if (task.ConvIndex != task.Conv.Count)
                {
                    chat = task.NextConv();
                    Main.NewText(task.ConvIndex);
                    Main.NewText(task.Conv.Count);
                    return;
                }
                if (task.ConvIndex == task.Conv.Count)
                {
                    Main.NewText("Remove");
                    Main.LocalPlayer.GetModPlayer<MyPlayer>().NowTasks.RemoveAt(index);
                }
            }
            if (npc.type == NPCID.Guide)
            {
                WeightedRandom<string> random = new WeightedRandom<string>();
                if(GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive)
                {
                    random.Add("我不是那个泰拉大陆上的向导! 虽然长得有些像就是了");
                    random.Add("你问我这是哪? 这是械元域, 泰拉大陆之外的一个大型的自成一体的空岛");
                    random.Add("你知道的, 我是一个机械师, 你可以建造那个叫做合金分析仪的装置然后用它分析一些机械材料, 这样我就可以通过结果把物品给你造出来并且售卖");
                    random.Add("我曾经在一个古老的部落考古时看到过他们用蕴含元素力量的玩意激活了祭坛———见了鬼了,那祭坛的元素能量从哪里来的?他们怎么会通过那玩意变强的?");
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
        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
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