using System.Collections.Generic;
using UnityEngine;

namespace LiteFramework.Runtime.ObjectPool
{
    [System.Serializable]
    public class Pool<T> where T: Object
    {
        [SerializeField] private T _source;
        [SerializeField] private int _defaultCapacity = 10;
        [SerializeField] private int _max = 1000;

        private readonly Queue<T> _available;
        private readonly List<T> _actives;
        private readonly List<T> _all;

        public int CountActive => _actives.Count;
        public int CountInactive => _available.Count;
        public int CountAll => _all.Count;
        
        public Pool()
        {
            _available = new Queue<T>(_defaultCapacity);
            _actives = new List<T>(_defaultCapacity);
            _all = new List<T>(_defaultCapacity);
        }

        public Pool(T prefab, int defaultCapacity = 10, int max = 1000)
        {
            _source = prefab;
            _defaultCapacity = defaultCapacity;
            _max = max;
            _available = new Queue<T>(_defaultCapacity);
            _actives = new List<T>(_defaultCapacity);
            _all = new List<T>(_defaultCapacity);
        }

        public T Get()
        {
            T obj;
            if (CountActive >= _max)
            {
                obj = _actives[0];
                Release(obj);
            }
            if (CountInactive > 0)
            {
                obj = _available.Dequeue();
            }
            else
            {
                obj = CreateFunc();
            }

            if (obj is null) return null;
            _actives.Add(obj);
            ActionOnGet(obj);
            return obj;
        }
        
        public T Get(Transform parent, bool worldPositionStays = true)
        {
            var obj = Get();
            obj.GameObject().transform.SetParent(parent, worldPositionStays);
            return obj;
        }
        
        public T Get(Vector3 position, Quaternion rotation)
        {
            var obj = Get();
            obj.GameObject().transform.SetPositionAndRotation(position, rotation);
            return obj;
        }
        
        public T Get(Vector3 position, Quaternion rotation, Transform parent)
        {
            var obj = Get();
            obj.GameObject().transform.SetPositionAndRotation(position, rotation);
            obj.GameObject().transform.SetParent(parent);
            return obj;
        }
        

        public void Release(T obj)
        {
            if(!_actives.Contains(obj)) return;
            _actives.Remove(obj);
            _available.Enqueue(obj);
            ActionOnRelease(obj);
        }

        public void Clear()
        {
            for (int i = 0; i < _all.Count; i++)
            {
                ActionOnDestroy(_all[i]);
            }
            
            _available.Clear();
            _actives.Clear();
        }

        private void ActionOnDestroy(T obj)
        {
            Object.Destroy(obj);
        }

        private void ActionOnRelease(T obj)
        {
            if (obj is IPoolale<T> iPool)
            {
                iPool.OnRelease();
            }
            obj.GameObject().SetActive(false);
        }

        private void ActionOnGet(T obj)
        {
            obj.GameObject().SetActive(true);
            if (obj is IPoolale<T> iPool)
            {
                iPool.OnGet();
            }
        }

        private T CreateFunc()
        {
            if (_source is not null)
            {
                var obj = Object.Instantiate(_source);
                if (obj is IPoolale<T> iPool)
                {
                    iPool.ReleaseEvent += Release;
                }
                _all.Add(obj);
                return obj;
            }

            Debug.LogWarning("Source cannot be null");
            return null;
        }
    }
}