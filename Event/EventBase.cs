using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader.IO;
using ElementMachine.Bases.ToolObject;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using ElementMachine.Bases.Support;

namespace ElementMachine.Event
{
    public abstract class EventBase : ModType
    {
        private static readonly Dictionary<string, EventBase> Events = new();
        private static readonly AssetEntrust<Texture2D> EventIcons = new();
        private static EventBase Event;
        private static int Progress;
        private static int ProgressMax;
        private static int ProgressDirection;
        private static float ProgressAlpha;
        private static int Timer;

        public int Index;
        public int Wave;
        public virtual bool Activing { get; set; }
        public virtual string EventTexture => (GetType().Namespace + "/" + Name).Replace(".", "/") + ".png";

        public abstract void ModifyInvasionProgress(ref string text, ref Color color);
        public abstract void ReportProgress(out int value, out int maxvalue);
        public virtual void Update() { }
        public virtual void ModifyNPCSpawn(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo) { }
        public virtual void ModifyNPCSpawnRate(Player player, ref int spawnrate, ref int maxspawn) { }
        public virtual void ModifyNPCSpawnRange(Player player, ref int spawnrangex, ref int spawnrangey, ref int saferangex, ref int saferangey) { }
        public virtual void OnStart() { }
        public virtual void OnEnd() { }
        public virtual void LoadData(TagCompound tagcompound, bool fromplayer) { }
        public virtual void SaveData(TagCompound tagcompound, bool forplayer) { }

        protected sealed override void Register()
        {
            ModTypeLookup<EventBase>.Register(this);
            Events.Add(FullName, this);
            Index = Events.Count;
            if(!Main.dedServ) EventIcons.Register(EventTexture, FullName);
        }
        public override void Unload()
        {
            Events.Clear();
            EventIcons.Clear(true);
        }

