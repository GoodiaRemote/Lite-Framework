using UnityEngine;

namespace LiteFramework.Runtime.ObjectPool
{
    public static class PoolExtension
    {
        public static GameObject GameObject(this Object obj)
        {
            return obj switch
            {
                GameObject gameObject => gameObject,
                Component component => component.gameObject,
                _ => null
            };
        }
    }
}