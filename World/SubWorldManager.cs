using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.IO;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ElementMachine.World
{
    public class SubWorldSystem : ModSystem
    {
        public static Dictionary<string, SubWorld> subworlds;
        public static bool subworldReseted = false;
        public static SubWorld current;
        public static WorldFileData orig;
        private static bool SetWorldSizeWork;
        private readonly Color[] color = !Main.dedServ ? new Color[Main.instance.mapSectionTexture.Width * Main.instance.mapSectionTexture.Height] : null;
        public override void OnModLoad()
        {
            Player.Hooks.OnEnterWorld += Hook_OnEnterWorld;
            On.Terraria.WorldGen.setWorldSize += WorldGen_setWorldSize;
            On.Terraria.Main.DrawToMap_Section += Main_DrawToMap_Section;
        }
        private void Main_DrawToMap_Section(On.Terraria.Main.orig_DrawToMap_Section orig, Main self, int secX, int secY)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Color[] mapColorCacheArray = color;
            int num = secX * 200;
            int num2 = num + 200;
            int num3 = secY * 150;
            int num4 = num3 + 150;
            int num5 = num / Main.textureMaxWidth;
            int num6 = num3 / Main.textureMaxHeight;
            int num7 = num % Main.textureMaxWidth;
            int num8 = num3 % Main.textureMaxHeight;
            if (num2 > Main.Map.MaxWidth || num4 > Main.Map.MaxHeight)
            {
                return;
            }
            if (!CheckMap(num5, num6))
            {
                return;
            }
            int num9 = 0;
            for (int i = num3; i < num4; i++)
            {
                for (int j = num; j < num2; j++)
                {
                    MapTile mapTile = Main.Map[j, i];
                    mapColorCacheArray[num9] = MapHelper.GetMapTileXnaColor(ref mapTile);
                    num9++;
                }
            }
            try
            {
                Main.instance.GraphicsDevice.SetRenderTarget(Main.instance.mapTarget[num5, num6]);
            }
            catch (ObjectDisposedException)
            {
                Main.initMap[num5, num6] = false;
                return;
            }
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Main.instance.mapSectionTexture.SetData(mapColorCacheArray, 0, mapColorCacheArray.Length);
            Main.spriteBatch.Draw(Main.instance.mapSectionTexture, new Vector2(num7, num8), Color.White);
            Main.spriteBatch.End();
            Main.instance.GraphicsDevice.SetRenderTarget(null);
            double totalMilliseconds4 = stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Stop();
        }
        protected static bool CheckMap(int i, int j)
        {
            if (Main.instance.mapTarget[i, j] == null || Main.instance.mapTarget[i, j].IsDisposed)
            {
                Main.initMap[i, j] = false;
            }
            if (!Main.initMap[i, j])
            {
                try
                {
                    int width = Main.textureMaxWidth;
                    int height = Main.textureMaxHeight;
                    if (i == Main.mapTargetX - 1)
                    {
                        width = 400;
                    }
                    if (j == Main.mapTargetY - 1)
                    {
                        height = 600;
                    }
                    Main.instance.mapTarget[i, j] = new RenderTarget2D(Main.instance.GraphicsDevice, width, height, false, Main.instance.GraphicsDevice.PresentationParameters.BackBufferFormat, 0, 0, RenderTargetUsage.PreserveContents);
                }
                catch
                {
                    Main.mapEnabled = false;
                    for (int k = 0; k < Main.mapTargetX; k++)
                    {
                        for (int l = 0; l < Main.mapTargetY; l++)
                        {
                            try
                            {
                                Main.initMap[k, l] = false;
                                Main.instance.mapTarget[k, l].Dispose();
                            }
                            catch
                            {
                            }
                        }
                    }
                    return false;
                }
                Main.initMap[i, j] = true;
                return true;
            }
            return true;
        }
        public override void OnModUnload()
        {
            Player.Hooks.OnEnterWorld -= Hook_OnEnterWorld;
            On.Terraria.WorldGen.setWorldSize -= WorldGen_setWorldSize;
        }
        public static void Register(SubWorld subworld)
        {
            subworlds ??= new();
            subworlds.TryAdd(subworld.GetType().FullName, subworld);
        }
        private void Hook_OnEnterWorld(Player player)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                current?.OnEnterWorld(player);
            }
        }
        public static bool IsActive<T>() where T : SubWorld
        {
            return current?.GetType().FullName == typeof(T).FullName;
        }
        public static bool AnyActive => current is null;
        private static string CurrentPath
        {
            get
            {
                if (current is null)
                {
                    throw new NullReferenceException("current world is null");
                }
                if (orig is null)
                {
                    throw new NullReferenceException("orig not set");
                }
                return Path.Combine(Main.WorldPath, $"{ElementMachine.Instance.Name}", "Subworlds", Path.GetFileNameWithoutExtension(orig.Path), current.Name + ".wld");
            }
        }
        public static bool Enter<T>() where T : SubWorld
        {
            if (subworlds.TryGetValue(typeof(T).FullName, out var world))
            {
                if (current is not null)
                {
                    world.Lastworld = current;
                }
                if (orig is null)
                {
                    orig = Main.ActiveWorldFileData;
                }
                current = world;
                Main.gameMenu = true;
                Task.Factory.StartNew(LoadWorld);
                subworldReseted = false;
                return true;
            }
            return false;
        }
        public static void Exit(bool iferrorbacktitil = true)
        {
            if (current is not null)
            {
                var last = current.LastReachable;
                if (last is null)
                {
                    ExitAll(iferrorbacktitil);
                }
                else
                {
                    current.ClearLastUntil(last);
                    current = last;
                    Main.gameMenu = true;
                    Task.Factory.StartNew(LoadWorld);
                }
                subworldReseted = false;
            }
        }
        public static void ExitAll(bool backtitil = true)
        {
            if (orig is null)
            {
                if (backtitil)
                {
                    Main.menuMode = MenuID.Title;
                    ElementMachine.Instance.Logger.Error("orig is lost");
                }
                return;
            }
            current = null;
            Main.gameMenu = true;
            Task.Factory.StartNew(LoadWorld);
            subworldReseted = false;
        }
        private static void LoadWorld()
        {
            try
            {
                WorldGen.gen = true;
                WorldGen.loadFailed = false;
                WorldGen.loadSuccess = false;
                WorldGen.worldBackup = true;
                Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
                if (current is not null)
                {
                    SetWorldSizeWork = true;
                    current.Lastworld?.OnExitWorld(Main.LocalPlayer);
                    if (current.ShouldSaveWorld)
                    {
                        if (current.ShouldSavePlayer)
                        {
                            Player.SavePlayer(Main.ActivePlayerFileData);
                        }
                        if (!File.Exists(CurrentPath))
                        {
                            string folder = Path.Combine(Main.WorldPath, $"{ElementMachine.Instance.Name}", "Subworlds", Path.GetFileNameWithoutExtension(orig.Path));
                            if (!Directory.Exists(folder))
                            {
                                Directory.CreateDirectory(folder);
                            }
                            WorldGen.clearWorld();
                            current.CreateWorld(new Point(Main.maxTilesX, Main.maxTilesY));
                            GetCurrentSubWorldFileData().SetAsActive();
                            WorldFile.SaveWorld(false, true);
                            if (!File.Exists(CurrentPath))
                            {
                                throw new IOException("World save failed.Path:" + CurrentPath);
                            }
                        }
                        WorldGen.clearWorld();
                        GetCurrentSubWorldFileData().SetAsActive();
                        current.SetReSpawnPosition(new Point(Main.maxTilesX, Main.maxTilesY));
                        WorldGen.playWorldCallBack(1);
                    }
                    else
                    {
                        if (current.ShouldSavePlayer)
                        {
                            Player.SavePlayer(Main.ActivePlayerFileData);
                        }
                        WorldGen.clearWorld();
                        current.CreateWorld(new Point(Main.maxTilesX, Main.maxTilesY));
                        current.SetReSpawnPosition(new Point(Main.maxTilesX, Main.maxTilesY));
                        Main.LocalPlayer.Spawn(PlayerSpawnContext.SpawningIntoWorld);
                        if (Main.netMode != NetmodeID.Server)
                        {
                            Main.sectionManager.SetAllFramesLoaded();
                        }
                        while (Main.loadMapLock)
                        {
                            float num = (float)Main.loadMapLastX / Main.maxTilesX;
                            Main.statusText = Lang.gen[68].Value + " " + ((int)(num * 100f + 1f)).ToString() + "%";
                            Thread.Sleep(0);
                            if (!Main.mapEnabled)
                            {
                                break;
                            }
                        }
                        if (Main.gameMenu)
                        {
                            Main.gameMenu = false;
                        }
                        if (Main.netMode == NetmodeID.SinglePlayer && Main.anglerWhoFinishedToday.Contains(Main.player[Main.myPlayer].name))
                        {
                            Main.anglerQuestFinished = true;
                        }
                        //IL.Terraria.Main.OnTickForpublicCodeOnly += FinishPlayWorld;
                    }
                    SetWorldSizeWork = false;
                    return;
                }
                if (orig is null)
                {
                    Player.SavePlayer(Main.ActivePlayerFileData);
                    Main.menuMode = MenuID.Title;
                    Main.gameMenu = false;
                    ElementMachine.Instance.Logger.Error("orig is null");
                }
                SetWorldSizeWork = true;
                WorldGen.clearWorld();
                orig.SetAsActive();
                WorldGen.playWorldCallBack(1);
                SetWorldSizeWork = false;
            }
            catch (Exception e)
            {
                SetWorldSizeWork = false;
                Main.menuMode = MenuID.Title;
                Main.gameMenu = false;
                ElementMachine.Instance.Logger.Error(e);
            }
        }
        public static WorldFileData GetCurrentSubWorldFileData()
        {
            WorldFileData data = new(CurrentPath, false)
            {
                Name = current.SaveName,
                GameMode = Main.GameMode,
                CreationTime = DateTime.Now,
                Metadata = FileMetadata.FromCurrentSettings(FileType.World),
                WorldGeneratorVersion = 1065151889409UL
            };
            data.SetSeed(orig.SeedText);
            using (MD5 md5 = MD5.Create())
            {
                data.UniqueId = new Guid(md5.ComputeHash(Encoding.ASCII.GetBytes(Path.GetFileNameWithoutExtension(orig.Path) + current.SaveName)));
            }
            return data;
        }
        private void WorldGen_setWorldSize(On.Terraria.WorldGen.orig_setWorldSize orig)
        {
            lock (Main.instance)
            {
                if (!SetWorldSizeWork || current is null)
                {
                    if (Main.tile.Width != 8401 || Main.tile.Height != 2401)
                    {
                        Main.Map = new WorldMap(8401, 2401);
                        Main.tile = (Tilemap)typeof(Tilemap).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, new Type[]
                        {
                            typeof(ushort),
                            typeof(ushort)
                        }).Invoke(new object[]
                        {
                                (ushort)8401,
                                (ushort)2401
                        });
                        Main.mapTargetX = 5;
                        Main.mapTargetY = 2;
                        Main.instance.mapTarget = new RenderTarget2D[Main.mapTargetX, Main.mapTargetY];
                        Main.initMap = new bool[Main.mapTargetX, Main.mapTargetY];
                        Main.mapWasContentLost = new bool[Main.mapTargetX, Main.mapTargetY];
                        Main.maxSectionsX = (Main.maxTilesX - 1) / 200 + 1;
                        Main.maxSectionsY = (Main.maxTilesY - 1) / 150 + 1;
                        Main.sectionManager = new WorldSections((Main.maxTilesX - 1) / 200 + 1, (Main.maxTilesY - 1) / 150 + 1);
                    }
                    orig();
                }
                else
                {
                    Main.rightWorld = current.Width * 16;
                    Main.bottomWorld = current.Height * 16;
                    Main.maxTilesX = (int)Main.rightWorld / 16 + 1;
                    Main.maxTilesY = (int)Main.bottomWorld / 16 + 1;
                    if (Main.maxTilesX * Main.maxTilesY * 18 > GC.GetGCMemoryInfo().TotalAvailableMemoryBytes * 0.8)
                    {
                        string textValue = "地图可能占用的空间超过限制";
                        Utils.ShowFancyErrorMessage(textValue, 0);
                        throw new Exception(textValue);
                    }
                    Main.Map = new WorldMap(Main.maxTilesX, Main.maxTilesY);
                    Main.tile = (Tilemap)typeof(Tilemap).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, new Type[]
                    {
                        typeof(ushort),
                        typeof(ushort)
                    }).Invoke(new object[]
                    {
                        (ushort)Main.maxTilesX,
                        (ushort)Main.maxTilesY
                    });
                    Main.mapTargetX = Main.maxTilesX / Main.textureMaxWidth + 1;
                    Main.mapTargetY = Main.maxTilesY / Main.textureMaxHeight + 1;
                    Main.maxSectionsX = (Main.maxTilesX - 1) / 200 + 1;
                    Main.maxSectionsY = (Main.maxTilesY - 1) / 150 + 1;
                    Main.instance.mapTarget = new RenderTarget2D[Main.mapTargetX, Main.mapTargetY];
                    Main.initMap = new bool[Main.mapTargetX, Main.mapTargetY];
                    Main.mapWasContentLost = new bool[Main.mapTargetX, Main.mapTargetY];
                    Main.sectionManager = new WorldSections((Main.maxTilesX - 1) / 200 + 1, (Main.maxTilesY - 1) / 150 + 1);
                }
            }
        }
        public static void FinishPlayWorld()
        {
            //Main.OnTickForpublicCodeOnly -= FinishPlayWorld;
            Main.player[Main.myPlayer].Spawn(PlayerSpawnContext.SpawningIntoWorld);
            Main.ActivePlayerFileData.StartPlayTimer();
            WorldGen._lastSeed = Main.ActiveWorldFileData.Seed;
            Player.Hooks.EnterWorld(Main.myPlayer);
            WorldFile.SetOngoingToTemps();
            typeof(SoundEngine).GetMethod("PlaySound", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] { 11, -1, -1, 1, 1f, 0f });
            //SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
            Main.resetClouds = true;
            WorldGen.noMapUpdate = false;
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (!subworldReseted && (Main.menuMode == MenuID.WorldSelect || Main.menuMode == MenuID.Title))
            {
                current = null;
                orig = null;
                foreach (var world in subworlds.Values)
                {
                    world.ClearLastUntil(null);
                }
                subworldReseted = true;
            }
        }
    }
}
