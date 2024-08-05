﻿using LiteFramework.Event.Type;
using UnityEngine;

namespace LiteFramework.Event.Listener
{
    [AddComponentMenu("EventListener/"+ nameof(BoolEventListener))]
    public class BoolEventListener : GenericEventListener<bool>
    {
        [SerializeField] private BoolEvent _event;

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