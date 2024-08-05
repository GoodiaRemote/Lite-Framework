using System.IO;
using LiteFramework.Runtime.Config;
using LiteFramework.Runtime.Utils;
using UnityEditor;
using UnityEngine;

namespace LiteFramework.Editor.Utils
{
    public static class EditorConfigHelper
    {
        public static T GetOrCreateConfig<T>() where T : GameConfig
        {
            var config = ConfigHelper.GetConfig<T>();
            if (config == null)
            {
                config = ScriptableObject.CreateInstance<T>();
                config.Description = ObjectNames.NicifyVariableName(typeof(T).Name);
                string path = "Assets/Resources/GameConfig/";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                AssetDatabase.CreateAsset(config, $"{path}/{typeof(T).Name}.asset");
                AssetDatabase.SaveAssets();
            }

            return config;
        }
    }
}