        public static List<EventBase> GetActivingEvents() => (from EventBase e in Events.Values where e.Activing select e).ToList();
        public static void UpdateAndPrepareDraw()
        {
            if (Event is not null)
            {
                if (Event.Activing)
                {
                    Event.Update();
                }
                DrawHelper.AddDrawTask(new DrawHelper.DrawTask()
                {
                    FindIndex = FindIndex,
                    Draw = Draw,
                    Name = nameof(EventBase),
                    Mode = 1
                });
            }
            else
            {
                EventBase nextevent = Events.Values.FirstOrDefault(e => e.Activing, null);
                if (nextevent is not null)
                {
                    Event = nextevent;
                    Progress = 0;
                    ProgressAlpha = 0;
                    ProgressDirection = 1;
                    Timer = 160;
                    DrawHelper.AddDrawTask(new DrawHelper.DrawTask()
                    {
                        FindIndex = FindIndex,
                        Draw = Draw,
                        Name = nameof(EventBase),
                        Mode = 1
                    });
                }
            }
            Events.Values.ForEach(e =>
            {
                if (e != Event && e.Activing)
                {
                    e.Update();
                }
            });
        }
        public static void StartEvent<T>() where T : EventBase
        {
            StartEvent(ModContent.GetInstance<T>().FullName);
        }
        public static void StartEvent(string fullname)
        {
            if (Events.TryGetValue(fullname, out var e))
            {
                Event?.OnEnd();
                Event = e;
                e.Activing = true;
                Event.OnStart();
                Progress = 0;
                ProgressAlpha = 0;
                ProgressDirection = 1;
                Timer = 160;
            }
        }
        private static int FindIndex(List<Terraria.UI.GameInterfaceLayer> layers)
        {
            return layers.FindIndex(layer => layer.Name == "Vanilla: Invasion Progress Bars");
        }
        private static void Draw()
        {
            if (Event.Activing)
            {
                Timer = 160;
            }
            if (!Main.gamePaused && Timer > 0)
            {
                Timer--;
            }
            if (Timer > 0)
            {
                ProgressAlpha += 0.05f;
            }
            else
            {
                ProgressAlpha -= 0.05f;
            }
            if (ProgressAlpha > 1)
            {
                ProgressAlpha = 1;
            }
            if (ProgressAlpha < 0)
            {
                ProgressAlpha = 0;
            }
            if (ProgressAlpha <= 0f)
            {
                Event = null;
                return;
            }
            float num = 0.5f + ProgressAlpha * 0.5f;
            string text = "";
            Color c = Color.White;
            Texture2D value = EventIcons[Event.FullName];
            Event.ModifyInvasionProgress(ref text, ref c);
            Event.ReportProgress(out Progress, out ProgressMax);
            Vector2 texturescale = new(25.6f / value.Width, 25.6f / value.Height);
            texturescale *= num;

            if (Event.Wave > 0)
            {
                int num2 = (int)(200f * num);
                int num3 = (int)(45f * num);
                Vector2 vector = new(Main.screenWidth - 120, Main.screenHeight - 40);
                Rectangle r4 = new((int)vector.X - num2 / 2, (int)vector.Y - num3 / 2, num2, num3);
                Utils.DrawInvBG(Main.spriteBatch, r4, new Color(63, 65, 151, 255) * 0.785f);
                string key = "Game.WaveMessage";
                object arg = (ProgressMax != 0) ? ((float)Progress / ProgressMax).ToString("##.##%") : Language.GetTextValue("Game.InvasionPoints", Progress);
                string text2 = Language.GetTextValue(key, Event.Wave, arg);
                Texture2D value2 = TextureAssets.ColorBar.Value;
                float num4 = MathHelper.Clamp(Progress / (float)ProgressMax, 0f, 1f);
                if (ProgressMax == 0)
                {
                    num4 = 1f;
                }
                float num5 = 169f * num;
                float num6 = 8f * num;
                Vector2 vector2 = vector + Vector2.UnitY * num6 + Vector2.UnitX * 1f;
                Utils.DrawBorderString(Main.spriteBatch, text2, vector2, Color.White * ProgressAlpha, num, 0.5f, 1f, -1);
                Main.spriteBatch.Draw(value2, vector, null, Color.White * ProgressAlpha, 0f, new Vector2(value2.Width / 2, 0f), num, SpriteEffects.None, 0f);
                vector2 += Vector2.UnitX * (num4 - 0.5f) * num5;
                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, vector2, new Rectangle?(new Rectangle(0, 0, 1, 1)), new Color(255, 241, 51) * ProgressAlpha, 0f, new Vector2(1f, 0.5f), new Vector2(num5 * num4, num6), SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, vector2, new Rectangle?(new Rectangle(0, 0, 1, 1)), new Color(255, 165, 0, 127) * ProgressAlpha, 0f, new Vector2(1f, 0.5f), new Vector2(2f, num6), SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, vector2, new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Black * ProgressAlpha, 0f, new Vector2(0f, 0.5f), new Vector2(num5 * (1f - num4), num6), SpriteEffects.None, 0f);
            }
            else
            {
                int num7 = (int)(200f * num);
                int num8 = (int)(45f * num);
                Vector2 vector3 = new(Main.screenWidth - 120, Main.screenHeight - 40);
                Rectangle r4 = new((int)vector3.X - num7 / 2, (int)vector3.Y - num8 / 2, num7, num8);
                Utils.DrawInvBG(Main.spriteBatch, r4, new Color(63, 65, 151, 255) * 0.785f);
                string text3 = (ProgressMax != 0) ? ((float)Progress / ProgressMax).ToString("##.##%") : Progress.ToString();
                text3 = Language.GetTextValue("Game.WaveCleared", text3);
                Texture2D value3 = TextureAssets.ColorBar.Value;
                if (ProgressMax != 0)
                {
                    Main.spriteBatch.Draw(value3, vector3, null, Color.White * ProgressAlpha, 0f, new Vector2((float)(value3.Width / 2), 0f), num, SpriteEffects.None, 0f);
                    float num9 = MathHelper.Clamp(Progress / (float)ProgressMax, 0f, 1f);
                    Vector2 vector4 = FontAssets.MouseText.Value.MeasureString(text3);
                    float num10 = num;
                    if (vector4.Y > 22f)
                    {
                        num10 *= 22f / vector4.Y;
                    }
                    float num11 = 169f * num;
                    float num12 = 8f * num;
                    Vector2 vector5 = vector3 + Vector2.UnitY * num12 + Vector2.UnitX * 1f;
                    Utils.DrawBorderString(Main.spriteBatch, text3, vector5 + new Vector2(0f, -4f), Color.White * ProgressAlpha, num10, 0.5f, 1f, -1);
                    vector5 += Vector2.UnitX * (num9 - 0.5f) * num11;
                    Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, vector5, new Rectangle?(new Rectangle(0, 0, 1, 1)), new Color(255, 241, 51) * ProgressAlpha, 0f, new Vector2(1f, 0.5f), new Vector2(num11 * num9, num12), SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, vector5, new Rectangle?(new Rectangle(0, 0, 1, 1)), new Color(255, 165, 0, 127) * ProgressAlpha, 0f, new Vector2(1f, 0.5f), new Vector2(2f, num12), SpriteEffects.None, 0f);
                    Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, vector5, new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Black * ProgressAlpha, 0f, new Vector2(0f, 0.5f), new Vector2(num11 * (1f - num9), num12), SpriteEffects.None, 0f);
                }
            }
            Vector2 value4 = FontAssets.MouseText.Value.MeasureString(text);
            float num13 = 120f;
            if (value4.X > 200f)
            {
                num13 += value4.X - 200f;
            }
            Rectangle r3 = Utils.CenteredRectangle(new Vector2(Main.screenWidth - num13, Main.screenHeight - 80), (value4 + new Vector2((float)(value.Width + 12), 6f)) * num);
            Utils.DrawInvBG(Main.spriteBatch, r3, c);
            Main.spriteBatch.Draw(value, r3.Left() + Vector2.UnitX * num * 8f, null, Color.White * ProgressAlpha, 0f, new Vector2(0f, value.Height / 2), texturescale, SpriteEffects.None, 0f);
            Utils.DrawBorderString(Main.spriteBatch, text, r3.Right() + Vector2.UnitX * num * -22f, Color.White * ProgressAlpha, num * 0.9f, 1f, 0.4f, -1);
        }
        public static void Clear()
        {
            Event = null;
            Progress = 0;
            ProgressMax = 0;
            ProgressAlpha = 0;
            Timer = 0;
            Events.Values.ForEach(e =>
            {
                e.OnEnd();
                e.Activing = false;
            });
        }
        public static void Save(TagCompound tag, bool forplayer)
        {
            TagCompound mytag = new();
            TagCompound subtag;
            Events.Values.ForEach(e =>
            {
                subtag = new();
                e.SaveData(subtag, forplayer);
                mytag.Set(e.FullName, subtag);
            });
            tag.Set(nameof(EventBase), mytag);
        }
        public static void Load(TagCompound tag, bool fromplayer)
        {
            if (tag.TryGet(nameof(EventBase), out TagCompound mytag))
            {
                Events.Keys.ForEach(k =>
                {
                    if (mytag.TryGet(k, out TagCompound subtag))
                    {
                        Events[k].LoadData(subtag, fromplayer);
                    }
                });
            }
        }
    }
    public class EventNPCs : GlobalNPC
    {
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            var list = EventBase.GetActivingEvents();
            if (list.Any())
            {
                var @event = list[0];
                @event.ModifyNPCSpawn(pool, spawnInfo);
            }
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            var list = EventBase.GetActivingEvents();
            if (list.Any())
            {
                var @event = list[0];
                @event.ModifyNPCSpawnRate(player, ref spawnRate, ref maxSpawns);
            }
        }
        public override void EditSpawnRange(Player player, ref int spawnRangeX, ref int spawnRangeY, ref int safeRangeX, ref int safeRangeY)
        {
            var list = EventBase.GetActivingEvents();
            if (list.Any())
            {
                var @event = list[0];
                @event.ModifyNPCSpawnRange(player, ref spawnRangeX, ref spawnRangeY, ref safeRangeX, ref safeRangeY);
            }
        }
    }
}
