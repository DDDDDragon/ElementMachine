using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;
using Terraria.Localization;
using Terraria;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.GameInput; 
using ElementMachine.Tiles;
using System.Reflection;

namespace ElementMachine
{
	public class ElementMachine : Mod
	{
		public override void Load()
		{
			On.Terraria.Main.GUIChatDrawInner += Main_GUIChatDrawInner;
		}
		public bool npcChatFocus4 = false;
		public bool MouseLeft = false;
        private void Main_GUIChatDrawInner(On.Terraria.Main.orig_GUIChatDrawInner orig, Terraria.Main self)
        {
            orig(self);
			if(Main.npc[Main.player[Main.myPlayer].talkNPC].type == NPCID.Guide)
			{
				List<List<TextSnippet>> list = Utils.WordwrapStringSmart(Main.npcChatText, Color.White, Main.fontMouseText, 460, 10);
				int count = list.Count;
				Vector2 mouse = new Vector2((float)Main.mouseX, (float)Main.mouseY);
				float y = (float)(130 + count * 30);
				int num = 180 + (Main.screenWidth - 800) / 2;
				DynamicSpriteFont font = Main.fontMouseText;
				string focusText = Language.GetTextValue("LegacyInterface.28");
				int num2 = (int)Main.mouseTextColor;
				Color chatColor = new Color(num2, (int)((double)num2 / 1.1), num2 / 2, num2);

				Player player = Main.player[Main.myPlayer];
	  			Vector2 vector = new Vector2((float)num, y);
				Vector2 vector2 = vector;
				Vector2 vector3 = new Vector2(0.9f);
				Vector2 stringSize = ChatManager.GetStringSize(font, Language.GetTextValue("LegacyInterface.51"), vector3, -1f);
				Color baseColor = chatColor;
				Vector2 vector4 = new Vector2(1f);
				if (stringSize.X > 260f)
				{
					vector4.X *= 260f / stringSize.X;
				}
				Vector2 vector5 = new Vector2((float)num + stringSize.X * vector4.X + 30f, y);
				vector3 = new Vector2(0.9f);
				stringSize = ChatManager.GetStringSize(font, Language.GetTextValue("LegacyInterface.52"), vector3, -1f);
				vector4 = new Vector2(1f);
				Vector2 vector6 = new Vector2(vector5.X + stringSize.X * vector4.X + 30f, y);
				stringSize = ChatManager.GetStringSize(font, Language.GetTextValue("LegacyInterface.25"), vector3, -1f);
				Vector2 vector7 = new Vector2(vector6.X + stringSize.X * vector4.X + 30f, y);
				stringSize = ChatManager.GetStringSize(font, focusText, vector3, -1f);
				if(mouse.Between(vector7, vector7 + stringSize * vector3 * vector4.X) && ! PlayerInput.IgnoreMouseInterface)
				{
					player.mouseInterface = true;
					player.releaseUseItem = false;
					vector3 *= 1.1f;
					if(!npcChatFocus4)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					npcChatFocus4 = true;
				}
				else
				{
					if(npcChatFocus4)
					{
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					npcChatFocus4 = false;
				}
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, focusText, vector7 + stringSize * vector4 * 0.5f, chatColor, 0f, stringSize * 0.5f, vector3 * vector4, -1f, 2f);
				if(mouse.Between(vector7, vector7 + stringSize * vector3 * vector4.X) && ! PlayerInput.IgnoreMouseInterface)
				{
					if(Main.mouseLeft) MouseLeft = true;
					if(MouseLeft && !Main.mouseLeft) 
					{
						Main.playerInventory = true;
						Main.npcChatText = "";
						Main.npcShop = Main.MaxShopIDs - 1;
						int nextSlot = 0;
						for(int i = 0; i < 40; i++)
						{
							Main.instance.shop[Main.npcShop].item[i].SetDefaults(ItemID.None);
							Main.instance.shop[Main.npcShop].item[nextSlot].value = 0;
						}
						
						Main.instance.shop[Main.npcShop].item[nextSlot].SetDefaults(ItemID.Chest);
						Main.instance.shop[Main.npcShop].item[nextSlot].value = 1000;
						if(MyPlayer.AnalyzedItemsType.Count > 0)
						{
							while(nextSlot < MyPlayer.AnalyzedItemsType.Count)
							{
								nextSlot++;
								Main.instance.shop[Main.npcShop].item[nextSlot].SetDefaults(MyPlayer.AnalyzedItemsType[nextSlot - 1]);
								Main.instance.shop[Main.npcShop].item[nextSlot].value = MyPlayer.AnalyzedItemsValue[nextSlot - 1];
							}
						}
						MouseLeft = false;
					}
				}
			}	
		}		public override void Unload()
		{

			On.Terraria.Main.GUIChatDrawInner -= Main_GUIChatDrawInner;
			base.Unload();
		}
	}
}