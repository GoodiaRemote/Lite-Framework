using System;
using Object = UnityEngine.Object;

namespace LiteFramework.Runtime.ObjectPool
{
    public interface IPoolale<out T> where T : Object
    {
        public event Action<T> ReleaseEvent;
        void OnGet();
        void OnRelease();
        void ReleaseToPool();
    }
}