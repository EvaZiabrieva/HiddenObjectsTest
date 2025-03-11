using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zenject;

public class ConfigsSystem : MonoBehaviour, IConfigSystem, IInitializable
{
#if UNITY_EDITOR
    [Serializable]
    public class InspectorConfigHolder
    {
        [SerializeReference] public List<BaseConfig> configs;
        [SerializeReference] public string type;

        public InspectorConfigHolder(Type type)
        {
            this.type = type.ToString();
            configs = new List<BaseConfig>();
            BaseConfig config = (BaseConfig)Activator.CreateInstance(type);
            config.id = type.ToString();
            configs.Add(config);
        }
    }

    [SerializeReference] public List<InspectorConfigHolder> Configs;

    [ContextMenu(nameof(FillInspectorConfigs))]
    private void FillInspectorConfigs()
    {
        Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(domainAssembly => domainAssembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(BaseConfig))
                ).ToArray();

        foreach (Type type in types)
        {
            if (Configs.FirstOrDefault(c => c.type == type.ToString()) != null)
                continue;

            Configs.Add(new InspectorConfigHolder(type));
        }
    }

    [ContextMenu(nameof(WriteAllConfigs))]
    private void WriteAllConfigs()
    {
        foreach (InspectorConfigHolder holder in Configs)
        {
            foreach (BaseConfig config in holder.configs)
            {
                WriteConfig(_path + $"/{config.id}.json", config);
            }
        }
    }
#endif
    private readonly string _directoryName = "Configs";

    private string _path => $"Resources/{_directoryName}";
    private List<BaseConfig> _loadedConfigs;

    private void OnValidate()
    {
        if (Directory.Exists(_path))
            return;

        try
        {
            Directory.CreateDirectory(_path);
        }
        catch (Exception e)
        {
            Debug.LogError($"Path {_path} is invalid. Can't create directory, exception: {e.Message}");
        }

#if UNITY_EDITOR
        FillInspectorConfigs();
#endif
    }

    public bool IsInitialized => _loadedConfigs != null;

    public void Initialize()
    {
        _loadedConfigs = new List<BaseConfig>();
    }

    public void Shutdown()
    {
        _loadedConfigs.Clear();
    }

    public T GetConfig<T>(string id) where T : BaseConfig, new()
    {
        foreach (BaseConfig config in _loadedConfigs)
        {
            if (config.id == id)
                return (T)config;
        }

        string path = _directoryName + $"/{id}";
        return GetParsedConfig<T>(path, id);
    }

    private T GetParsedConfig<T>(string path, string id) where T : BaseConfig, new()
    {
        TextAsset json = Resources.Load<TextAsset>(path);
        if (string.IsNullOrEmpty(json.ToString()))
        {
            Debug.LogError($"Config {id} can not be found. Creating default");
            return CreateConfig<T>(path, id);
        }

        return JsonUtility.FromJson<T>(json.ToString());
    }

    private bool TryReadJson(string path, out string json)
    {
        json = null;

        if (!File.Exists(path))
            return false;

        json = File.ReadAllText(path);
        return true;
    }

    private T CreateConfig<T>(string path, string id) where T : BaseConfig, new()
    {
        T config = new T();
        WriteConfig(path, config);
        return config;
    }

    private void WriteConfig<T>(string path, T config) where T : BaseConfig
    {
        string json = JsonUtility.ToJson(config);

        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLineAsync(json);
        }
    }
}