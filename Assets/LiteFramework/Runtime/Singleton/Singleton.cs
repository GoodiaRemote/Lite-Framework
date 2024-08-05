using LiteFramework.Runtime.Base;
using UnityEngine;

namespace LiteFramework.Runtime.Singleton
{
    public class Singleton<T> : BaseMonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
        }
    }
}