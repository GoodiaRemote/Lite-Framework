using LiteFramework.Runtime.Event.Type;
using UnityEngine;

namespace LiteFramework.Runtime.Event.Listener
{
    [AddComponentMenu("EventListener/"+ nameof(CollisionEventListener))]
    public class CollisionEventListener : GenericEventListener<Collision>
    {
        [SerializeField] private CollisionEvent _event;

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