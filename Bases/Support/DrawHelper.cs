using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Exceptions;
using Terraria.UI;

namespace ElementMachine.Bases.Support
{
    public class DrawHelper : ModSystem
    {
        public struct CustomVertexInfo : IVertexType
        {
            private static readonly VertexDeclaration _vertexDeclaration = new(new VertexElement[3]
            {
                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
                new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
            });
            public Vector2 Position;
            public Color Color;
            public Vector3 TexCoord;

            public CustomVertexInfo(Vector2 position, Color color, Vector3 texCoord)
            {
                Position = position;
                Color = color;
                TexCoord = texCoord;
            }

            public VertexDeclaration VertexDeclaration
            {
                get
                {
                    return _vertexDeclaration;
                }
            }
        }
        public static Asset<T> SureLoaded<T>(Asset<T> asset) where T : class
        {
            if (asset.State == AssetState.NotLoaded)
            {
                ModContent.SplitName(asset.Name, out string modName, out string subName);
                if (Main.dedServ && Main.Assets == null)
                {
                    Main.Assets = new AssetRepository(null);
                }
                if (modName == "Terraria")
                {
                    Main.Assets.Request<T>(subName, AssetRequestMode.ImmediateLoad);
                }
                if (!ModLoader.TryGetMod(modName, out Mod mod))
                {
                    throw new MissingResourceException("Missing mod: " + asset.Name);
                }
                mod.Assets.Request<T>(subName, AssetRequestMode.ImmediateLoad);
            }
            return asset;
        }
        public class DrawTask
        {
            public Func<List<GameInterfaceLayer>, int> FindIndex;
            public Action Draw;
            public string Name;
            public InterfaceScaleType scaleType;
            public int Mode;
        }
        private static Queue<DrawTask> drawTasks = new();
        public static void AddDrawTask(DrawTask task)
        {
            drawTasks.Enqueue(task);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            while (drawTasks.TryDequeue(out var task))
            {
                int index = task.FindIndex?.Invoke(layers) ?? -1;
                if (index != -1)
                {
                    switch (task.Mode)
                    {
                        case 0:
                            {
                                layers.Insert(index, new LegacyGameInterfaceLayer(task.Name,
                                    delegate
                                    {
                                        task?.Draw();
                                        return true;
                                    }, task.scaleType));
                                break;
                            }
                        case 1:
                            {
                                layers.RemoveAt(index);
                                layers.Insert(index, new LegacyGameInterfaceLayer(task.Name,
                                    delegate
                                    {
                                        task?.Draw();
                                        return true;
                                    }, task.scaleType));
                                break;
                            }
                    }
                }
                else
                {
                    if (task.Mode == 2)
                    {
                        layers.Add(new LegacyGameInterfaceLayer(task.Name,
                            delegate
                            {
                                task?.Draw();
                                return true;
                            }, task.scaleType));
                    }
                    else if (task.Mode == 3)
                    {
                        layers.Insert(0, new LegacyGameInterfaceLayer(task.Name,
                            delegate
                            {
                                task?.Draw();
                                return true;
                            }, task.scaleType));
                    }
                }
            }
        }
    }
}