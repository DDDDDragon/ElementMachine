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
using ElementMachine.NPCs.Boss.SandDiablos;
using ElementMachine.NPCs.BossItems.SandDiablos;
using ElementMachine.Oblation;
using Terraria.GameContent;
using Terraria.Audio;
using ElementMachine.Tasks;
using System.Linq;

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
		public override void PostSetupContent()
		{
			Mod bossCheckList;
			if(ModLoader.HasMod("BossCheckList"))
			{
				bossCheckList = ModLoader.GetMod("BossCheckList");
				bossCheckList.Call(new object[]
				{
					"AddBoss",
					.75f,
					new List<int>
					{
						ModContent.NPCType<SandDiablos>()
					},
					this,
					GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "砂角魔灵" : "SandDiablos",
					new Func<bool>(() => MyWorld.SandDiablos),
					ModContent.ItemType<SandHorn>(),
					new List<int>(){
					},
					new List<int>(){
						ModContent.ItemType<SandDiablosHorn>(),
						ModContent.ItemType<SandDiablosCarapace>(),
						ModContent.ItemType<SandDiablosCore>()
					},
					GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "在沙漠使用[i:" + ModContent.ItemType<SandHorn>() + "]以召唤砂角魔灵" : "Use[i:" + ModContent.ItemType<SandHorn>() + "]in desert to summon it",
					this,
					ModContent.Request<Texture2D>("ElementMachine/NPCs/Boss/SandDiablos/SandDiablosAll"),
					this
				});
			}
		}
		public override void Load()
		{
			LoadElements();
			LoadDerivations();
			Instance = this;
			On.Terraria.Main.GUIChatDrawInner += Main_GUIChatDrawInner;
			On.Terraria.Main.DrawMenu += Main_DrawMenuE;
			TaskManager.Load();
        }
		public static ElementMachine Instance;
		public static Dictionary<int, string> NumtoElements = new Dictionary<int, string>();
		public static Dictionary<string, int> ItemtoDerivations = new Dictionary<string, int>();
		public static Dictionary<int, (string, string)> NumtoDerivations = new Dictionary<int, (string, string)>();
		public void LoadDerivations()
		{
			ItemtoDerivations.Clear();
			NumtoDerivations.Clear();
			NumtoDerivations.Add(1, ("Bone Derivation", "骨制派生"));
			ItemtoDerivations.Add("SandCracker", 1);
		}
		public void LoadElements()
		{
			NumtoElements.Clear();
			NumtoElements.Add(-1, "None");
			NumtoElements.Add(1, "Flame");
			NumtoElements.Add(2, "Ice");
			NumtoElements.Add(3, "Earth");
			NumtoElements.Add(4, "Water");
			NumtoElements.Add(5, "Nature");
		}
		public static int GetDerivation(string ItemName)
		{
			int check = 0;
			int ret = -1;
			foreach(var key in ItemtoDerivations.Keys)
			{
				if(ItemName.Contains(key)) 
				{
					if(check < key.Length)
					{
						ret = ItemtoDerivations[key];
						check = key.Length;
					}
				}
			}
			return ret;
		}
		public static string GetDerivationName(int ElementNum)
		{
			foreach(var key in NumtoDerivations.Keys)
			{
				if(ElementNum == key)
				{
					return GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? NumtoDerivations[key].Item2 : NumtoDerivations[key].Item1;
				}
			}
			return "";
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
		public bool npcChatFocus5 = false;
		public bool MouseLeft = false;
        private void Main_GUIChatDrawInner(On.Terraria.Main.orig_GUIChatDrawInner orig, Terraria.Main self)
        {
            orig(self);
			if(Main.npc[Main.player[Main.myPlayer].talkNPC] != null && Main.npc[Main.player[Main.myPlayer].talkNPC].type == NPCID.Guide)
			{
				List<List<TextSnippet>> list = Utils.WordwrapStringSmart(Main.npcChatText, Color.White, FontAssets.MouseText.Value, 460, 10);
				int count = list.Count;
				Vector2 mouse = new Vector2((float)Main.mouseX, (float)Main.mouseY);
				float y = (float)(130 + count * 30);
				int num = 180 + (Main.screenWidth - 800) / 2;
				DynamicSpriteFont font = FontAssets.MouseText.Value;
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
				Vector2 vector5 = new((float)num + stringSize.X * vector4.X + 30f, y);
				vector3 = new Vector2(0.9f);
				Vector2 vector100 = new Vector2(0.9f);
				stringSize = ChatManager.GetStringSize(font, Language.GetTextValue("LegacyInterface.52"), vector3, -1f);
				vector4 = new Vector2(1f);
				Vector2 vector6 = new(vector5.X + stringSize.X * vector4.X + 30f, y);
				stringSize = ChatManager.GetStringSize(font, Language.GetTextValue("LegacyInterface.25"), vector3, -1f);
				Vector2 vector7 = new(vector6.X + (stringSize.X * vector4.X + 30f) * 2, y);
				stringSize = ChatManager.GetStringSize(font, focusText, vector3, -1f);
				Vector2 vector8 = new(vector7.X + (stringSize.X * vector4.X + 30f), y);
				if (mouse.Between(vector7, vector7 + stringSize * vector3 * vector4.X) && ! PlayerInput.IgnoreMouseInterface)
				{
					player.mouseInterface = true;
					player.releaseUseItem = false;
					vector3 *= 1.1f;
					if(!npcChatFocus4)
					{
						SoundEngine.PlaySound(SoundID.MenuTick);
					}
					npcChatFocus4 = true;
				}
				else
				{
					if(npcChatFocus4)
					{
						SoundEngine.PlaySound(SoundID.MenuTick);
					}
					npcChatFocus4 = false;
				}
				if (mouse.Between(vector8, vector8 + stringSize * vector3 * vector4.X) && !PlayerInput.IgnoreMouseInterface)
				{
					player.mouseInterface = true;
					player.releaseUseItem = false;
					vector100 *= 1.1f;
					if (!npcChatFocus5)
					{
						SoundEngine.PlaySound(SoundID.MenuTick);
					}
					npcChatFocus5 = true;
				}
				else
				{
					if (npcChatFocus5)
					{
						SoundEngine.PlaySound(SoundID.MenuTick);
					}
					npcChatFocus5 = false;
				}
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, focusText, vector7 + stringSize * vector4 * 0.5f, chatColor, 0f, stringSize * 0.5f, vector3 * vector4, -1f, 2f);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, font, GameCulture.FromCultureName(GameCulture.CultureName.Chinese).IsActive ? "对话" : "Talk", vector8 + stringSize * vector4 * 0.5f, chatColor, 0f, stringSize * 0.5f, vector100 * vector4, -1f, 2f);
				if (mouse.Between(vector7, vector7 + stringSize * vector3 * vector4.X) && ! PlayerInput.IgnoreMouseInterface)
				{
					if(Main.mouseLeft) MouseLeft = true;
					if(MouseLeft && !Main.mouseLeft) 
					{
						Main.playerInventory = true;
						Main.npcChatText = "";
						int ShopID = Main.MaxShopIDs - 2;
						Main.SetNPCShopIndex(ShopID);
						int nextSlot = 0;
						for(int i = 0; i < 40; i++)
						{
							Main.instance.shop[ShopID].item[i].SetDefaults(ItemID.None);
							Main.instance.shop[ShopID].item[nextSlot].value = 0;
						}
						
						Main.instance.shop[ShopID].item[nextSlot].SetDefaults(ItemID.Chest);
						Main.instance.shop[ShopID].item[nextSlot].value = 1000;
						if(MyPlayer.AnalyzedItemsName.Count > 0)
						{
							while(nextSlot < MyPlayer.AnalyzedItemsName.Count)
							{
								nextSlot++;
								Main.instance.shop[ShopID].item[nextSlot].SetDefaults(this.Find<ModItem>(MyPlayer.AnalyzedItemsName[nextSlot - 1]).Type);
								Main.instance.shop[ShopID].item[nextSlot].value = MyPlayer.AnalyzedItemsValue[nextSlot - 1];
							}
						}
						MouseLeft = false;
					}
				}
				if (mouse.Between(vector8, vector8 + stringSize * vector100 * vector4.X) && !PlayerInput.IgnoreMouseInterface)
				{
					if (Main.mouseLeft) MouseLeft = true;
					if (MouseLeft && !Main.mouseLeft)
					{
						Main.npcChatText = Main.npc[Main.player[Main.myPlayer].talkNPC].GetChat();
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
								Background background = new Background(ModContent.Request<Texture2D>("ElementMachine/UI/ModBackGround").Value);
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