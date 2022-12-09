using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using ElementMachine.Bases.Support;
using System.Linq;

namespace ElementMachine.Bases.ToolObject
{
    public class KeyAlreadyExistsException : Exception
    {
        public KeyAlreadyExistsException(string key) : base($"{key} already exists.")
        {

        }
    }
    public class AssetEntrust<T> : IDisposable where T : class
    {
        private readonly Dictionary<string, string> paths = new();
        private readonly Dictionary<string, Asset<T>> assets = new();
        internal T this[string key] => GetAsset(key).Value;
        internal Asset<T> GetAsset(string key)
        {
            if (paths.TryGetValue(key, out var path))
            {
                if (assets.TryGetValue(path, out var asset))
                {
                    switch (asset.State)
                    {
                        case AssetState.Loaded:
                            {
                                return asset;
                            }
                        case AssetState.Loading:
                            {
                                while (asset.State == AssetState.Loading) ;
                                return asset;
                            }
                        case AssetState.NotLoaded:
                            {
                                asset = ModContent.Request<T>(path, AssetRequestMode.ImmediateLoad);
                                assets[path] = asset;
                                return asset;
                            }
                    }
                }
                else
                {
                    asset = ModContent.Request<T>(path, AssetRequestMode.ImmediateLoad);
                    assets[path] = asset;
                    return asset;
                }
            }
            throw new KeyNotFoundException(key);
        }
        internal string Register(string path, string key = null, bool replace = false, AssetRequestMode mode = AssetRequestMode.DoNotLoad)
        {
            path = path.Replace('.', '/');
            key ??= path.Split('/')[^1];
            if (paths.ContainsKey(key) && !replace)
            {
                throw new KeyAlreadyExistsException(key);
            }
            else
            {
                if (!ModContent.HasAsset(path))
                {
                    throw new ArgumentException($"Resource does not exist at {path}");
                }
                paths[key] = path;
                if (mode != AssetRequestMode.DoNotLoad)
                {
                    assets[path] = ModContent.Request<T>(path, mode);
                }
            }
            return key;
        }
        internal bool Remove(string key, bool disposeresource = false)
        {
            if (paths.TryGetValue(key, out var path))
            {
                if (assets.TryGetValue(path, out var asset))
                {
                    if (disposeresource)
                    {
                        asset.Dispose();
                    }
                    assets.Remove(path);
                }
                paths.Remove(key);
                return true;
            }
            return false;
        }
        internal void Clear(bool disposeresources = false)
        {
            if (disposeresources)
            {
                assets.Values.ForEach(resource => resource.Dispose());
            }
            assets.Clear();
            paths.Clear();
        }
        internal IReadOnlyDictionary<string, T> GetLoadedResources()
        {
            Dictionary<string, T> result = new();
            foreach (var pair in paths)
            {
                if (assets.TryGetValue(pair.Value, out var asset))
                {
                    if (asset.State == AssetState.Loaded)
                    {
                        result[pair.Key] = asset.Value;
                    }
                }
            }
            return result;
        }
        internal int LoadedCount => (from Asset<T> asset in assets.Values where asset.State == AssetState.Loaded select asset).Count();
        internal bool TryGetResource(string key, out T resource, bool loadifexists = true)
        {
            resource = null;
            if (paths.TryGetValue(key, out var path))
            {
                if (assets.TryGetValue(path, out var asset))
                {
                    if (asset.State == AssetState.Loaded)
                    {
                        resource = asset.Value;
                        return true;
                    }
                }
                else
                {
                    if (loadifexists && ModContent.HasAsset(path))
                    {
                        var newasset = ModContent.Request<T>(path, AssetRequestMode.ImmediateLoad);
                        assets[path] = newasset;
                        resource = newasset.Value;
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
        internal bool IsTheKeyValid(string key)
        {
            if (paths.TryGetValue(key, out var path))
            {
                return ModContent.HasAsset(path);
            }
            return false;
        }
        internal bool ContainsPath(string path, out IEnumerable<string> keys)
        {
            keys = from KeyValuePair<string, string> pair in paths where pair.Value == path select pair.Key;
            return keys.Any();
        }
        internal string GetPath(string key) => paths[key];
        public void Dispose()
        {
            foreach (var asset in assets.Values)
            {
                asset.Dispose();
            }
        }
    }
}