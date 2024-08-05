using LiteFramework.Event.Type;
using UnityEngine;

namespace LiteFramework.Event.Listener
{
    [AddComponentMenu("EventListener/"+ nameof(Collider2DEventListener))]
    public class Collider2DEventListener : GenericEventListener<Collider2D>
    {
        [SerializeField] private Collider2DEvent _event;

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