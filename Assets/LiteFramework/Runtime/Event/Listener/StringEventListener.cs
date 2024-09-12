﻿using LiteFramework.Runtime.Event.Type;
using UnityEngine;

namespace LiteFramework.Runtime.Event.Listener
{
    [AddComponentMenu("EventListener/"+ nameof(StringEventListener))]
    public class StringEventListener : GenericEventListener<string>
    {
        [SerializeField] private StringEvent _event;

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