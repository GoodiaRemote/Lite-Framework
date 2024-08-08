using System;
using System.Linq;
using UnityEngine;

namespace LiteFramework.Runtime.Base
{
    public class SensorObject3D : BaseMonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _radius = 1;
        [SerializeField] private int _sensorCapacity = 50;
        [SerializeField, Min(0.02f)] private float _frequency = 0.05f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private bool _gizmos = true;
        
        private Collider[] _results;
        private int _hitCount;
        private float _timer;
        public Action<int, Collider[]> OnSensor;

        private void Awake()
        {
            _results = new Collider[_sensorCapacity];
        }

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            if (_timer >= _frequency)
            {
                _timer = 0;
                _hitCount = Physics.OverlapSphereNonAlloc(Position + _offset, _radius, _results, _layerMask);
                OnSensor?.Invoke(_hitCount, _results.Where(c => c is not null).OrderBy(c=>
                    (c.transform.position - transform.position).sqrMagnitude).ToArray());
            }
        }
        
        private void OnDrawGizmos()
        {
            if(!_gizmos) return;
            Gizmos.color = _hitCount > 0 ? Color.blue : Color.gray;
            Gizmos.DrawWireSphere(transform.position + _offset, _radius);
        }
    }
}