using System;
using System.Linq;
using LiteFramework.Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LiteFramework.Runtime.Base
{
    public class SensorObject : BaseMonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _radius = 1;
        [SerializeField] private int _sensorCapacity = 50;
        [SerializeField, Min(0.02f)] private float _frequency = 0.05f;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private bool _gizmos = true;
        
        [ShowInInspector, HideInEditorMode] private Collider2D[] _results;
        
        public Action<int, Collider2D[]> OnSensor;
        private int _hitCount;
        private float _timer;

        private void Awake()
        {
            _results = new Collider2D[_sensorCapacity];
        }

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            if (_timer >= _frequency)
            {
                _timer = 0;
                _hitCount = Physics2D.OverlapCircleNonAlloc(Position + _offset, _radius, _results, _layerMask);
                OnSensor?.Invoke(_hitCount, _results.Where(c => c != null).ToArray());
            }
        }
        

        private void OnDrawGizmos()
        {
            if(!_gizmos) return;
            Gizmos.color = _hitCount > 0 ? Color.blue : Color.gray;
            GizmosHelper.DrawGizmoCircle(transform.position + _offset, _radius);
        }
    }
}