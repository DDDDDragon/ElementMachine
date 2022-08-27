using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using ElementMachine.NPCs.ElementCreatures;
using ElementMachine.Element;

namespace ElementMachine.Tasks
{
    public class CatchFireElfTask : TaskBase
    {
        public override void LoadConv()
        {
            Name = "捕捉: 小火灵";
            TransName = " Catch: LittleFireElf";
            AddConv("你醒了？你之前从天上掉了下来——你是从泰拉大陆来的吧？(按对话键继续)", "You awake? You fell from the sky before——you come from the Terraria?(Press the Talk button to continue)");
            AddConv("我怎么知道的？你们用望远镜只能看到械元域的底部，以为我们是星星也正常，但是我们看你们的生活可真是一清二楚。(按对话键继续)", "How do I know that? You Terrarian can just see the bottom of ElementMachineLand, so you think it's a star,but we can see your lives so clear.(Press the Talk button to continue)");
            AddConv("你的装备应该已经被能量漩涡分解了，那我就给你点基础工具吧，还有这个——元素捕手！你可以用它来捕捉小型元素生物，当然也不是无偿的，你要帮我调查一些东西，具体内容我会之后告诉你(按对话键继续)", "Your equipments must be decompose by the energy stream, then let me give you some basic tools,and this——ElementCatcher! You can use this to catch small Element creatures, of course it's not free, you should do some search for me and I will tell you then.(Press the Talk button to continue)");
            AddConv("元素生物是什么？哦我想起来了，你们泰拉大陆是没有这些玩意的。在械元域，很多元素不再是抽象的概念，而是化成了生物存在于世上，比如这片草原时常会出现的小火灵就是最基本的一种元素生物。(按对话键继续)", "What is Element creatures? Oh I remember, there're no that things on your Terrarian. On our ElementMachineLand, many Elements are not abstract things, they turned into creatures to live in this world, such as LittleFireElf appears on the grassland sometimes(Press the Talk button to continue)");
            AddConv("好了，更多的以后再说吧，你先去帮我抓3只小火灵吧！", "OK, please catch 3 LittleFireElf for me first！");
            DescriptionIndex = Conv.Count - 1;
            AddConv("你真的抓到了三只小火灵！蟹蟹你，不过研究完了我留着它也没有用，还是送你吧！", "You really catch 3 LittleFireElf! Thanks but it's useless for me after I research it, so I'll give it to you!");
            AddEvent(Reward);
            NPC = NPCID.Guide;
            base.LoadConv();
        }

        public void Reward(int index)
        {
            if (index == 2 && !reward)
            {
                Item.NewItem(null, Main.LocalPlayer.Center, ModContent.ItemType<ElementCatcher>());
                reward = true;
            }
        }
        public bool reward = false;
        public override bool IsComplete()
        {
             int stack = 0;
             foreach(var i in Main.LocalPlayer.inventory)
             {
                 if (i.ModItem != null && i.ModItem.Type == ModContent.ItemType<LittleFireElfItem>()) stack += i.stack;
             }
            if (stack >= 3) return true;
            else return false;
        }
        public override void OnComplete()
        {
            
            base.OnComplete();
        }
    }
}
