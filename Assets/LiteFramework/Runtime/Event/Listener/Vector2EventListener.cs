﻿using LiteFramework.Runtime.Event.Type;
using UnityEngine;

namespace LiteFramework.Runtime.Event.Listener
{
    [AddComponentMenu("EventListener/"+ nameof(Vector2EventListener))]
    public class Vector2EventListener : GenericEventListener<Vector2>
    {
        [SerializeField] private Vector2Event _event;

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