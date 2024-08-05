using System;
using LiteFramework.Runtime.ObjectPool;
using UnityEngine;

namespace LiteFramework.Example
{
    public class Box : MonoBehaviour,IPoolale<Box>
    {

        private float _timer;
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > 1)
            {
                ReleaseEvent?.Invoke(this);
            }
        }

        public event Action<Box> ReleaseEvent;
        public void OnGet()
        {
            _timer = 0;
        }

        public void OnRelease()
        {
            
        }
    }
}