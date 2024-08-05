using UnityEngine;

namespace LiteFramework.Runtime.Base
{
    public class BaseUIMonoBehaviour : MonoBehaviour
    {
        private Transform _transform;
        private RectTransform _rectTransform;
        
        public Vector2 AnchoredPosition
        {
            get => _rectTransform.anchoredPosition;
            set => _rectTransform.anchoredPosition = value;
        }

        public Vector3 AnchoredPosition3D
        {
            get => _rectTransform.anchoredPosition3D;
            set => _rectTransform.anchoredPosition3D = value;
        }
        
        protected virtual void Awake()
        {
            _transform = transform;
            _rectTransform = _transform as RectTransform;
        }
    }
}