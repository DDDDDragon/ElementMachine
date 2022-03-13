using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;
using Terraria.Localization;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework.Graphics;
using ElementMachine.UI;
using System;
using System.Linq;
using ElementMachine.Oblation;
using Terraria.ModLoader.IO;
using System.Reflection;

namespace ElementMachine
{
    public enum ElementsType
    {
        None = -1,
        Flame,
        Ice,
        Earth,
        Water,
        Nature
    }
    public abstract class ElementItem : ModItem
    {
        /// <summary>
        /// 物品的元素,为一个list
        /// </summary>
        public List<ElementsType> elementsType = Enum.GetValues(typeof(ElementsType)).Cast<ElementsType>().ToList();
        public int Element = 0;
        public virtual void PreAddElement(){}
        public virtual void PostAddElement(){}
        public virtual void SetElement(){}
        public void AddElement(ElementsType element)
		{
            PreAddElement();
            PostAddElement();
		}
        public override void PostDrawTooltipLine(DrawableTooltipLine line)
        {
            base.PostDrawTooltipLine(line);
        }
        public override void SetStaticDefaults()
        {
            SetElement();
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            SetElement();
            base.SetDefaults();
        }
    }
    public class ElementGlobalItem : GlobalItem
    {
        private void InsertElementalTooltip(List<TooltipLine> tooltips, int index, bool none = true)
		{
			TooltipLine tooltipLine = new TooltipLine(base.mod, "EAM:Elemental", GameCulture.Chinese.IsActive ? "元素属性: " : "Element:");
			if(none)
			{
				tooltipLine.text += (GameCulture.Chinese.IsActive ? "无" : "None");
			}
			tooltips.Insert(index, tooltipLine);
		}
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if(item.modItem is ElementItem || item.modItem is OblationCore)
            {
                int num = tooltips.FindIndex((TooltipLine t) => t.Name.Equals("Damage"));
                if(num != -1) 
                {
                    if(ElementMachine.GetElement(item.modItem.Name) == -1) this.InsertElementalTooltip(tooltips, num + 1);
                    else this.InsertElementalTooltip(tooltips, num + 1, false);
                }
                else if((item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1) && !item.vanity)
                {
                    int num2 = tooltips.FindIndex((TooltipLine t) => t.Name.Equals("Defense"));
                    if(num2 != -1) 
                    {
                        if(ElementMachine.GetElement(item.modItem.Name) == -1) this.InsertElementalTooltip(tooltips, num2 + 1);
                        else this.InsertElementalTooltip(tooltips, num2 + 1, false);
                    }
                }
                else
                {
                    int num3 = tooltips.FindIndex((TooltipLine t) => t.Name.Equals("ItemName"));
                    if(num3 != -1)
                    {
                        if(ElementMachine.GetElement(item.modItem.Name) == -1) this.InsertElementalTooltip(tooltips, num3 + 1);
                        else this.InsertElementalTooltip(tooltips, num3 + 1, false);
                    }
                }
            }
            base.ModifyTooltips(item, tooltips);
        }
        
        public override bool PreDrawTooltip(Item item, ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y)
        {
            Texture2D texture = ModContent.GetTexture("ElementMachine/UI/TooltipBackground");
            int num = 0;
			int num2 = 0;
			foreach (TooltipLine tooltipLine in lines)
			{
				if (tooltipLine.Name.Equals("EAM:Elemental"))
				{
					num = Math.Max(num, (int)ChatManager.GetStringSize(Main.fontMouseText, tooltipLine.text, new Vector2(1f, 1f), -1f).X + 30 + 15);
				}
				foreach (string text in tooltipLine.text.Split(new char[]
				{
					'\n'
				}))
				{
					num = Math.Max(num, (int)ChatManager.GetStringSize(Main.fontMouseText, text, new Vector2(1f, 1f), -1f).X);
					num2 += (int)Main.fontMouseText.MeasureString("中").Y;
				}
			}
			if (x + num + 12 > Main.screenWidth)
			{
				x = Main.screenWidth - num - 16;
			}
			if (y + num2 + 12 > Main.screenHeight)
			{
				y = Main.screenHeight - num2 - 16;
			}
            Drawing.DrawAdvBox(Main.spriteBatch, new Rectangle(x - 14, y - 12, num + 28, num2 + 16), Color.White * 0.5f, texture, new Vector2(12f, 12f));
            return true;
        }
        public override void PostDrawTooltipLine(Item item, DrawableTooltipLine line)
        {
            if (line.Name.Equals("EAM:Elemental"))
			{
				Vector2 position = new Vector2((float)(line.X + (int)ChatManager.GetStringSize(Main.fontMouseText, line.text, new Vector2(1f, 1f), -1f).X), line.Y);
                string num = "";
                if(ElementMachine.GetElement(item.modItem.Name) != -1)
                {
                    num = ElementMachine.GetElementName(ElementMachine.GetElement(item.modItem.Name));
                    Main.spriteBatch.Draw(ModContent.GetTexture($"ElementMachine/Element/{num}Icon"), position, Color.White);
                    
                }
                
                position.X += 30;
			}
            base.PostDrawTooltipLine(item, line);
        }
    }
}