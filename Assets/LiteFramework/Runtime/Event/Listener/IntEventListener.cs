using LiteFramework.Event.Type;
using UnityEngine;

namespace LiteFramework.Event.Listener
{
    [AddComponentMenu("EventListener/"+ nameof(IntEventListener))]
    public class IntEventListener : GenericEventListener<int>
    {
        [SerializeField] private IntEvent _event;

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