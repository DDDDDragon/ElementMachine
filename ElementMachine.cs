using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;
using Terraria.Localization;
using Terraria;
using ReLogic.Graphics;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.GameInput; 
using Terraria.UI;
using ElementMachine.Tiles;
using Terraria.ModLoader.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using System;

namespace ElementMachine
{
	public class Background : UIElement
	{
		public Background(Texture2D image)
		{
			this.Image = image;
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this.Image, new Rectangle((int)dimensions.X - 6, (int)dimensions.Y - 6, 551, 90), Color.White);
			base.Draw(spriteBatch);
		}
		public Texture2D Image;
	}
	public class ElementMachine : Mod
	{
		public override void Load()
		{
			LoadElements();
			On.Terraria.Main.GUIChatDrawInner += Main_GUIChatDrawInner;
			On.Terraria.Main.DrawMenu += Main_DrawMenuE;
		}
		public static Dictionary<string, int> ItemtoElements = new Dictionary<string, int>();
		public static Dictionary<int, string> NumtoElements = new Dictionary<int, string>();
		public void LoadElements()
		{
			NumtoElements.Add(-1, "None");
			NumtoElements.Add(1, "Flame");
			NumtoElements.Add(2, "Ice");
			NumtoElements.Add(3, "Earth");
			NumtoElements.Add(4, "Water");
			NumtoElements.Add(5, "Nature");
			ItemtoElements.Add("Frozen", 2);
			ItemtoElements.Add("Desert", 3);
			ItemtoElements.Add("DesertAttacker", 1);
			ItemtoElements.Add("Ice", 2);
			ItemtoElements.Add("StingerChakram", 5);
			ItemtoElements.Add("VineChakram", 5);
		}
		public static int GetElement(string ItemName)
		{
			int check = 0;
			int ret = -1;
			foreach(var key in ItemtoElements.Keys)
			{
				if(ItemName.Contains(key)) 
				{
					if(check < key.Length)
					{
						ret = ItemtoElements[key];
						check = key.Length;
					}
				}
			}
			return ret;
		}
		public static string GetElementName(int ElementNum)
		{
			foreach(var key in NumtoElements.Keys)
			{
				if(ElementNum == key) return NumtoElements[key];
			}
			return "";
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
						if(MyPlayer.AnalyzedItemsName.Count > 0)
						{
							while(nextSlot < MyPlayer.AnalyzedItemsName.Count)
							{
								nextSlot++;
								Main.instance.shop[Main.npcShop].item[nextSlot].SetDefaults(this.ItemType(MyPlayer.AnalyzedItemsName[nextSlot - 1]));
								Main.instance.shop[Main.npcShop].item[nextSlot].value = MyPlayer.AnalyzedItemsValue[nextSlot - 1];
							}
						}
						MouseLeft = false;
					}
				}
			}	
		}		
		private void Main_DrawMenuE(On.Terraria.Main.orig_DrawMenu orig, Terraria.Main self, Microsoft.Xna.Framework.GameTime gameTime)
        {
			FieldInfo uiStateField = Main.MenuUI.GetType().GetField("_history", BindingFlags.NonPublic | BindingFlags.Instance);
            List<UIState> _history = (List<UIState>)uiStateField.GetValue(Main.MenuUI);
            for (int x = 0; x < _history.Count; x++)
            {
                if (_history[x].GetType().FullName == "Terraria.ModLoader.UI.UIMods")
                {
                    FieldInfo elementsField = _history[x].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                    List<UIElement> elements = (List<UIElement>)elementsField.GetValue(_history[x]);
                    FieldInfo uiElementsField = elements[0].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                    List<UIElement> uiElements = (List<UIElement>)uiElementsField.GetValue(elements[0]);
                    FieldInfo myModUIPanelField = uiElements[0].GetType().GetField("Elements", BindingFlags.NonPublic | BindingFlags.Instance);
                    List<UIElement> myModUIPanel = myModUIPanelField.GetValue(uiElements[0]) as List<UIElement>;
                    UIList uiList = (UIList)myModUIPanel[0];
					for (int j = 0; j < uiList._items.Count; j++)
                    {
						if(uiList._items[j].GetType().GetField("_mod", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(uiList._items[j]).ToString() == "ElementMachine")
						{
							object _mod = uiList._items[j].GetType().GetField("_mod", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(uiList._items[j]);
							List<UIElement> myUIElementItem = (List<UIElement>)uiList._items[j].GetType().GetField("Elements", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(uiList._items[j]);
							if (!(myUIElementItem[0] is Background))
							{
								Background background = new Background(ModContent.GetTexture("ElementMachine/UI/ModBackGround"));
								uiList._items[j].RemoveAllChildren();
								uiList._items[j].Append(background);
								uiList._items[j].GetType().GetField("_modIconAdjust", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(uiList._items[j], 0);
								uiList._items[j].OnInitialize();
								
								for (int i = 0; i < myUIElementItem.Count; i++)
								{
									if (myUIElementItem[i].ToString() == "Terraria.ModLoader.UI.UIModStateText")
									{
										
									}
								}
							}
							myModUIPanel[0] = uiList;
                    		myModUIPanelField.SetValue(uiElements[0], myModUIPanel);
                    		uiElementsField.SetValue(elements[0], uiElements);
                    		elementsField.SetValue(_history[x], elements);
                   			uiStateField.SetValue(Main.MenuUI, _history);
							
							break;
						}
					}
					break;
                }
			}
			orig(self, gameTime);
		}
		public override void Unload()
		{
			On.Terraria.Main.DrawMenu -= Main_DrawMenuE;
			On.Terraria.Main.GUIChatDrawInner -= Main_GUIChatDrawInner;
			base.Unload();
		}
	}
}