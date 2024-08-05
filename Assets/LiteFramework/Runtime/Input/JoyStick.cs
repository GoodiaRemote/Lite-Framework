using LiteFramework.Runtime.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace LiteFramework.Runtime.Input
{
    public enum StickType
    {
        Fixed = 0,
        Floating = 1,
        Dynamic = 2
    }
    
    public enum AxisOptions
    {
        Both = 0,
        Horizontal = 1,
        Vertical = 2
    }
    
    [RequireComponent(typeof(RectTransform))]
    public class JoyStick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [InputControl(layout = "Vector2")]
        [SerializeField] private string _internalControlPath;
        [SerializeField] private StickType _stickType;
        [SerializeField] private AxisOptions _axis;
        [SerializeField, Min(0)] private float _movementRange = 50f;
        [SerializeField, Range(0, 1)] private float _deadZone = 0.1f;
        [SerializeField] private bool _showOnlyWhenPressed;
        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _handle;

        private RectTransform _rectTransform;
        private Canvas _canvas;

        protected override string controlPathInternal
        {
            get => _internalControlPath;
            set => _internalControlPath = value;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = ((RectTransform)transform.parent).localToWorldMatrix;
            Gizmos.color = Color.cyan;
            GizmosHelper.DrawGizmoCircle(transform.position, _movementRange);
        }

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
            _canvas = GetComponentInParent<Canvas>();
            
            if(_showOnlyWhenPressed) _background.gameObject.SetActive(false);
        }
        
        private Vector2 ScreenToAnchoredPosition(Vector2 screenPosition)
        {
            var canvasWorldCamera = _canvas.worldCamera;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPosition, canvasWorldCamera, out var localPoint))
            {
                Vector2 sizeDelta;
                var pivotOffset = _rectTransform.pivot * (sizeDelta = _rectTransform.sizeDelta);
                return localPoint - (_background.anchorMax * sizeDelta) + pivotOffset;
            }
            return Vector2.zero;
        }
        
        private Vector2 EnabledAxis()
        {
            return _axis switch
            {
                AxisOptions.Horizontal => Vector2.right,
                AxisOptions.Vertical => Vector2.up,
                _ => Vector2.one
            };
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _background.gameObject.SetActive(true);

            if (_stickType != StickType.Fixed)
            {
                _background.localPosition = ScreenToAnchoredPosition(eventData.position);
            }
            
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SentDefaultValueToControl();

            _handle.anchoredPosition = Vector2.zero;

            if (_showOnlyWhenPressed) _background.gameObject.SetActive(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var canvasWorld = _canvas.worldCamera;
            var position = RectTransformUtility.WorldToScreenPoint(canvasWorld, _background.position);

            var input = (eventData.position - position) / (_movementRange * _canvas.scaleFactor) * EnabledAxis();
            var rawMagnitude = input.magnitude;
            var normalized = input.normalized;

            if (rawMagnitude < _deadZone) input = Vector2.zero;
            else if (rawMagnitude > 1f) input = input.normalized;

            SendValueToControl(input);

            if (_stickType == StickType.Dynamic && rawMagnitude > 1f)
            {
                var difference = _movementRange * (rawMagnitude - 1f) * normalized;
                _background.anchoredPosition += difference;
            }

            _handle.anchoredPosition = input * _movementRange;
        }
    }
}