using ElementMachine.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ElementMachine.World
{
    public abstract class SubWorld : ModType
    {
        public virtual string SaveName => GetType().Name;
        public abstract int Width { get; }
        public abstract int Height { get; }
        public virtual bool ShouldSaveWorld { get; } = true;
        public virtual bool ShouldSavePlayer { get; } = true;
        public virtual void PreEnterWorld()
        {

        }
        public virtual void OnEnterWorld(Player player)
        {

        }
        public virtual void OnExitWorld(Player player)
        {

        }
        public virtual void SetReSpawnPosition(Point worldsize)
        {
            Main.LocalPlayer.ChangeSpawn(worldsize.X, worldsize.Y);
        }
        public virtual void CreateWorld(Point worldsize)
        {

        }
        protected sealed override void Register()
        {
            SubWorldSystem.Register(this);
        }
        public SubWorld Lastworld { get; internal set; }
        internal SubWorld LastReachable
        {
            get
            {
                if (Lastworld is null)
                {
                    return null;
                }
                if (!Lastworld.ShouldSaveWorld)
                {
                    return Lastworld.LastReachable;
                }
                return LastReachable;
            }
        }
        internal void ClearLastUntil(SubWorld end)
        {
            if (Lastworld is null || Lastworld == end)
            {
                return;
            }
            Lastworld.ClearLastUntil(end);
            Lastworld = null;
        }
    }
    public class TestWorld : SubWorld
    {
        public override int Width => 200;
        public override int Height => 150;
        public override void CreateWorld(Point worldsize)
        {
            GenerationProgress progress = Main.AutogenProgress;
            Main.worldSurface = Main.maxTilesY - 42;
            Main.rockLayer = Main.maxTilesY;
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY));
                    WorldGen.PlaceTile(i, j, TileID.Stone);
                }
            }
            base.CreateWorld(worldsize);
        }
    }
}