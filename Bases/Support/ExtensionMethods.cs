using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.Utilities;

namespace ElementMachine.Bases.Support
{
    public static class ExtensionMethods
    {
        #region doubleExtensionMethods
        public static double NormalDistribution(this UnifiedRandom random, double IntervalMedian, double IntervalVariance)
        {
            return IntervalMedian + IntervalVariance * Math.Sqrt(-2 * Math.Log(random.NextDouble())) * Math.Sin(2 * Math.PI * random.NextDouble());
        }
        #endregion
        #region Vector2ExtensionMethods
        public static Vector2 NormalizeSafely(this Vector2 vector)
        {
            if (vector.X == 0 && vector.Y == 0)
            {
                return Vector2.UnitX;
            }
            return Vector2.Normalize(vector);
        }
        public static Vector2 NormalDistribution(this Vector2 vector2, double IntervalVariance, float? newlength = null)
        {
            float f = (float)new UnifiedRandom().NormalDistribution(vector2.ToRotation(), IntervalVariance);
            float l = newlength ?? vector2.Length();
            return f.ToRotationVector2() * l;
        }
        public static Vector2 RotatedBy(this Vector2 vector2, float r, bool Clockwise = true)
        {
            float R = vector2.ToRotation() + r * (Clockwise ? 1 : -1);
            return new Vector2((float)Math.Cos(R), (float)Math.Sin(R)) * vector2.Length();
        }
        public static int NewProj(this Vector2 position, Vector2 velocity, int type, int damage = 1, float knockback = 0, int owner = 255, float ai0 = 0, float ai1 = 0, IEntitySource source = null)
        {
            return Projectile.NewProjectile(source,
                position.X,
                position.Y,
                velocity.X,
                velocity.Y,
                type,
                damage,
                knockback,
                owner,
                ai0,
                ai1);
        }
        public static Projectile NewProjDirect(this Vector2 position, Vector2 velocity, int type, int damage = 1, float knockback = 0, int owner = 255, float ai0 = 0, float ai1 = 0, IEntitySource source = null)
        {
            return Main.projectile[position.NewProj(velocity, type, damage, knockback, owner, ai0, ai1, source)];
        }
        public static int NewNpc(this Vector2 position, int type, IEntitySource source = null, int start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int target = 255)
        {
            return NPC.NewNPC(source, (int)position.X, (int)position.Y, type, start, ai0, ai1, ai2, ai3, target);
        }
        public static NPC NewNpcDirect(this Vector2 position, int type, IEntitySource source = null, int start = 0, float ai0 = 0, float ai1 = 0, float ai2 = 0, float ai3 = 0, int target = 255)
        {
            return Main.npc[position.NewNpc(type, source, start, ai0, ai1, ai2, ai3, target)];
        }
        public static int NewItem(this Vector2 position, Vector2 size, int type, int stack = 1, IEntitySource source = null, bool noBroadcast = false, int prefixGiven = 0, bool noGrabDelay = false, bool reverseLookup = false)
        {
            if (size == default)
            {
                size.X = 1;
                size.Y = 1;
            }
            return Item.NewItem(source,
                new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.X),
                type,
                stack,
                noBroadcast,
                prefixGiven,
                noGrabDelay,
                reverseLookup);
        }
        public static Item NewItemDirect(this Vector2 position, Vector2 size, int type, int stack = 1, IEntitySource source = null, bool noBroadcast = false, int prefixGiven = 0, bool noGrabDelay = false, bool reverseLookup = false)
        {
            return Main.item[position.NewItem(size, type, stack, source, noBroadcast, prefixGiven, noGrabDelay, reverseLookup)];
        }
        public static Point TileCoord(this Vector2 position)
        {
            return new Point((int)(position.X / 16), (int)(position.Y / 16));
        }
        public static bool HasTile(this Vector2 position)
        {
            Point coord = position.TileCoord();
            if (WorldGen.InWorld(coord.X, coord.Y))
            {
                return Main.tile[coord.X, coord.Y].HasTile;
            }
            return false;
        }
        public static Tile GetTile(this Vector2 position)
        {
            return Main.tile[(int)position.X / 16, (int)position.Y / 16];
        }
        public static bool Intersects(this Vector2 v, Rectangle r)
        {
            return r.Intersects(new Rectangle((int)v.X, (int)v.Y, 1, 1));
        }
        #endregion
        #region PlayerExtensionMethods
        public static bool HasItem(this Player player, int type, out Item item)
        {
            return player.HasItem(new Predicate<Item>((item) => item.type == type), out int index, out item);
        }
        public static bool HasItem(this Player player, int type, out int index, out Item item)
        {
            return player.HasItem(new Predicate<Item>((item) => item.type == type), out index, out item);
        }
        public static bool HasItem(this Player player, Predicate<Item> predicate, out int index, out Item item)
        {
            item = null;
            index = -1;
            Item Item;
            for (int i = 0; i < player.inventory.Length; i++)
            {
                Item = player.inventory[i];
                if (predicate(Item))
                {
                    item = Item;
                    index = i;
                    return true;
                }
            }
            return false;
        }
        public static Dictionary<int, List<(Item[], int)>> GetItemPositionSpecialChest(this Player player, int[] ignore)
        {
            List<Item[]> chest = new();
            if (!ignore.Contains(-2))
            {
                chest.Add(player.bank.item);
            }
            if (!ignore.Contains(-3))
            {
                chest.Add(player.bank2.item);
            }
            if (!ignore.Contains(-4))
            {
                chest.Add(player.bank3.item);
            }
            if (!ignore.Contains(-5))
            {
                chest.Add(player.bank4.item);
            }
            if (player.chest > -1 && player.chest < Main.chest.Length && !ignore.Contains(player.chest))
            {
                chest.Add(Main.chest[player.chest].item);
            }
            Dictionary<int, List<(Item[], int)>> result = new();
            for (int i = 0; i < chest.Count; i++)
            {
                Item[] recipe = chest[i];
                for (int j = 0; j < recipe.Length; j++)
                {
                    if (result.TryGetValue(recipe[j].type, out var items))
                    {
                        items.Add((recipe, j));
                    }
                    else
                    {
                        result.Add(recipe[j].type, new() { (recipe, j) });
                    }
                }
            }
            return result;
        }
        public static bool CanGrab(this Player.ItemSpaceStatus status)
        {
            return status.CanTakeItem || status.ItemIsGoingToVoidVault;
        }
        #endregion
        #region ModExtensionMethods
        public static TmodFile GetFiles(this Mod mod)
        {
            return (TmodFile)mod.GetType().GetProperty("File", BindingFlags.Instance | BindingFlags.NonPublic).GetGetMethod().Invoke(mod, null);
        }
        #endregion
        #region ArrayExtensionMethods
        public static void DefaultFill<T>(this T[] array) where T : new()
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] is null)
                {
                    array[i] = Activator.CreateInstance<T>();
                }
            }
        }
        public static void DefaultFill<T>(params T[][] values) where T : new()
        {
            foreach (T[] value in values)
            {
                value.DefaultFill();
            }
        }
        public static void DefaultFill<T>(this T[] array, T thing) where T : struct
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = thing;
            }
        }
        public static void ForEach<T>(this IEnumerable<T> array, Action<T> action)
        {
            if (action is null)
            {
                throw new InvalidOperationException(action.GetType().FullName);
            }
            foreach (T item in array)
            {
                action(item);
            }
        }
        #endregion
        #region ItemExtensionMethods
        public static bool IsTheSameAs(this Item item, Item compareItem)
        {
            return item.netID == compareItem.netID && item.type == compareItem.type;
        }
        #endregion
        #region OtherExtensionMethods
        public static newT TransT<newT, T>(this T t, bool nullable = true) where newT : class, T where T : class
        {
            if (t is null)
            {
                if (nullable)
                {
                    return null;
                }
                throw new ArgumentNullException(nameof(t));
            }
            if (nullable)
            {
                return t as newT;
            }
            return (newT)t;
        }
        /// <summary>
        /// 创建一个一模一样的字典，但保持原引用
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> WeakClone<TKey, TValue>(this Dictionary<TKey, TValue> dic)
        {
            Dictionary<TKey, TValue> result = new();
            foreach (var pair in dic)
            {
                result.Add(pair.Key, pair.Value);
            }
            return result;
        }
        /// <summary>
        /// 创建一个一模一样的字典，但是全部克隆了新的
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> StrongClone<TKey, TValue>(this Dictionary<TKey, TValue> dic) where TKey : ICloneable where TValue : ICloneable
        {
            Dictionary<TKey, TValue> result = new();
            foreach (var pair in dic)
            {
                result.Add((TKey)pair.Key.Clone(), (TValue)pair.Value.Clone());
            }
            return result;
        }
        #endregion
    }
    public class CustomComparer<T> : IComparer<T>
    {
        private readonly Func<T, T, int> ComparerFunction;
        public CustomComparer(Func<T, T, int> comparer)
        {
            ComparerFunction = comparer;
        }
        public int Compare(T x, T y)
        {
            return ComparerFunction(x, y);
        }
        public static readonly CustomComparer<Item> ItemTypeComparer = new((i1, i2) => i1.type.CompareTo(i2.type));
        public static readonly CustomComparer<Item> ItemDamageComparer = new((i1, i2) => i1.damage.CompareTo(i2.damage));
        public static readonly CustomComparer<Item> ItemRareComparer = new((i1, i2) => Math.Abs(i1.rare).CompareTo(Math.Abs(i2.rare)));
        public static readonly CustomComparer<NPC> NPCTypeComparer = new((i1, i2) => i1.type.CompareTo(i2.type));
        public CustomComparer<Entity> DistanceComparer(Vector2 center)
        {
            return new CustomComparer<Entity>((e1, e2) => Vector2.Distance(center, e1.Center).CompareTo(Vector2.Distance(center, e2.Center)));
        }
    }
}
