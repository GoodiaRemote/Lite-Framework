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

        public static void TryReleaseToPool<T>(this T obj) where T : Object
        {
            if (obj is IPoolale<T> iPool)
            {
                iPool.ReleaseToPool();
            }
            else
            {
                Debug.LogWarning("Object of type " + obj.GetType() + " not implement IPoolale");
            }
        }
    }
}