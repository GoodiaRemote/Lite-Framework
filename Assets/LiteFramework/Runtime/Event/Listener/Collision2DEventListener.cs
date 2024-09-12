using LiteFramework.Runtime.Event.Type;
using UnityEngine;

namespace LiteFramework.Runtime.Event.Listener
{
    [AddComponentMenu("EventListener/"+ nameof(Collision2DEventListener))]
    public class Collision2DEventListener : GenericEventListener<Collision2D>
    {
        [SerializeField] private Collision2DEvent _event;

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