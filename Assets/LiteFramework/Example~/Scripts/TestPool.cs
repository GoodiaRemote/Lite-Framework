using LiteFramework.Runtime.ObjectPool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LiteFramework.Example
{
    public class TestPool : MonoBehaviour
    {
        public Pool<Box> Pool;


        [Button]
        public void Spawn()
        {
            Pool.Get();
        }
    }
}