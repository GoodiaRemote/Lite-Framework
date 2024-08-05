using LiteFramework.Runtime.Config;
using UnityEngine;

namespace LiteFramework.Runtime.Utils
{
    public static class ConfigHelper
    {
        public static T GetConfig<T>() where T : GameConfig
        {
            return Resources.Load<T>("GameConfig/" + typeof(T).Name);
        }
    }
}