using Terraria.Localization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ElementMachine.NPCs
{
    public class GlobalNPCs : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            if(npc.type == NPCID.Guide)
            {
                if(!Main.LocalPlayer.GetModPlayer<MyPlayer>().FirstTalk)
                {
                    switch(Main.LocalPlayer.GetModPlayer<MyPlayer>().TalkIndex)
                    {
                        case 0 :
                            chat = GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "你醒了？你之前从天上掉了下来——你是从泰拉大陆来的吧？(按对话键继续)" 
                                : "You awake? You fell from the sky before——you come from the Terraria?(Press the Talk button to continue)";
                            break;
                        case 1:
                            chat = GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "我怎么知道的？你们用望远镜只能看到械元域的底部，以为我们是星星也正常，但是我们看你们的生活可真是一清二楚。(按对话键继续)"
                                : "How do I know that? You Terrarian can just see the bottom of ElementMachineLand, so you think it's a star,but we can see your lives so clear.(Press the Talk button to continue)";
                            break;
                        case 2:
                            chat = GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "你的装备应该已经被能量漩涡分解了，那我就给你点基础工具吧，还有这个——元素捕捉器！你可以用它来捕捉小型元素生物，当然也不是无偿的，你要帮我调查一些东西，具体内容我会之后告诉你(按对话键继续)"
                                : "Your equipments must be decompose by the energy stream, then let me give you some basic tools,and this——ElementCatcher! You can use this to catch small Element creatures, of course it's not free, you should do some search for me and I will tell you then.(Press the Talk button to continue)";
                            break;
                        case 3:
                            chat = GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "元素生物是什么？哦我想起来了，你们泰拉大陆是没有这些玩意的。在械元域，很多元素不再是抽象的概念，而是化成了生物存在于世上，比如这片草原时常会出现的小火灵就是最基本的一种元素生物。(按对话键继续)"
                                : "What is Element creatures? Oh I remember, there're no that things on your Terrarian. On our ElementMachineLand, many Elements are not abstract things, they turned into creatures to live in this world, such as LittleFireElf appears on the grassland sometimes(Press the Talk button to continue)";
                            break;
                        case 4:
                            chat = GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "好了，更多的以后再说吧，你先去帮我抓3只小火灵吧！"
                                : "OK, please catch 3 LittleFireElf for me first！";
                            Item.NewItem(null, Main.LocalPlayer.Center, ItemID.CopperAxe);
                            Item.NewItem(null, Main.LocalPlayer.Center, ItemID.CopperShortsword);
                            Item.NewItem(null, Main.LocalPlayer.Center, ItemID.CopperPickaxe);
                            break;
                    }
                    Main.LocalPlayer.GetModPlayer<MyPlayer>().TalkIndex++;
                    return;
                }
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