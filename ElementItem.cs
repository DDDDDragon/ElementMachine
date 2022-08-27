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
using Terraria.GameContent;

namespace ElementMachine
{
    public abstract class ElementItem : ModItem
    {
        public float ElementLevel;
        /// <summary>
        /// 1-Flame 2-Ice 3-Earth 4- Water 5-Nature
        /// </summary>
        public int Element;
        public override void PostDrawTooltipLine(DrawableTooltipLine line)
        {
            base.PostDrawTooltipLine(line);
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }
    }
    public abstract class ElementProj : ModProjectile
    {
        public float ElementLevel;
        /// <summary>
        /// 1-Flame 2-Ice 3-Earth 4- Water 5-Nature
        /// </summary>
        public int Element = -1;
    }
    public class ElementGlobalItem : GlobalItem
    {
        private void InsertElementalTooltip(List<TooltipLine> tooltips, int index, Item item, bool none = true)
		{
			TooltipLine tooltipLine = new TooltipLine(base.Mod, "EAM:Elemental", GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "元素属性: " : "Element:");
            TooltipLine tooltipLine2 = new TooltipLine(base.Mod, "EAM:Derivation", GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "派生: " : "Derivation:");
			if(none)
			{
				tooltipLine.Text += (GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "无" : "None");
			}
            if(ElementMachine.GetDerivation(item.ModItem.Name) != -1)
            {
                tooltipLine2.Text += ElementMachine.GetDerivationName(ElementMachine.GetDerivation(item.ModItem.Name));
            }
            else tooltipLine2.Text += (GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "无" : "None");
			tooltips.Insert(index, tooltipLine);
            if(item.damage > 0) tooltips.Insert(index, tooltipLine2);
		}
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            ElementItem EItem = item.ModItem as ElementItem;
            if(item.ModItem is ElementItem || item.ModItem is OblationCore)
            {
                int num = tooltips.FindIndex((TooltipLine t) => t.Name.Equals("Damage"));
                if(num != -1) 
                {
                    if(EItem.Element == -1) this.InsertElementalTooltip(tooltips, num + 1, item);
                    else this.InsertElementalTooltip(tooltips, num + 1, item, false);
                }
                else if((item.headSlot != -1 || item.bodySlot != -1 || item.legSlot != -1) && !item.vanity)
                {
                    int num2 = tooltips.FindIndex((TooltipLine t) => t.Name.Equals("Defense"));
                    if(num2 != -1) 
                    {
                        if(EItem.Element == -1) this.InsertElementalTooltip(tooltips, num2 + 1, item);
                        else this.InsertElementalTooltip(tooltips, num2 + 1, item, false);
                    }
                }
                else
                {
                    int num3 = tooltips.FindIndex((TooltipLine t) => t.Name.Equals("ItemName"));
                    if(num3 != -1)
                    {
                        if(EItem.Element == -1) this.InsertElementalTooltip(tooltips, num3 + 1, item);
                        else this.InsertElementalTooltip(tooltips, num3 + 1, item, false);
                    }
                }
            }
            base.ModifyTooltips(item, tooltips);
        }
        
        public override bool PreDrawTooltip(Item item, ReadOnlyCollection<TooltipLine> lines, ref int x, ref int y)
        {
            ElementItem EItem = item.ModItem as ElementItem;
            Texture2D texture = ModContent.Request<Texture2D>("ElementMachine/UI/TooltipBackground").Value;
            int num = 0;
			int num2 = 0;
			foreach (TooltipLine tooltipLine in lines)
			{
				if (tooltipLine.Name.Equals("EAM:Elemental"))
				{
					num = Math.Max(num, (int)ChatManager.GetStringSize(FontAssets.MouseText.Value, tooltipLine.Text, new Vector2(1f, 1f), -1f).X + 30 + 15);
				}
				foreach (string text in tooltipLine.Text.Split(new char[]
				{
					'\n'
				}))
				{
					num = Math.Max(num, (int)ChatManager.GetStringSize(FontAssets.MouseText.Value, text, new Vector2(1f, 1f), -1f).X);
					num2 += (int)FontAssets.MouseText.Value.MeasureString("中").Y;
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
            ElementItem EItem = item.ModItem as ElementItem;
            if (line.Name.Equals("EAM:Elemental"))
			{
				Vector2 position = new Vector2((float)(line.X + (int)ChatManager.GetStringSize(FontAssets.MouseText.Value, line.Text, new Vector2(1f, 1f), -1f).X), line.Y);
                string num = "";
                if(EItem.Element != -1)
                {
                    num = ElementMachine.GetElementName(EItem.Element);
                    Main.spriteBatch.Draw(ModContent.Request<Texture2D>($"ElementMachine/Element/{num}Icon").Value, position, Color.White);
                    
                }
                
                position.X += 30;
			}
            base.PostDrawTooltipLine(item, line);
        }
    }
}