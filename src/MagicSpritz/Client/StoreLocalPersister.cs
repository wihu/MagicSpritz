using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MessagePack;

namespace MagicSpritz
{
public class StoreLocalPersister : IDisposable
{
    public struct Config
    {
        public bool AutoSave;
        public float AutoSaveIntervalSec;
        public string FilePath;
        public bool TextFormat;

    }
    private interface IPersister
    {
        bool TryGet<T>(string key, out T obj);
        bool TrySet<T>(string key, T obj);
        bool Load();
        bool Save();
    }

    private abstract class Persister : IPersister
    {
        protected Config _config;
        public Persister(Config config)
        {
            _config = config;
        }

        public abstract bool TryGet<T>(string key, out T obj);
        public abstract bool TrySet<T>(string key, T obj);
        public abstract bool Load();
        public abstract bool Save();
    }

    private class BinaryPersister : Persister
    {
        public BinaryPersister(Config config) : base(config)
        {
        }

        private Dictionary<string, byte[]> _data = new Dictionary<string, byte[]>();

        public override bool TryGet<TValue>(string key, out TValue obj)
        {
            obj = default(TValue);
            return false;
        }

        public override bool TrySet<TValue>(string key, TValue obj)
        {
            return false;
        }

        public override bool Load()
        {
            return false;
        }
        public override bool Save()
        {
            return false;
        }
    }

    private class TextPersister : Persister
    {
        public TextPersister(Config config) : base(config)
        {
        }

        private Dictionary<string, string> _data = new Dictionary<string, string>();

        public override bool TryGet<TValue>(string key, out TValue obj)
        {
            obj = default(TValue);
            if (!_data.TryGetValue(key, out var val))
            {
                return false;
            }

            var bytes = MessagePackSerializer.FromJson(val);
            obj = MessagePackSerializer.Deserialize<TValue>(bytes);
            return true;
        }

        public override bool TrySet<TValue>(string key, TValue obj)
        {
            var bytes = MessagePackSerializer.Serialize<TValue>(obj);
            _data[key] = MessagePackSerializer.ToJson(bytes);
            return true;
        }

        public override bool Load()
        {
            string path = _config.FilePath;
            if (!File.Exists(path))
            {
                return false;
            }
            string content = File.ReadAllText(path);
            if (string.IsNullOrEmpty(content))
            {
                return false;
            }
            var bytes = MessagePackSerializer.FromJson(content);
            var data = MessagePackSerializer.Deserialize<object>(bytes) as Dictionary<object, object>;
            _data = data.ToDictionary(x => x.Key.ToString(), x => x.Value.ToString());
            
            return true;
        }

        public override bool Save()
        {
            string path = _config.FilePath;
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            var bytes = MessagePackSerializer.Serialize<object>(_data);
            string content = MessagePackSerializer.ToJson(bytes);
            File.WriteAllText(path, content);
            return false;
        }
    }

    private Config _config;
    private IPersister _persister;

    public StoreLocalPersister(Config config)
    {
        _config = config;
        _persister = config.TextFormat ? new TextPersister(config) : new BinaryPersister(config) as IPersister;
    }

    public bool Get<T>(string key, out T obj)
    {
        return _persister.TryGet<T>(key, out obj);
    }

    public void Set<T>(string key, T obj)
    {
        _persister.TrySet<T>(key, obj);
    }

    public bool Load()
    {
        return _persister.Load();
    }

    public bool Save()
    {
        return _persister.Save();
    }

    public void Clear()
    {

    }

    public void Update()
    {

    }

    void IDisposable.Dispose()
    {
        if (_config.AutoSave)
        {
            Save();
        }
    }
}
}
