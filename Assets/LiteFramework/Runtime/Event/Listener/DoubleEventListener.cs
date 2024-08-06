using LiteFramework.Event.Type;
using UnityEngine;

namespace LiteFramework.Event.Listener
{
    [AddComponentMenu("EventListener/"+ nameof(DoubleEventListener))]
    public class DoubleEventListener : GenericEventListener<double>
    {
        [SerializeField] private DoubleEvent _event;

        protected override void RegisterEvent()
        {
            _event.Register(OnEventRaise);
        }

        protected override void UnRegisterEvent()
        {
            _event.Unregister(OnEventRaise);
        }
    }
}